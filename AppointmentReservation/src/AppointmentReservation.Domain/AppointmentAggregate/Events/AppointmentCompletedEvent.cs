using AppointmentReservation.Domain.Common;

namespace AppointmentReservation.Domain.AppointmentAggregate.Events;

public record AppointmentCompletedEvent(Appointment Appointment) : IDomainEvent;