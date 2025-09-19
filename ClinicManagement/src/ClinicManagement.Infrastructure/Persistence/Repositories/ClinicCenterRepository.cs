using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.ClinicCenterAggregate;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Infrastructure.Persistence.Repositories;

public class ClinicCenterRepository(ClinicManagementDbContext context) : IClinicCenterRepository
{

	public async Task AddAsync(ClinicCenter clinicCenter)
	{
		context.ClinicCenters.Add(clinicCenter);
		await context.SaveChangesAsync();
	}

	public Task<bool> AnyAsync()
	{
		return context.ClinicCenters.AnyAsync();
	}

	public Task<ClinicCenter?> Get(Guid id)
	{
		return context.ClinicCenters.FirstOrDefaultAsync(cc => cc.Id == id);
	}

	public async Task UpdateAsync(ClinicCenter clinicCenter)
	{
		context.ClinicCenters.Update(clinicCenter);
		await context.SaveChangesAsync();
	}
}