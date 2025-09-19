using AppointmentReservation.Core.Common;
using AppointmentReservation.Domain.Common.Entities;
using AppointmentReservation.Domain.Common.ValueObjects;
using ErrorOr;

namespace AppointmentReservation.Domain.DoctorAggregate;

public class Doctor : AggregateRoot
{
	private List<Guid> _serviceIds = [];

	public Schedule Schedule { get; private set; } = Schedule.Empty();

	public Guid ClinicId { get; init; }

	public Doctor(
		Schedule schedule,
		List<Guid> serviceIds,
		Guid clinicId,
		Guid? id = null
		) : base(id ?? Guid.NewGuid())
	{
		Schedule = schedule ?? Schedule.Empty();
		_serviceIds = serviceIds;
		ClinicId = clinicId;
	}

	public bool CanProvideServices(List<Guid> serviceIds)
	{
		foreach (var serviceId in serviceIds)
		{
			if (_serviceIds.Contains(serviceId))
				return true;
		}

		return false;
	}

	public bool IsAvailableAt(DateOnly date, TimeRange timeRange)
	{
		return Schedule.CanBookTimeSlot(date, timeRange);
	}

	public bool CanProccessAppointment(List<Guid> serviceIds, DateOnly date, TimeRange time, out List<Error> errors)
	{
		errors = new();
		if (!CanProvideServices(serviceIds))
			errors.Add(DoctorErrors.CannotProvideServices);
		

		if (!IsAvailableAt(date, time))
			errors.Add(DoctorErrors.CannotHaveTwoOrMoreOverlappingAppointments);

		if (errors.Count > 0)
			return false;

		return true;
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

	// For EF Core
	private Doctor()
	{
	}
}
