using ClinicManagement.Domain.Common;
using ErrorOr;

namespace ClinicManagement.Domain.ClinicAggregate.Events;

public record RoomRemovedEvent(Guid ClinicId, Guid RoomId) : IDomainEvent
{
	public static readonly Error RoomNotFound = Error.Conflict(
		"RoomRemovedEvent.RoomNotFound",
		"Doctor was not found"
		);
}
