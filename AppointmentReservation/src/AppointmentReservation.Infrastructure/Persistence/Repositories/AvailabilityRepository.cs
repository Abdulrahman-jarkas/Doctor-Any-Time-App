using AppointmentReservation.Domain.AvailabilityAggregate;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppointmentReservation.Infrastructure.Persistence.Repositories;

public class AvailabilityRepository(AppointmentsReservationDbContext context) : IAvailabilityRepository
{
	public async Task AddAsync(Availability availability)
	{
		context.Availabilities.Add(availability);
		await context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Availability availability)
	{
		context.Availabilities.Remove(availability);
		await context.SaveChangesAsync();
	}

	public Task<Availability?> Get(Guid id, AvailabilityStatus? status = AvailabilityStatus.Valid)
	{
		return context.Availabilities
			 .Include(a => a.Requests)
			 .SingleOrDefaultAsync(a => a.Id == id && a.Status == status);
	}


	public Task<List<Availability>> GetAvailabilities(AvailabilityStatus status, DateOnly date)
	{
		return context.Availabilities
			 .Include(a => a.Requests)
			 .Where(a => a.Status == status && a.Date == date)
			 .ToListAsync();
	}

	public Task<List<Availability>> GetClinicAvailabilities(Guid clinicId, AvailabilityStatus status)
	{
		return context.Availabilities
			 .Include(a => a.Requests)
			 .Where(a => a.Status == status && a.ClinicId == clinicId)
			 .ToListAsync();
	}

	public Task<List<Availability>> GetDoctorAvailabilities(Guid doctorId, AvailabilityStatus status)
	{
		return context.Availabilities
			 .Include(a => a.Requests)
			 .Where(a => a.Status == status && a.DoctorId == doctorId)
			 .ToListAsync();
	}

	public async Task UpdateAsync(Availability availability)
	{
		context.Availabilities.Update(availability);
		await context.SaveChangesAsync();
	}
}
