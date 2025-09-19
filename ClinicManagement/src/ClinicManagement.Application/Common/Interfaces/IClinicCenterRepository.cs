using ClinicManagement.Domain.ClinicCenterAggregate;

namespace ClinicManagement.Application.Common.Interfaces;

public interface IClinicCenterRepository
{
	public Task AddAsync(ClinicCenter clinicCenter);

	public Task UpdateAsync(ClinicCenter clinicCenter);

	public Task<ClinicCenter?> Get(Guid id);

	public Task<bool> AnyAsync();
}