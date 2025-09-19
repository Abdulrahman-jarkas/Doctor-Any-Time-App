using ErrorOr;
using MediatR;
using AvailabilityEntity =  AppointmentReservation.Domain.AvailabilityAggregate.Availability;

namespace AppointmentReservation.Application.Clinic.Commands.PublishAvailability;

public record PublishAvailabilityCommand(
	Guid ClinicId,
	Guid DoctorId,
	List<Guid> ServiceIds,
	DateOnly Date,
	TimeOnly From,
	TimeOnly To) : IRequest<ErrorOr<AvailabilityEntity>>;