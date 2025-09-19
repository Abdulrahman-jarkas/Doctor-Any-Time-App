
using AppointmentReservation.Domain.RoomAggregate;
using Microsoft.EntityFrameworkCore;

namespace AppointmentReservation.Infrastructure.Persistence.Repositories;

public class RoomRepository : IRoomRepository
{
	private readonly AppointmentsReservationDbContext _context;

	public RoomRepository(AppointmentsReservationDbContext context)
	{
		_context = context;
	}

	public async Task AddAsync(Room room)
	{
		_context.Rooms.Add(room);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Room room)
	{
		_context.Rooms.Update(room);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Room room)
	{
		_context.Rooms.Remove(room);
		await _context.SaveChangesAsync();
	}

	public Task<Room?> GetRoomByIdAsync(Guid roomId)
	{
		return _context.Rooms
							.Where(c => c.Id == roomId)
							.SingleOrDefaultAsync();
	}

	public Task<List<Room>> GetRoomsByClinicIdAsync(Guid clinicId)
	{
		return _context.Rooms
			.Where(room => room.ClinicId == clinicId)
			.ToListAsync();
	}
}