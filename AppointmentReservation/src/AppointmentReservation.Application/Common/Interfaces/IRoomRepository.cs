
using AppointmentReservation.Domain.Common.ValueObjects;
using AppointmentReservation.Domain.RoomAggregate;

namespace AppointmentReservation.Infrastructure.Persistence.Repositories;

public  interface IRoomRepository
{
	public Task UpdateAsync(Room room);
	public Task AddAsync(Room room);
	public Task DeleteAsync(Room room);
	Task<Room?> GetRoomByIdAsync(Guid roomId);
	Task<List<Room>> GetRoomsByClinicIdAsync(Guid clinicId);
	Task<Room?> GetAvailabileRoomAsync(Guid clinicId, DateOnly date, TimeRange time, IReadOnlyList<Guid> serviceIds);
}