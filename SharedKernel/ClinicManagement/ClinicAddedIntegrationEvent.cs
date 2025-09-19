namespace SharedKernel.ClinicManagement;

public record ClinicAddedIntegrationEvent(
	Guid ClinicCenterId,
	Guid ClinicId,
	int MaxAppointmentPerDay
	) : IIntegrationEvent;
