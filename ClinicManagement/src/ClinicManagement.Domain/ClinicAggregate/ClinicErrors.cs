using ErrorOr;

namespace ClinicManagement.Domain.ClinicAggregate;

public record ClinicErrors
{
	public static readonly Error DoctorAlreadyExist = Error.Conflict(
		"Clinic",
		"Doctor Already exist in clinic"
		);

	public static readonly Error DoctorNotFound = Error.NotFound(
		"Clinic",
		"Doctor was not found in clinic"
		);

	public static readonly Error RoomAlreadyExist = Error.Conflict(
		"Clinic",
		"Room Already exist in clinic"
		);

	public static readonly Error RoomNotFound = Error.NotFound(
		"Clinic",
		"Room was not found in clinic"
		);
}
