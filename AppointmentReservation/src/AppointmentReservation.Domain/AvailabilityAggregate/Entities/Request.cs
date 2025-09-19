using AppointmentReservation.Core.Common;
using ErrorOr;

namespace AppointmentReservation.Domain.AvailabilityAggregate;

public class Request : Entity
{
	public Guid PatientId { get; init; }

	public RequestStatus Status { get; private set; }

	public Availability Availability { get; private set; }

	public Request(Guid patientId)
	{
		PatientId = patientId;
		Status = RequestStatus.Pending;
	}

	public ErrorOr<Success> Cancel()
	{
		if (Status == RequestStatus.Accepted)
			return AvailabilityErrors.CannotCancelRequest;

		Status = RequestStatus.Cancelled;
		return Result.Success;
	}

	public ErrorOr<Success> Accept()
	{
		if (Status == RequestStatus.Cancelled)
			return AvailabilityErrors.CannotAcceptRequest;

		Status = RequestStatus.Accepted;
		return Result.Success;
	}

	public ErrorOr<Success> Confirm()
	{
		if (Status == RequestStatus.Accepted)
			return AvailabilityErrors.CannotAcceptRequest;

		Status = RequestStatus.Confirmed;
		return Result.Success;
	}

	// For Ef Core
	private Request()
	{
	}
}