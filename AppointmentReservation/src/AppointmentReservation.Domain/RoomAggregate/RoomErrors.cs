using ErrorOr;

namespace AppointmentReservation.Domain.RoomAggregate;

public class RoomErrors
{
	public static readonly Error CannotProvideServices =
		Error.Validation(code: "Room.CannotProvideServices",
					description: "A Room can not provide this services");

	public static readonly Error CannotHaveTwoOrMoreOverlappingAppointments =
		Error.Validation(code: "Room.CannotHaveTwoOrMoreOverlappingAppointments",
					description: "A Room can not have tow or more overlapping appointments");

	public static readonly Error RoomNotFound =
		Error.Validation(code: "Room.RoomNotFound",
					description: "Can not found room");
}
