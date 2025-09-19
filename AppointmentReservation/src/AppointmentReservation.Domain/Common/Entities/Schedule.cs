using AppointmentReservation.Domain.Common.ValueObjects;

using ErrorOr;

namespace AppointmentReservation.Domain.Common.Entities;

public class Schedule
{
	public Dictionary<DateOnly, List<TimeRange>> Calender { get; private set; } = new();

	public Schedule(
		Dictionary<DateOnly, List<TimeRange>>? calendar = null)
	{
		Calender = calendar ?? new();
	}

	public static Schedule Empty()
	{
		return new Schedule();
	}

	internal bool CanBookTimeSlot(DateOnly date, TimeRange time)
	{
		if (!Calender.TryGetValue(date, out var timeSlots))
		{
			return true;
		}

		return !timeSlots.Any(timeSlot => timeSlot.OverlapsWith(time));
	}

	internal ErrorOr<Success> BookTimeSlot(DateOnly date, TimeRange time)
	{
		if (!Calender.TryGetValue(date, out var timeSlots))
		{
			Calender[date] = new() { time };
			return Result.Success;
		}

		if (!CanBookTimeSlot(date, time))
		{
			return Error.Conflict();
		}

		timeSlots.Add(time);
		return Result.Success;
	}

	internal ErrorOr<Success> RemoveBooking(DateOnly date, TimeRange time)
	{
		if (!Calender.TryGetValue(date, out var timeSlots) || !timeSlots.Contains(time))
		{
			return Error.NotFound(description: "Booking not found");
		}

		if (!timeSlots.Remove(time))
		{
			return Error.Unexpected();
		}

		return Result.Success;
	}

	internal int GetScheduledItemsCount(DateOnly date)
	{
		return Calender.TryGetValue(date, out var timeSltos) ? timeSltos.Count : 0;
	}

	public Schedule() { }
}