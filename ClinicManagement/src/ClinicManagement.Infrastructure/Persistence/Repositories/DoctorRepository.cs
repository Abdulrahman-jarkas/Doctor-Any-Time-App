using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.DoctorAggregate;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Infrastructure.Persistence.Repositories;

public class DoctorRepository(ClinicManagementDbContext context) : IDoctorRepository
{
	public async Task AddAsync(Doctor doctor)
	{
		context.Doctors.Add(doctor);
		await context.SaveChangesAsync();
	}

	public async Task Delete(Doctor doctor)
	{
		context.Doctors.Remove(doctor);
		await context.SaveChangesAsync();
	}

	public Task<Doctor?> Get(Guid id)
	{
		return context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
	}

	public Task<List<Doctor>> GetByClinicId(Guid clinicId)
	{
		return context.Doctors.Where(d => d.ClinicId == clinicId).ToListAsync();
	}
}
