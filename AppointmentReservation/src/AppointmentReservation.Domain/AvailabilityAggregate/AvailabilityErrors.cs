using ErrorOr;

namespace AppointmentReservation.Domain.AvailabilityAggregate;

public class AvailabilityErrors
{
	public static Error PatientCannotRequestAppointmentTwice => Error.Conflict(
			"AvailabilityErrors.PatientCannotRequestAppointmentTwice",
			"patient can not request appointment twice");

	public static Error PatientRequestWasNotFound => Error.NotFound(
			"AvailabilityErrors.PatientRequestWasNotFound",
			"patient request not found");

	public static Error CannotCancelRequest => Error.Conflict(
			"AvailabilityErrors.CannotCancelRequest",
			"cannot cancel request");

	public static Error CannotAcceptRequest => Error.Conflict(
			"AvailabilityErrors.CannotAcceptRequest",
			"cannot accept request");

	public static Error CannotConfirmRequest => Error.Conflict(
		"AvailabilityErrors.CannotConfirmRequest",
		"cannot confirm request");

	public static Error AvailabilityNotFound => Error.Conflict(
			"AvailabilityErrors.AvailabilityNotFound",
			"cannot find availability");

	public static Error PatientNotFound => Error.Unexpected(
			"AvailabilityErrors.PatientNotFound",
			"cannot find patient");

	public static Error ClinicNotFound => Error.Unexpected(
			"AvailabilityErrors.ClinicNotFound",
			"cannot find clinic");

	public static Error DoctorNotFound => Error.NotFound(
		"AvailabilityErrors.DoctorNotFound",
		"cannot find doctor");

	public static Error RoomNotFound => Error.Unexpected(
			"AvailabilityErrors.RoomNotFound",
			"cannot find room");

	public static Error RoomAlreadyReserved => Error.Unexpected(
			"AvailabilityErrors.RoomAlreadyReserved",
			"The room cannot be reserved because it is already booked.");

	public static Error AvailabilityNotValidForNow => Error.Conflict(
			"AvailabilityErrors.AvailabilityNotValidForNow",
			"The availability is not valid for now");
}
