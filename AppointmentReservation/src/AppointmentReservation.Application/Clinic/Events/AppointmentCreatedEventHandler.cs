using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.AppointmentAggregate.Events;
using AppointmentReservation.Domain.Common.EventualConsistency;
using AppointmentReservation.Infrastructure.Persistence.Repositories;
using MediatR;

namespace AppointmentReservation.Application.Clinic.Events;

public class AppointmentCreatedEventHandler(
	IClinicRepository clinicRepository,
	IRoomRepository roomRepository
	)
	: INotificationHandler<AppointmentCreatedEvent>
{
	public async Task Handle(AppointmentCreatedEvent notification, CancellationToken cancellationToken)
	{
		var clinic = await clinicRepository.GetClinicAsync(notification.Appointment.ClinicId);

		if (clinic == null)
			throw new EventualConsistencyException(AppointmentCreatedEvent.ClinicNotFound);

		var room = await roomRepository.GetRoomByIdAsync(notification.Appointment.RoomId);

		if (room == null)
			throw new EventualConsistencyException(AppointmentCanceledEvent.RoomNotFound);

		var addToScheduleRes = room.AddToSchedule(notification.Appointment.Date, notification.Appointment.Time);

		if (addToScheduleRes.IsError)
			throw new EventualConsistencyException(AppointmentCreatedEvent.UpdateRoomScheduleFailed);

		await clinicRepository.UpdateAsync(clinic);
	}
}

