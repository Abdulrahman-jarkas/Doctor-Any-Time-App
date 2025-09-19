using ErrorOr;
using MediatR;

namespace AppointmentReservation.Application.Clinic.Commands.CancelAppointment;

public record CancelAppointment(Guid AppointmentId, Guid ClinicId) : IRequest<ErrorOr<Success>>;
