namespace SharedKernel.ClinicManagement;

public record DoctorAddedIntegrationEvent(
	Guid ClinicId,
	Guid DoctorId,
	List<Guid> ServiceIds) : IIntegrationEvent;
