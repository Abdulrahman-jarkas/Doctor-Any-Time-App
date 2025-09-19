using AppointmentReservation.Domain.Common;
using ErrorOr;

namespace AppointmentReservation.Domain.AvailabilityAggregate.Events;

public record AvailabilityPublishedEvent(Availability Availability) : IDomainEvent
{
	public static readonly Error DoctorNotFound = Error.Conflict(
		"AvailabilityPublishedEvent.DoctorNotFound",
		"doctor not found"
		);

	public static readonly Error UpdateDoctorScheduleFailed = Error.Conflict(
		"AvailabilityPublishedEvent.UpdateDoctorScheduleFailed",
		"Update doctor schedule failed"
	);
}
