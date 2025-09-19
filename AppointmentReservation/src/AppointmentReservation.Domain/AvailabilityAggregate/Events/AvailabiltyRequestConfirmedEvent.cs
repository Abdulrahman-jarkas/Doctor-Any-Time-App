using AppointmentReservation.Domain.Common;
using ErrorOr;

namespace AppointmentReservation.Domain.AvailabilityAggregate.Events;

public record AvailabiltyRequestConfirmedEvent(Availability Availability, Guid RequestId) : IDomainEvent
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
}
