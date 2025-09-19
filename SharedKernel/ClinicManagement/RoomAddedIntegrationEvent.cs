namespace SharedKernel.ClinicManagement;

public record RoomAddedIntegrationEvent(
	Guid ClinicId,
	Guid RoomId,
	List<Guid> ServiceIds
	) : IIntegrationEvent;
