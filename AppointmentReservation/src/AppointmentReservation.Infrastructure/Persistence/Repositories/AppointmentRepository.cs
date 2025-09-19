using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.AppointmentAggregate;
using Microsoft.EntityFrameworkCore;

namespace AppointmentReservation.Infrastructure.Persistence.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
	private readonly AppointmentsReservationDbContext _context;

	public AppointmentRepository(AppointmentsReservationDbContext context)
	{
		_context = context;
	}
	public async Task AddAsync(Appointment appointment)
	{
		_context.Appointments.Add(appointment);
		await _context.SaveChangesAsync();
	}

	public Task<Appointment?> Get(Guid id, AppointmentStatus status)
	{
		return _context.Appointments
			.FirstOrDefaultAsync(a => a.Id == id && a.Status == status);
	}

	public async Task UpdateAsync(Appointment appointment)
	{
		Console.Write(_context.ChangeTracker.DebugView);
		//_context.Appointments.Update(appointment);

		await _context.SaveChangesAsync();
		Console.Write(_context.ChangeTracker.DebugView);
	}

	public Task<bool> IsThereAnyAppointmentsAsync(Guid clinicId, Guid? doctorId = null, Guid? roomId = null)
	{
		return _context.Appointments.AnyAsync(a =>
					a.ClinicId == clinicId &&
					a.Status == AppointmentStatus.Scheduled &&
					(doctorId == null || a.DoctorId == doctorId) &&
					(roomId == null || a.RoomId == roomId));
	}

	public Task<List<Appointment>> GetAppointmentsAsync(AppointmentStatus status)
	{
		return _context.Appointments.Where(a => a.Status == status).ToListAsync();
	}

	public Task<List<Appointment>> GetAppointmentsAsync(Guid clinicId, AppointmentStatus status, Guid? doctorId = null)
	{
		return _context.Appointments.Where(a =>
					a.ClinicId == clinicId &&
					a.Status == status &&
					(doctorId == null || a.DoctorId == doctorId)).ToListAsync();
	}
}
