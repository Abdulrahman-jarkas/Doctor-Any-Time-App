using AppointmentReservation.Application.Common.Extensions;
using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.AvailabilityAggregate;
using AppointmentReservation.Domain.ClinicAggregate;
using AppointmentReservation.Infrastructure.Persistence.Repositories;
using ErrorOr;
using MediatR;

namespace AppointmentReservation.Application.Clinic.Commands.AcceptAppointmentRequest;

public class AcceptAvailabilityRequestCommandHandler : IRequestHandler<AcceptAvailabilityRequestCommand, ErrorOr<Success>>
{
	private readonly IAvailabilityRepository _availabilityRepository;
	private readonly IClinicRepository _clinicRepository;
	private readonly IRoomRepository _roomRepository;

	public AcceptAvailabilityRequestCommandHandler(
		IAvailabilityRepository availabilityRepository,
		IClinicRepository clinicRepository,
		IRoomRepository roomRepository
		)
	{
		_availabilityRepository = availabilityRepository;
		_clinicRepository = clinicRepository;
		_roomRepository = roomRepository;
	}

	public async Task<ErrorOr<Success>> Handle(AcceptAvailabilityRequestCommand request, CancellationToken cancellationToken)
	{
		var availability = await _availabilityRepository.Get(request.AvailabilityId);

		if (availability == null)
			return AvailabilityErrors.AvailabilityNotFound;

		var clinic = await _clinicRepository.GetClinicAsync(availability.ClinicId);

		if (clinic == null)
			return AvailabilityErrors.ClinicNotFound;

		var rooms = await _roomRepository.GetRoomsByClinicIdAsync(clinic.Id);

		if (!rooms.CanProccessAppointment(
			clinic.MaxAppointmentsPerDay,
			availability.ServiceIds.ToList(),
			availability.Date,
			availability.Time,
			out var errors))
			return errors;

		var acceptRes = availability.AcceptRequest(request.RequestId);

		if (acceptRes.IsError)
			return acceptRes.Errors;

		await _availabilityRepository.DeleteAsync(availability);

		return Result.Success;
	}
}
