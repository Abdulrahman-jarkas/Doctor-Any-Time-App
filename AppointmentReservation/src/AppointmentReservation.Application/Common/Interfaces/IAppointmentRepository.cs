using AppointmentReservation.Domain.AppointmentAggregate;
using AppointmentEntity = AppointmentReservation.Domain.AppointmentAggregate.Appointment;

namespace AppointmentReservation.Application.Common.Interfaces;

public interface IAppointmentRepository
{
	public Task AddAsync(AppointmentEntity appointment);
	public Task<AppointmentEntity?> Get(Guid id, AppointmentStatus status);
	public Task<bool> IsThereAnyAppointmentsAsync(Guid clinicId, Guid? doctorId = null, Guid? roomId = null);
	public Task UpdateAsync(AppointmentEntity appointment);
	public Task<List<AppointmentEntity>> GetAppointmentsAsync(Guid clinicId, AppointmentStatus status, Guid? doctorId = null);
}
