using AppointmentReservation.Domain.ClinicAggregate;
using AppointmentReservation.Domain.Common.ValueObjects;
using RoomEntity = AppointmentReservation.Domain.RoomAggregate.Room;
using ErrorOr;

namespace AppointmentReservation.Application.Common.Extensions;

public static class ClinicRoomsExtensions
{
	public static bool CanAddMoreAppointments(
		this List<RoomEntity> rooms,
		DateOnly date,
		int maxAppointmentsPerDay)
	{
		return rooms.Sum(room => room.GetScheduledAppointmentCountPerDay(date)) < maxAppointmentsPerDay;
	}

	public static bool CanReserveRoom(
		this List<RoomEntity> rooms,
		List<Guid> serviceIds,
		DateOnly date,
		TimeRange time)
	{
		return rooms.Any(room => room.CanReserveRoom(serviceIds, date, time));
	}

	public static bool CanProccessAppointment(
		this List<RoomEntity> rooms,
		int maxAppointmentsPerDay,
		List<Guid> serviceIds,
		DateOnly date,
		TimeRange time,
		out List<Error> errors)
	{
		if (!rooms.CanAddMoreAppointments(date, maxAppointmentsPerDay))
		{
			errors = new List<Error> { ClinicErrors.CannotHaveMoreSessionThanSubscriptionAllows };
			return false;
		}

		if (!rooms.CanReserveRoom(serviceIds, date, time))
		{
			errors = new() { ClinicErrors.ThereAreNoRoomsToProvideThisServicesAtThisTime };
			return false;
		}

		errors = new();
		return true;
	}
}
