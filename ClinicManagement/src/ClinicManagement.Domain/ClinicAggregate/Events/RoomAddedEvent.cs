using ClinicManagement.Domain.Common;
using ClinicManagement.Domain.RoomAggregate;

namespace ClinicManagement.Domain.ClinicAggregate.Events;

public record RoomAddedEvent(Room Room) : IDomainEvent;
