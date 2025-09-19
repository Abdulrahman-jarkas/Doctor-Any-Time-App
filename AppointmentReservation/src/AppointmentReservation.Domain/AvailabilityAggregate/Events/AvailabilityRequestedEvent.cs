using AppointmentReservation.Domain.Common;
using ErrorOr;

namespace AppointmentReservation.Domain.AvailabilityAggregate.Events;

public record AvailabilityRequestedEvent(Availability Availability, Request Request) : IDomainEvent
{
	public static readonly Error PatientNotFound = Error.NotFound(
		"AvailabilityRequestedEvent.PatientNotFound",
		"Patient was not found"
		);

	public static readonly Error UpdatePatientScheduleFailed = Error.Conflict(
		"ScheduledAppointmentCanceledEvent.UpdatePatientScheduleFailed",
		"Update patient schedule failed"
	);
}
