using ErrorOr;
using MediatR;

namespace AppointmentReservation.Application.Clinic.Commands.AcceptAppointmentRequest;

public record AcceptAvailabilityRequestCommand(
	Guid AvailabilityId,
	Guid RequestId) : IRequest<ErrorOr<Success>>;