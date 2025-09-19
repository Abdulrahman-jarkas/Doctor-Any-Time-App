using ErrorOr;
using MediatR;

namespace AppointmentReservation.Application.Patient.Commands.CancelAppointment;

public record PatientCancelAppointmentCommand(Guid AppointmentId, Guid PatientId) : IRequest<ErrorOr<Success>>;
