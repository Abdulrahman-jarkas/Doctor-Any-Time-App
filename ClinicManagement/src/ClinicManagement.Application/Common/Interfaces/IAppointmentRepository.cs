using ClinicManagement.Domain.Common.Entities;

namespace ClinicManagement.Application.Common.Interfaces;

public interface IAppointmentRepository
{
	public Task AddAsync(Appointment appointment);
	public Task<Appointment?> Get(Guid id);
	public Task<bool> IsThereAnyAppointmentsAsync(Guid clinicId, Guid? doctorId = null, Guid? roomId = null);
	public Task DeleteAsync(Appointment appointment);
}
