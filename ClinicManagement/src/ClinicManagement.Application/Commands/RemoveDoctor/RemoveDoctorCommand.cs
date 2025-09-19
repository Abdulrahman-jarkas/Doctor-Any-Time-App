using ErrorOr;
using MediatR;

namespace ClinicManagement.Application.Commands.RemoveDoctor;

public record RemoveDoctorCommand(Guid ClinicId, Guid DoctorId) : IRequest<ErrorOr<Success>>;