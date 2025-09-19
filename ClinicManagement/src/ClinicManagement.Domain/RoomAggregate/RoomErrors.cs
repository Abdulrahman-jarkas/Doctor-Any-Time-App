using ErrorOr;

namespace ClinicManagement.Domain.RoomAggregate;

public record RoomErrors
{
	public static readonly Error CannotDeleteRoomWithPendingAppointments = Error.Conflict(
			"Room.CannotDeleteRoomWithPendingAppointments",
			"The room cannot be deleted because it has pending appointments.");
}