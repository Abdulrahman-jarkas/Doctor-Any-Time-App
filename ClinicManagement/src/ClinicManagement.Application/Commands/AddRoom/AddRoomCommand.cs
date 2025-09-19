using ErrorOr;
using MediatR;

namespace ClinicManagement.Application.Commands.AddRoom;

public record AddRoomCommand(Guid ClinicId, List<Guid> ServiceIds) : IRequest<ErrorOr<Success>>;
