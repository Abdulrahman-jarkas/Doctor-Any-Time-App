using AppointmentReservation.Core.Common;
using AppointmentReservation.Domain.Common.Entities;
using AppointmentReservation.Domain.Common.ValueObjects;
using AppointmentReservation.Domain.DoctorAggregate;
using ErrorOr;

namespace AppointmentReservation.Domain.PatientAggregate;

public class Patient : AggregateRoot
{
	public Schedule Schedule { get; private set; } = Schedule.Empty();

	public Patient(Schedule schedule, Guid? id = null) : base(id ?? Guid.NewGuid())
	{
		Schedule = schedule ?? Schedule.Empty();
	}

	public bool IsAvailableAt(DateOnly date, TimeRange timeRange)
	{
		return Schedule.CanBookTimeSlot(date, timeRange);
	}

	public ErrorOr<Success> AddToSchedule(DateOnly date, TimeRange timeRange)
	{

		var res = Schedule.BookTimeSlot(date, timeRange);

		if (res.IsError & res.FirstError.Type == ErrorType.Conflict)
			return DoctorErrors.CannotHaveTwoOrMoreOverlappingAppointments;

		return res;
	}


	public ErrorOr<Success> RemoveFromSchedule(DateOnly date, TimeRange timeRange)
	{
		return Schedule.RemoveBooking(date, timeRange);
	}


	// For Ef Core
	private Patient()
	{
	}
}