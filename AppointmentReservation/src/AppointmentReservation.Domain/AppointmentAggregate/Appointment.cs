using AppointmentReservation.Core.Common;
using AppointmentReservation.Domain.AppointmentAggregate.Events;
using AppointmentReservation.Domain.Common.ValueObjects;
using ErrorOr;

namespace AppointmentReservation.Domain.AppointmentAggregate;

public class Appointment : AggregateRoot
{
	public AppointmentStatus Status { get; private set; }
	public DateOnly Date { get; private set; }

	public TimeRange Time { get; private set; } = null!;

	public List<Guid> ServiceIds { get; private set; } = new();

	public Guid ClinicId { get; init; }

	public Guid RoomId { get; private set; }

	public Guid DoctorId { get; private set; }

	public Guid PatientId { get; private set; }

	public Appointment(
		Guid clinicId,
		Guid doctorId,
		Guid roomId,
		Guid patientId,
		List<Guid> serviceIds,
		DateOnly date,
		TimeRange time)
	{
		Date = date;
		Time = time;
		ServiceIds = serviceIds;
		ClinicId = clinicId;
		DoctorId = doctorId;
		RoomId = roomId;
		PatientId = patientId;
		Status = AppointmentStatus.Scheduled;

		_domainEvents.Add(new AppointmentCreatedEvent(this));
	}


	public ErrorOr<Success> Cancel()
	{
		if (Status != AppointmentStatus.Scheduled)
			return AppointmentErrors.CannotCancelRequest;

		var diff = Date.ToDateTime(Time.Start) - DateTime.UtcNow;

		if (diff.TotalHours < 24)
			return AppointmentErrors.CannotCancelTooCLoseAppointment;

		Status = AppointmentStatus.Cancelled;

		_domainEvents.Add(new AppointmentCanceledEvent(this));

		return Result.Success;
	}

	public ErrorOr<Success> Complete()
	{
		if (Status != AppointmentStatus.Scheduled)
			return AppointmentErrors.CannotCompleteRequest;

		var now = DateTime.UtcNow;
		var appointmentStartDateTime = Date.ToDateTime(Time.Start);

		if (appointmentStartDateTime >= now)
			return AppointmentErrors.InvalidCompleteDateTime;

		Status = AppointmentStatus.Completed;

		_domainEvents.Add(new AppointmentCompletedEvent(this));

		return Result.Success;
	}

	// For EF Core
	private Appointment()
	{
	}
}