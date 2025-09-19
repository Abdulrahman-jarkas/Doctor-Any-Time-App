using ErrorOr;

namespace AppointmentReservation.Domain.AppointmentAggregate;

public static class AppointmentErrors
{
	public static Error CannotCancelTooCLoseAppointment
		=> Error.Validation(
			"Appointment.CannotCancelTooCLoseAppointment",
			"appointment too close, you can not cancel it");

	public static Error CannotCancelRequest => Error.Conflict(
		"Appointment.CannotCancelRequest",
		"cannot cancel request");

	public static Error CannotCompleteRequest => Error.Conflict(
		"Appointment.CannotCancelRequest",
		"cannot complete request");

	public static Error InvalidCompleteDateTime => Error.Conflict(
		"Appointment.CannotCompleteRequest",
		"Cannot complete the request because the appointment has not started yet.");
}
