namespace SharedKernel.AppointmentReservation;

public record AppointmentCanceledIntegrationEvent(Guid AppointmentId) : IIntegrationEvent;
