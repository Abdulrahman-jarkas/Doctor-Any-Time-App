using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.RoomAggregate;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Infrastructure.Persistence.Repositories;

public class RoomRepository(ClinicManagementDbContext context) : IRoomRepository
{

	public async Task AddAsync(Room room)
	{
		context.Rooms.Add(room);
		await context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Room room)
	{
		context.Rooms.Remove(room);
		await context.SaveChangesAsync();
	}

	public Task<Room?> Get(Guid id)
	{
		return context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
	}

	public Task<List<Room>> GetByClinicId(Guid clinicId)
	{
		return context.Rooms.Where(d => d.ClinicId == clinicId).ToListAsync();
	}
}