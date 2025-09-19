using ErrorOr;

namespace AppointmentReservation.Domain.DoctorAggregate;

public class DoctorErrors
{
	public static readonly Error CannotProvideServices =
		Error.Validation(code: "Doctor.CannotProvideServices",
					description: "A Doctor can not provide this services");

	public static readonly Error CannotHaveTwoOrMoreOverlappingAppointments =
		Error.Validation(code: "Doctor.CannotHaveTwoOrMoreOverlappingAppointments",
					description: "A Doctor can not have tow or more overlapping appointments");

	public static readonly Error DoctorNotFound =
		Error.NotFound(code: "Doctor.DoctorNotFound",
					description: "Doctor not found");

	public static readonly Error DeleteDoctorFailed =
		Error.Validation(code: "Doctor.DeleteDoctorFailed",
					description: "Doctor deletion failed, This doctor has scheduled appointments. Please cancel them before deleting.");
}
