using ErrorOr;
using MediatR;

namespace ClinicManagement.Application.Commands.RemoveClinic;

public record RemoveClinicCommand(Guid ClinicCenterId, Guid ClinicId) : IRequest<ErrorOr<Success>>;
