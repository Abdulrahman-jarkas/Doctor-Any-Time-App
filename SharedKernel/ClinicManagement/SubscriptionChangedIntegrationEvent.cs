namespace SharedKernel.ClinicManagement;

public record SubscriptionChangedIntegrationEvent(Guid ClinicCenterId, int MaxAppointmentPerDay) : IIntegrationEvent;
