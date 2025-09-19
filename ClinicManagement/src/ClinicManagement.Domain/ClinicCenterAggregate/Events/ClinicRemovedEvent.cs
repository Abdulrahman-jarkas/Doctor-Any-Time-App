using ClinicManagement.Domain.Common;
using ErrorOr;

namespace ClinicManagement.Domain.ClinicCenterAggregate.Events;

public record ClinicRemovedEvent(Guid ClinicId) : IDomainEvent
{
	public static readonly Error ClinicNotFound = Error.Conflict(
		"ClinicRemovedEvent.ClinicNotFound",
		"Clinic was not found");

	public static readonly Error FailedToDeleteDoctor = Error.Conflict(
		"ClinicRemovedEvent.FailedToDeleteDoctor",
		"failed to delete doctor");

	public static readonly Error FailedToDeletRoom = Error.Conflict(
		"ClinicRemovedEvent.FailedToDeleteDoctor",
		"failed to delete doctor");
}
