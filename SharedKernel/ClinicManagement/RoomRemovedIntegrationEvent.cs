namespace SharedKernel.ClinicManagement;

public record RoomRemovedIntegrationEvent(Guid ClinicId, Guid RoomId) : IIntegrationEvent;
