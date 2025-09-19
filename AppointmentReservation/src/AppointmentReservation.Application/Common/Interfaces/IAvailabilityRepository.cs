using AppointmentReservation.Domain.AvailabilityAggregate;

namespace AppointmentReservation.Infrastructure.Persistence.Repositories;

public interface IAvailabilityRepository
{
	Task AddAsync(Availability availability);
	Task<Availability?> Get(Guid id, AvailabilityStatus? status = AvailabilityStatus.Valid);
	Task<List<Availability>> GetAvailabilities(AvailabilityStatus status, DateOnly date);
	Task<List<Availability>> GetClinicAvailabilities(Guid clinicId, AvailabilityStatus status);
	Task<List<Availability>> GetDoctorAvailabilities(Guid doctorId, AvailabilityStatus status);
	Task UpdateAsync(Availability availability);
	Task DeleteAsync(Availability availability);
}