using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.DoctorAggregate;
using Microsoft.EntityFrameworkCore;

namespace AppointmentReservation.Infrastructure.Persistence.Repositories;

public class DoctorRepository : IDoctorRepository
{
	private readonly AppointmentsReservationDbContext _context;

	public DoctorRepository(AppointmentsReservationDbContext context)
	{
		_context = context;
	}

	public async Task AddAsync(Doctor doctor)
	{
		_context.Doctors.Add(doctor);
		await _context.SaveChangesAsync();
	}

	public async Task<Doctor?> GetDoctorAsync(Guid clinicId, Guid doctorId)
	{
		var res = await _context.Doctors
			.Where(d => d.ClinicId == clinicId && d.Id == doctorId)
			.SingleOrDefaultAsync();

		return res;
	}

	public async Task UpdateAsync(Doctor doctor)
	{
		_context.Doctors.Update(doctor);
		await _context.SaveChangesAsync();
	}

	public async Task<bool> AnyAsync()
	{
		return await _context.Doctors.AnyAsync();
	}

	public async Task RemoveDoctor(Doctor doctor)
	{
		_context.Doctors.Remove(doctor);
		await _context.SaveChangesAsync();
	}

	public Task<List<Doctor>> GetAsync(Guid clinicId)
	{
		return _context.Doctors.Where(d => d.ClinicId == clinicId).ToListAsync();
	}
}