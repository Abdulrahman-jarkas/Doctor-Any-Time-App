using ErrorOr;
using MediatR;

namespace AppointmentReservation.Application.Patient.Commands.RequestAvailability;

public record RequestAvailabilityCommand(Guid PatientId, Guid AvailabilityId) : IRequest<ErrorOr<Success>>;

