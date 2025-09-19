using PatientEntity = AppointmentReservation.Domain.PatientAggregate.Patient;

namespace AppointmentReservation.Application.Common.Interfaces;

public interface IPatientRepository
{
	public Task<PatientEntity?> GetAsync(Guid patientId);
	public Task UpdateAsync(PatientEntity patient);
	public Task AddAsync(PatientEntity patient);
	public Task<bool> AnyAsync();
}