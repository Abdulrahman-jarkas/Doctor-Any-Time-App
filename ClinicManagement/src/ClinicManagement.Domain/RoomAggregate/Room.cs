using ClinicManagement.Core.Common;

namespace ClinicManagement.Domain.RoomAggregate;

public class Room : AggregateRoot
{
	private List<Guid> _serviceIds = new();
	public Guid ClinicId { get; init; }

	public IReadOnlyList<Guid> ServiceIds => _serviceIds.AsReadOnly();

	public Room(Guid clinicId, List<Guid> serviceIds, Guid? id = null) : base(id ?? Guid.NewGuid())
	{
		ClinicId = clinicId;
		_serviceIds = serviceIds;
	}

	private Room() { }
}