using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.PatientAggregate;
using Microsoft.EntityFrameworkCore;

namespace AppointmentReservation.Infrastructure.Persistence.Repositories;

public class PatientRepository : IPatientRepository
{
	private readonly AppointmentsReservationDbContext _context;

	public PatientRepository(AppointmentsReservationDbContext context)
	{
		_context = context;
	}

	public async Task AddAsync(Patient patient)
	{
		_context.Patients.Add(patient);
		await _context.SaveChangesAsync();
	}

	public async Task<Patient?> GetAsync(Guid patientId)
	{
		var res = await _context.Patients.FindAsync(patientId);

		return res;
	}

	public async Task UpdateAsync(Patient patient)
	{
		_context.Patients.Update(patient);
		await _context.SaveChangesAsync();
	}

	public async Task<bool> AnyAsync()
	{
		return await _context.Patients.AnyAsync();
	}
}

