using AppointmentReservation.Domain.Common;
using ErrorOr;

namespace AppointmentReservation.Domain.AvailabilityAggregate.Events;

public record AvailabilityRequestCanceledEvent(Availability Availability, Request Request) : IDomainEvent
{
	public static readonly Error PatientNotFound = Error.NotFound(
		"AvailabilityRequestCanceledEvent.PatientNotFound",
		"Patient was not found"
		);

	public static readonly Error UpdatePatientScheduleFailed = Error.Conflict(
		"AvailabilityRequestCanceledEvent.UpdatePatientScheduleFailed",
		"Update patient schedule failed"
	);
}