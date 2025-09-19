using AppointmentReservation.Domain.Common;
using ErrorOr;

namespace AppointmentReservation.Domain.AppointmentAggregate.Events;

public record AppointmentCreatedEvent(Appointment Appointment) : IDomainEvent
{
	public static readonly Error ClinicNotFound = Error.Conflict(
		"AvailabilityPublishedEvent.ClinicNotFound",
		"clinic not found"
		);

	public static readonly Error UpdateRoomScheduleFailed = Error.Conflict(
		"AppointmentScheduledEvent.UpdateRoomScheduleFailed",
		"Update room schedule failed"
	);
}
