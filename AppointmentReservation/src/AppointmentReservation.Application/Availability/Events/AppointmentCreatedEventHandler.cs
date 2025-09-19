using AppointmentReservation.Application.Common.Extensions;
using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.AppointmentAggregate.Events;
using AppointmentReservation.Domain.AvailabilityAggregate;
using AppointmentReservation.Domain.Common.EventualConsistency;
using AppointmentReservation.Infrastructure.Persistence.Repositories;
using MediatR;

namespace AppointmentReservation.Application.Availability.Events;

public class AppointmentCreatedEventHandler(
	IAvailabilityRepository availabilityRepository,
	IClinicRepository clinicRepository,
	IRoomRepository roomRepository
	) : INotificationHandler<AppointmentCreatedEvent>
{
	public async Task Handle(AppointmentCreatedEvent notification, CancellationToken cancellationToken)
	{
		var clinic = await clinicRepository.GetClinicAsync(notification.Appointment.ClinicId);

		if (clinic == null)
			throw new EventualConsistencyException(AppointmentCreatedEvent.ClinicNotFound);

		var rooms = await roomRepository.GetRoomsByClinicIdAsync(clinic.Id);

		var validAvailabilities = await availabilityRepository.GetAvailabilities(AvailabilityStatus.Valid, notification.Appointment.Date);

		var canHandlerMoreAppointmentForDay = rooms
			.CanAddMoreAppointments(notification.Appointment.Date, clinic.MaxAppointmentsPerDay);

		if (!canHandlerMoreAppointmentForDay)
		{
			foreach (var availability in validAvailabilities)
			{
				var markAsInvalidRes = availability.MarkAsInvalid();

				if (markAsInvalidRes.IsError)
					throw new EventualConsistencyException(markAsInvalidRes.FirstError);


				await availabilityRepository.UpdateAsync(availability);
			}

			return;
		}

		foreach (var availability in validAvailabilities)
		{
			if (!rooms.CanReserveRoom(availability.ServiceIds.ToList(), availability.Date, availability.Time))
			{
				var markAsInvalidRes = availability.MarkAsInvalid();

				if (markAsInvalidRes.IsError)
					throw new EventualConsistencyException(markAsInvalidRes.FirstError);

				await availabilityRepository.UpdateAsync(availability);
			}

		}
	}
}
