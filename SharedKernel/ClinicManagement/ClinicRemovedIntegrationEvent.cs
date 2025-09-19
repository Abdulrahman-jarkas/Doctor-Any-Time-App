namespace SharedKernel.ClinicManagement;

public record ClinicRemovedIntegrationEvent(Guid ClinicId) : IIntegrationEvent;
