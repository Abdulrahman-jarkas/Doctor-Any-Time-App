using AppointmentReservation.Application.Common.Extensions;
using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.AvailabilityAggregate;
using AppointmentReservation.Domain.ClinicAggregate;
using AppointmentReservation.Domain.PatientAggregate;
using AppointmentReservation.Infrastructure.Persistence.Repositories;
using ErrorOr;
using MediatR;

namespace AppointmentReservation.Application.Patient.Commands.RequestAvailability;

public class RequestAvailabilityCommandHandler : IRequestHandler<RequestAvailabilityCommand, ErrorOr<Success>>
{
	private readonly IPatientRepository _patientRepository;
	private readonly IClinicRepository _clinicRepository;
	private readonly IAvailabilityRepository _availabilityRepository;
	private readonly IRoomRepository _roomRepository;


	public RequestAvailabilityCommandHandler(
		IPatientRepository patientRepository,
		IClinicRepository clinicRepository,
		IAvailabilityRepository availabilityRepository,
		IRoomRepository roomRepository
		)
	{
		_patientRepository = patientRepository;
		_clinicRepository = clinicRepository;
		_availabilityRepository = availabilityRepository;
		_roomRepository = roomRepository;
	}

	public async Task<ErrorOr<Success>> Handle(RequestAvailabilityCommand request, CancellationToken cancellationToken)
	{
		var availability = await _availabilityRepository.Get(request.AvailabilityId);

		if (availability == null)
			return AvailabilityErrors.AvailabilityNotFound;

		var patient = await _patientRepository.GetAsync(request.PatientId);

		if (patient == null)
			return PatientErrors.PatientNotFound;

		if (!patient.IsAvailableAt(availability.Date, availability.Time))
			return PatientErrors.CannotHaveTwoOrMoreOverlappingAppointments;

		var clinic = await _clinicRepository.GetClinicAsync(availability.ClinicId);

		if (clinic == null)
			return ClinicErrors.ClinicNotFound;

		var rooms = await _roomRepository.GetRoomsByClinicIdAsync(clinic.Id);

		if (!rooms.CanProccessAppointment(
			clinic.MaxAppointmentsPerDay,
			availability.ServiceIds.ToList(),
			availability.Date,
			availability.Time,
			out var errors))
			return ClinicErrors.ClinicCanNotHandleThisAppointmentForNow;

		var addReqResult = availability.Request(request.PatientId);

		if (addReqResult.IsError)
			return addReqResult.Errors;

		await _availabilityRepository.UpdateAsync(availability);

		return Result.Success;
	}
}
