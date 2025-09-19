using ErrorOr;

namespace AppointmentReservation.Domain.ClinicAggregate;

public static class ClinicErrors
{
	public static readonly Error CannotHaveMoreSessionThanSubscriptionAllows = Error.Validation(
						code: "Clinic.CannotHaveMoreAppointmentsThanSubscriptionAllows",
						description: "A clinic cannot have more scheduled appointments than the subscription allows");

	public static readonly Error ThereAreNoRoomsToProvideThisServices = Error.Validation(
						code: "Clinic.ThereAreNoRoomsToProvideThisServices",
						description: "There are no rooms to provide this services");

	public static readonly Error TheDoctorCannotProvideThisServices = Error.Validation(
						code: "Clinic.TheDoctorCannotProvideThisServices",
						description: "The doctor can not provide this services");

	public static readonly Error ThereAreNoRoomsToProvideThisServicesAtThisTime = Error.Validation(
						code: "Clinic.ThereAreNoRoomsToProvideThisServicesAtThisTime",
						description: "There are no rooms at this time to provide this services for now");

	public static readonly Error ClinicCanNotHandleThisAppointmentForNow = Error.Conflict(
						code: "Clinic.ClinicCanNotHandleThisAppointmentForNow",
						description: "Clinic can not handle this appointment for now");


	public static readonly Error NoRoomReservedForAppointment = Error.Unexpected(
						code: "Clinic.NoRoomReservedForAppointment",
						description: "no room reserved for this scheduled appointemnt");

	public static readonly Error RoomNotFound = Error.Unexpected(
						code: "Clinic.RoomNotFound",
						description: "Room not found");

	public static readonly Error RoomAlreadyFound = Error.Unexpected(
						code: "Clinic.RoomAlreadyFound",
						description: "Room already exist");

	public static readonly Error ClinicNotFound = Error.Unexpected(
					code: "Clinic.ClinicNotFound",
					description: "Clinic not found");

	public static readonly Error DeleteRoomFailed = Error.Validation(
					code: "Clinic.DeleteRoomFailed",
					description: "Room deletion failed, This room has scheduled appointments. Please cancel them before deleting.");

	public static readonly Error DeleteClinicFailed = Error.Validation(
					code: "Clinic.DeleteClinicFailed",
					description: "Clinic deletion failed, This clinic has scheduled appointments. Please cancel them before deleting.");

	public static readonly Error InvalidMaxAppointmentPerDay = Error.Validation(
					code: "Clinic.InvalidMaxAppointmentPerDay",
					description: "Invalid number for max appointment per day");
}
