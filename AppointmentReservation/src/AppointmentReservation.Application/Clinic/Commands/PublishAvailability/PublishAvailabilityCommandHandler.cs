using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.Common.ValueObjects;
using ErrorOr;
using MediatR;
using AvailabilityEntity = AppointmentReservation.Domain.AvailabilityAggregate.Availability;
using AppointmentReservation.Infrastructure.Persistence.Repositories;
using AppointmentReservation.Domain.AvailabilityAggregate;
using AppointmentReservation.Application.Common.Extensions;

namespace AppointmentReservation.Application.Clinic.Commands.PublishAvailability;

public class PublishAvailabilityCommandHandler : IRequestHandler<PublishAvailabilityCommand, ErrorOr<AvailabilityEntity>>
{
	private readonly IClinicRepository _clinicRepository;
	private readonly IDoctorRepository _doctorRepository;
	private readonly IAvailabilityRepository _availabilityRepository;
	private readonly IRoomRepository _roomRepository;


	public PublishAvailabilityCommandHandler(
		IClinicRepository clinicRepository,
		IDoctorRepository doctorRepository,
		IAvailabilityRepository availabilityRepository,
		IRoomRepository roomRepository)
	{
		_clinicRepository = clinicRepository;
		_doctorRepository = doctorRepository;
		_availabilityRepository = availabilityRepository;
		_roomRepository = roomRepository;
	}

	public async Task<ErrorOr<AvailabilityEntity>> Handle(PublishAvailabilityCommand request, CancellationToken cancellationToken)
	{
		var timeRange = TimeRange.FromTimes(request.From, request.To);

		if (timeRange.IsError)
			return timeRange.Errors;

		var clinic = await _clinicRepository.GetClinicAsync(request.ClinicId);

		if (clinic == null)
			return AvailabilityErrors.ClinicNotFound;

		var rooms = await _roomRepository.GetRoomsByClinicIdAsync(clinic.Id);

		if (!rooms.CanProccessAppointment(
			clinic.MaxAppointmentsPerDay,
			request.ServiceIds,
			request.Date,
			timeRange.Value,
			out var clinicErrors))
			return clinicErrors;

		var doctor = await _doctorRepository.GetDoctorAsync(clinic.Id, request.DoctorId);

		if(doctor == null)
			return AvailabilityErrors.DoctorNotFound;

		if (!doctor.CanProccessAppointment(request.ServiceIds, request.Date, timeRange.Value, out var doctorErrors))
			return doctorErrors;

		var availability = new AvailabilityEntity(clinic.Id, doctor.Id, request.ServiceIds, request.Date, timeRange.Value);

		await _availabilityRepository.AddAsync(availability);

		return availability;
	}
}
