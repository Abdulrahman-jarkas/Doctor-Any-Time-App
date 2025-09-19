using ErrorOr;
using MediatR;

namespace AppointmentReservation.Application.Patient.Commands.CancelAvailabilityRequest;

public record CancelAvailabilityRequestCommand(Guid AvailabilityId, Guid RequestId) : IRequest<ErrorOr<Success>>;

