using ClinicManagement.Domain.Common;
using ErrorOr;

namespace ClinicManagement.Domain.ClinicAggregate.Events;

public record DoctorRemovedEvent(Guid ClinicId, Guid DoctorId) : IDomainEvent
{
	public static readonly Error DoctorNotFound = Error.Conflict(
		"DoctorRemovedEvent.DoctorNotFound",
		"Doctor was not found"
		);
}
