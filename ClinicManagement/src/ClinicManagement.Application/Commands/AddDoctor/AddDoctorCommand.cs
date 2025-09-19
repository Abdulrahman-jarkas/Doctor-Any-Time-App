using ErrorOr;
using MediatR;

namespace ClinicManagement.Application.Commands.AddDoctor;

public record AddDoctorCommand(Guid ClinicId, List<Guid> ServiceIds) : IRequest<ErrorOr<Success>>;
