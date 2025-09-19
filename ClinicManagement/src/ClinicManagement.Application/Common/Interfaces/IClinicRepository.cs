using ClinicManagement.Domain.ClinicAggregate;

namespace ClinicManagement.Application.Common.Interfaces;

public interface IClinicRepository
{
	public Task AddAsync(Clinic clinic);

	public Task<Clinic?> Get(Guid id);

	public Task UpdateAsync(Clinic clinic);

	public Task DeleteAsync(Clinic clinic);
}

