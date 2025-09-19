using AppointmentReservation.Domain.RoomAggregate;
using ClinicEntity = AppointmentReservation.Domain.ClinicAggregate.Clinic;

namespace AppointmentReservation.Application.Common.Interfaces;

public interface IClinicRepository
{
	public Task<ClinicEntity?> GetClinicAsync(Guid clinicId);
	public Task UpdateAsync(ClinicEntity clinic);
	public Task AddAsync(ClinicEntity clinic);
	public Task<bool> AnyAsync();
	public Task DeleteAsync(ClinicEntity clinic);
	Task<List<ClinicEntity>> GetByClinicCenter(Guid id);
}

