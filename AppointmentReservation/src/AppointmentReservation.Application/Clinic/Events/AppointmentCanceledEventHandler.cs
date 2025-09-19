using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.AppointmentAggregate.Events;
using AppointmentReservation.Domain.Common.EventualConsistency;
using AppointmentReservation.Infrastructure.Persistence.Repositories;
using MediatR;

namespace AppointmentReservation.Application.Clinic.Events;

public class AppointmentCanceledEventHandler : INotificationHandler<AppointmentCanceledEvent>
{
	private readonly IClinicRepository _clinicRepository;
	private readonly IRoomRepository _roomRepository;

	public AppointmentCanceledEventHandler(
		IClinicRepository clinicRepository,
		IRoomRepository roomRepository)
	{
		_clinicRepository = clinicRepository;
		_roomRepository = roomRepository;
	}

	public async Task Handle(AppointmentCanceledEvent notification, CancellationToken cancellationToken)
	{
		var clinic = await _clinicRepository.GetClinicAsync(notification.Appointment.ClinicId);

		if (clinic == null)
			throw new EventualConsistencyException(AppointmentCanceledEvent.ClinicNotFound);

		var room = await _roomRepository.GetRoomByIdAsync(notification.Appointment.RoomId);

		if(room == null)
			throw new EventualConsistencyException(AppointmentCanceledEvent.RoomNotFound);

		var removeFromScheduleRes = room.RemoveFromSchedule(notification.Appointment.Date, notification.Appointment.Time);

		if (removeFromScheduleRes.IsError)
			 throw new EventualConsistencyException(AppointmentCanceledEvent.UpdateRoomScheduleFailed);

		await _clinicRepository.UpdateAsync(clinic);
	}
}