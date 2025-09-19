using ErrorOr;
using MediatR;

namespace ClinicManagement.Application.Commands.RemoveRoom;

public record RemoveRoomCommand(Guid ClinicId, Guid RoomId) : IRequest<ErrorOr<Success>>;
