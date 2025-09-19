using ClinicManagement.Domain.DoctorAggregate;

namespace ClinicManagement.Application.Common.Interfaces;

public interface IDoctorRepository
{
	public Task AddAsync(Doctor doctor);

	public Task Delete(Doctor doctor);

	public Task<List<Doctor>> GetByClinicId(Guid clinicId);

	public Task<Doctor?> Get(Guid id);
}

