using ErrorOr;

namespace ClinicManagement.Domain.DoctorAggregate;

public record DoctorErrors
{
	public static readonly Error CannotDeleteDoctorWithPendingAppointments = Error.Conflict(
			"Doctor.CannotDeleteDoctorWithPendingAppointments",
			"The doctor cannot be deleted because it has pending appointments.");
}