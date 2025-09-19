using ErrorOr;
using MediatR;

namespace ClinicManagement.Application.Commands.AddClinic;

public record AddClinicCommand(Guid ClinicCenterId) : IRequest<ErrorOr<Success>>;
