using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.ClinicAggregate;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Infrastructure.Persistence.Repositories;

public class ClinicRepository(ClinicManagementDbContext context) : IClinicRepository
{

	public async Task AddAsync(Clinic clinic)
	{
		context.Clinics.Add(clinic);
		await context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Clinic clinic)
	{
		context.Clinics.Remove(clinic);
		await context.SaveChangesAsync();
	}

	public Task<Clinic?> Get(Guid id)
	{
		return context.Clinics.FirstOrDefaultAsync(c => c.Id == id);
	}

	public async Task UpdateAsync(Clinic clinic)
	{
		context.Clinics.Update(clinic);
		await context.SaveChangesAsync();
	}
}
