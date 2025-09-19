using ClinicManagement.Core.Common;

namespace ClinicManagement.Domain.DoctorAggregate;

public class Doctor : AggregateRoot
{
	private readonly List<Guid> _serviceIds = new();
	public Guid ClinicId { get; init; }

	public IReadOnlyList<Guid> ServiceIds => _serviceIds.AsReadOnly();

	public Doctor(Guid clinicId, List<Guid> serviceIds, Guid? id = null) : base(id ?? Guid.NewGuid())
	{
		ClinicId = clinicId;
		_serviceIds = serviceIds;
	}

	private Doctor() { }
}
