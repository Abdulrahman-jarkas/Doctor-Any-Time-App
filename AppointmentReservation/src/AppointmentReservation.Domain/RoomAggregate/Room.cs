using AppointmentReservation.Core.Common;
using AppointmentReservation.Domain.Common.Entities;
using AppointmentReservation.Domain.Common.ValueObjects;
using ErrorOr;

namespace AppointmentReservation.Domain.RoomAggregate;

public class Room : Entity
{
	private readonly List<Guid> _serviceIds = [];

	public Schedule Schedule { get; private set; } = Schedule.Empty();

	public Guid ClinicId { get; init; }

	public Room(
		Guid clinicId,
		Schedule schedule,
		List<Guid> serviceIds, Guid? id = null) : base(id ?? Guid.NewGuid())
	{
		ClinicId = clinicId;
		Schedule = schedule ?? Schedule.Empty();
		_serviceIds = serviceIds.ToList();
	}

	public bool CanProvideServices(List<Guid> serviceIds)
	{
		return serviceIds.All(s => _serviceIds.Contains(s));
	}

	public bool IsAvailableAt(DateOnly date, TimeRange time)
	{
		return Schedule.CanBookTimeSlot(date, time);
	}

	public bool CanReserveRoom(List<Guid> serviceIds, DateOnly date, TimeRange time)
	{
		return CanProvideServices(serviceIds) && IsAvailableAt(date, time);
	}

	public int GetScheduledAppointmentCountPerDay(DateOnly date)
	{
		return Schedule.GetScheduledItemsCount(date);
	}

	public ErrorOr<Success> AddToSchedule(DateOnly date, TimeRange timeRange)
	{

		var res = Schedule.BookTimeSlot(date, timeRange);

		if (res.IsError & res.FirstError.Type == ErrorType.Conflict)
			return RoomErrors.CannotHaveTwoOrMoreOverlappingAppointments;

		return res;
	}

	public ErrorOr<Success> RemoveFromSchedule(DateOnly date, TimeRange timeRange)
	{
		return Schedule.RemoveBooking(date, timeRange);
	}

	// For EF Core
	private Room()
	{
	}
}
