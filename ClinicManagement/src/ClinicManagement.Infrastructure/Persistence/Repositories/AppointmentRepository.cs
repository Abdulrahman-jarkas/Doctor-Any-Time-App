using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Infrastructure.Persistence.Repositories;

public class AppointmentRepository(ClinicManagementDbContext context) : IAppointmentRepository
{

	public async Task AddAsync(Appointment appointment)
	{
		context.Appointments.Add(appointment);
		await context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Appointment appointment)
	{
		context.Appointments.Remove(appointment);
		await context.SaveChangesAsync();
	}

	public Task<Appointment?> Get(Guid id)
	{
		return context.Appointments.FirstOrDefaultAsync(r => r.Id == id);
	}

	public Task<bool> IsThereAnyAppointmentsAsync(Guid clinicId, Guid? doctorId = null, Guid? roomId = null)
	{
		return context.Appointments.AnyAsync(a =>
					a.ClinicId == clinicId &&
					(doctorId == null || a.DoctorId == doctorId) &&
					(roomId == null || a.RoomId == roomId));
	}
}