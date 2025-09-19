using ClinicManagement.Core.Common;

namespace ClinicManagement.Domain.Common.Entities;

public class Appointment : Entity
{
	public Guid ClinicId { get; init; }
	public Guid DoctorId { get; set; }
	public Guid RoomId { get; set; }

	public Appointment(Guid id, Guid clinicId, Guid doctorId, Guid roomId) : base(id) {
		ClinicId = clinicId;
		DoctorId = doctorId;
		RoomId = roomId;
	}

	// for ef core
	private Appointment()
	{
	}
}
