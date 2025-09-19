namespace AppointmentReservation.Contracts.Clinic;

public class PublishAvailabilityRequest
{
	public Guid DoctorId { get; set; }
	public List<Guid> ServiceIds { get; set; } = [];
	public DateOnly AppointmentDate { get; set; }
	public TimeOnly StartTime { get; set; }
	public TimeOnly EndTime { get; set; }
}

public class AppointmentResponse
{
	public Guid DoctorId { get; set; }
	public List<Guid> ServiceIds { get; set; } = [];
	public DateOnly AppointmentDate { get; set; }
	public TimeOnly StartTime { get; set; }
	public TimeOnly EndTime { get; set; }
}