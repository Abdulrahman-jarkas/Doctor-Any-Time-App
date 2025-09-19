namespace SharedKernel.ClinicManagement;

public record DoctorRemovedIntegrationEvent(Guid ClinicId, Guid DoctorId) : IIntegrationEvent;
