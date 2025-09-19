using AppointmentReservation.Domain.Common;
using ErrorOr;

namespace AppointmentReservation.Domain.AvailabilityAggregate.Events;

public record AvailabiltyRequestAcceptedEvent(Availability Availability, Guid RequestId) : IDomainEvent
{
	public static Error RequestNotFound => Error.Conflict(
		"AvailabilityErrors.RequestNotFound",
		"cannot find request");

	public static readonly Error ClinicNotFound = Error.Conflict(
		"AppointmentScheduledEvent.ClinicNotFound",
		"Clinic not found"
		);

	public static readonly Error NoRoomsAvailabileAtThisTimeForThisServices = Error.Conflict(
	"AppointmentScheduledEvent.RoomNotFound",
	"No rooms availabile at this time for this services"
	);

	public static readonly Error UpdateRoomScheduleFailed = Error.Conflict(
		"AppointmentScheduledEvent.UpdateRoomScheduleFailed",
		"Update room schedule failed"
	);
}
