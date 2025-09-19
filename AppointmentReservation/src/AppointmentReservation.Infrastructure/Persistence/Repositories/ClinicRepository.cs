using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.ClinicAggregate;
using Microsoft.EntityFrameworkCore;

namespace AppointmentReservation.Infrastructure.Persistence.Repositories;

class ClinicRepository : IClinicRepository
{
	private readonly AppointmentsReservationDbContext _context;

	public ClinicRepository(AppointmentsReservationDbContext context)
	{
		_context = context;
	}

	public Task<List<Clinic>> GetByClinicCenter(Guid id)
	{
		return _context.Clinics
			.Where(c => c.ClinicCenterId == id)
			.ToListAsync();
	}

	public Task<Clinic?> GetClinicAsync(Guid clinicId)
	{
		return _context.Clinics
							.Where(c => c.Id == clinicId)
							.SingleOrDefaultAsync();
	}

	public async Task UpdateAsync(Clinic clinic)
	{
		_context.Clinics.Update(clinic);
		await _context.SaveChangesAsync();
	}

	public async Task AddAsync(Clinic clinic)
	{
		_context.Clinics.Add(clinic);
		await _context.SaveChangesAsync();
	}

	public async Task<bool> AnyAsync()
	{
		return await _context.Clinics.AnyAsync();
	}

	public async Task DeleteAsync(Clinic clinic)
	{
		_context.Clinics.Remove(clinic);
		await _context.SaveChangesAsync();
	}
}