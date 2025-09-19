using ErrorOr;

namespace ClinicManagement.Domain.ClinicCenterAggregate;

public record ClinicCenterErrors
{
	public static readonly Error ExceedMaxNumberOfClinics = Error.Validation(
		"ClinicCenter",
		"Exceed max number of clinics"
		);

	public static readonly Error NoClinicToRemove = Error.NotFound(
		"ClinicCenter",
		"There is no clinic to remove"
		);

	public static readonly Error ClinicAlreadyExist = Error.Conflict(
		"ClinicCenter",
		"Clinic already exist"
		);

	public static readonly Error CannotDeleteClinicWithPendingAppointments = Error.Conflict(
		"ClinicCenter.CannotDeleteClinicWithPendingAppointments",
		"The clinic cannot be deleted because it has pending appointments.");
}