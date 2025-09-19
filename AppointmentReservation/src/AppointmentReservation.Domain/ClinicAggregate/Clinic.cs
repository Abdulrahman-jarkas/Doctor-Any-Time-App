using AppointmentReservation.Core.Common;
using ErrorOr;

namespace AppointmentReservation.Domain.ClinicAggregate;

public class Clinic : AggregateRoot
{
	public Guid ClinicCenterId { get; init; }
	public int MaxAppointmentsPerDay { get; private set; }

	public Clinic(Guid clinicCenterId, int maxAppointmentsPerDay, Guid? id = null)
		: base(id ?? Guid.NewGuid())
	{
		ClinicCenterId = clinicCenterId;
		MaxAppointmentsPerDay = maxAppointmentsPerDay;
	}

	public ErrorOr<Success> SetMaxAppointmentPerDay(int value)
	{
		if (value <= 0)
			return ClinicErrors.InvalidMaxAppointmentPerDay;

		MaxAppointmentsPerDay = value;

		return Result.Success;
	}

	// For EF Core
	private Clinic()
	{
	}
}

