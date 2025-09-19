using ErrorOr;

namespace AppointmentReservation.Domain.PatientAggregate;

public class PatientErrors
{
	public static readonly Error CannotHaveTwoOrMoreOverlappingAppointments = Error.Validation(
					"Patient.CannotHaveTwoOrMoreOverlappingAppointments",
					"A Patient can not have tow or more overlapping appointments");

	public static readonly Error PatientNotFound = Error.Validation(
					"Patient.PatientNotFound",
					"Patient was not found");
}