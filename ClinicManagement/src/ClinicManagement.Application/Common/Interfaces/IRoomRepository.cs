using ClinicManagement.Domain.DoctorAggregate;
using ClinicManagement.Domain.RoomAggregate;

namespace ClinicManagement.Application.Common.Interfaces;

public interface IRoomRepository
{
	public Task AddAsync(Room room);

	public Task DeleteAsync(Room room);

	public Task<List<Room>> GetByClinicId(Guid clinicId);

	public Task<Room?> Get(Guid id);
}