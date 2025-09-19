using ClinicManagement.Core.Common;
using ClinicManagement.Domain.ClinicAggregate.Events;
using ClinicManagement.Domain.DoctorAggregate;
using ClinicManagement.Domain.RoomAggregate;
using ErrorOr;

namespace ClinicManagement.Domain.ClinicAggregate;

public class Clinic : AggregateRoot
{
	public Guid ClinicCenterId { get; init; }
	private List<Guid> _doctorIds = new();
	private List<Guid> _roomIds = new();

	public IReadOnlyList<Guid> DoctorIds => _doctorIds.AsReadOnly();
	public IReadOnlyList<Guid> RoomIds => _roomIds.AsReadOnly();


	public Clinic(Guid clinicCenterId, Guid? id = null) : base(id ?? Guid.NewGuid())
	{
		ClinicCenterId = clinicCenterId;
	}

	public ErrorOr<Success> AddRoom(Room room)
	{
		if (_roomIds.Contains(room.Id))
			return ClinicErrors.RoomAlreadyExist;

		_roomIds.Add(room.Id);

		_domainEvents.Add(new RoomAddedEvent(room));

		return Result.Success;
	}

	public ErrorOr<Success> RemoveRoom(Guid roomId)
	{
		if (!_roomIds.Contains(roomId))
			return ClinicErrors.RoomNotFound;

		_roomIds.Remove(roomId);

		_domainEvents.Add(new RoomRemovedEvent(Id, roomId));

		return Result.Success;
	}

	public ErrorOr<Success> AddDoctor(Doctor doctor)
	{
		if (_doctorIds.Contains(doctor.Id))
			return ClinicErrors.DoctorAlreadyExist;

		_doctorIds.Add(doctor.Id);

		_domainEvents.Add(new DoctorAddedEvent(doctor));

		return Result.Success;
	}

	public ErrorOr<Success> RemoveDoctor(Guid doctorId)
	{
		if (!_doctorIds.Contains(doctorId))
			return ClinicErrors.DoctorNotFound;

		_doctorIds.Remove(doctorId);

		_domainEvents.Add(new DoctorRemovedEvent(Id, doctorId));

		return Result.Success;
	}

	private Clinic() { }
}