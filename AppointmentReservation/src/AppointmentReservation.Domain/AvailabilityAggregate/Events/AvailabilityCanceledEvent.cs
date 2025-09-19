using AppointmentReservation.Domain.Common;
using ErrorOr;

namespace AppointmentReservation.Domain.AvailabilityAggregate.Events;

public record AvailabilityCanceledEvent(Availability Availability) : IDomainEvent
{
	public static readonly Error DoctorNotFound = Error.Conflict(
		"ScheduledAppointmentCanceledEvent.DoctorNotFound",
		"doctor not found"
		);

	public static readonly Error UpdateDoctorScheduleFailed = Error.Conflict(
		"ScheduledAppointmentCanceledEvent.UpdateDoctorScheduleFailed",
		"Update doctor schedule failed"
	);
}
