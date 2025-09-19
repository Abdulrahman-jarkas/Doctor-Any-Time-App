using AppointmentReservation.Domain.Common;

namespace AppointmentReservation.Domain.ClinicAggregate.Events;

public record RoomRemovedEvent(Clinic Clinic, Guid RoomId) : IDomainEvent;
