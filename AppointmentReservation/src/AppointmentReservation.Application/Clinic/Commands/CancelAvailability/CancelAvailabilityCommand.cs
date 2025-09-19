using ErrorOr;
using MediatR;

namespace AppointmentReservation.Application.Clinic.Commands.CancelPublishedAppointment;

public record CancelAvailabilityCommand(Guid AvailabilityId, Guid ClinicId) : IRequest<ErrorOr<Success>>;
