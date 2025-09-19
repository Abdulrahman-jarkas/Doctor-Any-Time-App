namespace SharedKernel.AppointmentReservation;

public record AppointmentCreatedIntegrationEvent(
	Guid AppointmentId,
	Guid ClinicId,
	Guid RoomId,
	Guid DoctorId) : IIntegrationEvent;