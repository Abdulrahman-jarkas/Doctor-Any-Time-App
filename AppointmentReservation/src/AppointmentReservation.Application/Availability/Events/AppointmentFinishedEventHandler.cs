using AppointmentReservation.Application.Common.Extensions;
using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.AppointmentAggregate.Events;
using AppointmentReservation.Domain.AvailabilityAggregate;
using AppointmentReservation.Domain.Common.EventualConsistency;
using AppointmentReservation.Infrastructure.Persistence.Repositories;
using MediatR;

namespace AppointmentReservation.Application.Availability.Events;

public class AppointmentFinishedEventHandler(
	IAvailabilityRepository availabilityRepository,
	IClinicRepository clinicRepository,
	IRoomRepository roomRepository
	)
	: INotificationHandler<AppointmentCanceledEvent>,
	  INotificationHandler<AppointmentCompletedEvent>
{
	public async Task Handle(AppointmentCanceledEvent notification, CancellationToken cancellationToken)
	{
		await HandleInvalidAvailabilities(notification.Appointment.ClinicId, notification.Appointment.Date);
	}

	public async Task Handle(AppointmentCompletedEvent notification, CancellationToken cancellationToken)
	{
		await HandleInvalidAvailabilities(notification.Appointment.ClinicId, notification.Appointment.Date);
	}

	private async Task HandleInvalidAvailabilities(Guid clinicId, DateOnly date)
	{
		var clinic = await clinicRepository.GetClinicAsync(clinicId);

		if (clinic == null)
			throw new EventualConsistencyException(AppointmentCreatedEvent.ClinicNotFound);

		var rooms = await roomRepository.GetRoomsByClinicIdAsync(clinic.Id);

		var validAvailabilities = await availabilityRepository.GetAvailabilities(AvailabilityStatus.Invalid, date);

		var canHandlerMoreAppointmentForDay = rooms.CanAddMoreAppointments(date, clinic.MaxAppointmentsPerDay);

		if (canHandlerMoreAppointmentForDay)
		{
			foreach (var availability in validAvailabilities)
			{
				if (rooms.CanReserveRoom(availability.ServiceIds.ToList(), availability.Date, availability.Time))
				{
					var markAsInvalidRes = availability.MarkAsValid();

					if (markAsInvalidRes.IsError)
						throw new EventualConsistencyException(markAsInvalidRes.FirstError);

					await availabilityRepository.UpdateAsync(availability);
				}

			}
		}

	}
}
