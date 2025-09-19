using DoctorEntity =  AppointmentReservation.Domain.DoctorAggregate.Doctor;

namespace AppointmentReservation.Application.Common.Interfaces;

public interface IDoctorRepository
{
	public Task<DoctorEntity?> GetDoctorAsync(Guid clinicId, Guid doctorId);
	public Task UpdateAsync(DoctorEntity doctor);
	public Task AddAsync(DoctorEntity doctor);
	public Task<bool> AnyAsync();
	public Task<List<DoctorEntity>> GetAsync(Guid clinicId);
	public Task RemoveDoctor(DoctorEntity doctor);
}