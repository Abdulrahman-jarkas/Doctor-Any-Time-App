using AppointmentReservation.Core.Common;
using AppointmentReservation.Domain.AvailabilityAggregate.Events;
using AppointmentReservation.Domain.Common.ValueObjects;
using ErrorOr;
using MediatR;

namespace AppointmentReservation.Domain.AvailabilityAggregate;

public class Availability : AggregateRoot
{
	private readonly List<Guid> _serviceIds = new();
	public IReadOnlyList<Guid> ServiceIds => _serviceIds.AsReadOnly();

	private readonly List<Request> _requests = new();
	public IReadOnlyList<Request> Requests => _requests.AsReadOnly();

	public DateOnly Date { get; private set; }

	public TimeRange Time { get; private set; } = null!;

	public Guid ClinicId { get; init; }

	public Guid DoctorId { get; init; }

	public AvailabilityStatus Status { get; private set; }

	public Availability(
		Guid clinicId,
		Guid doctorId,
		List<Guid> serviceIds,
		DateOnly date,
		TimeRange time,
		Guid? id = null) : base(id ?? Guid.NewGuid())
	{
		ClinicId = clinicId;
		DoctorId = doctorId;
		_serviceIds = serviceIds;
		Date = date;
		Time = time;
		Status = AvailabilityStatus.Valid;

		_domainEvents.Add(new AvailabilityPublishedEvent(this));
	}

	public ErrorOr<Success> MarkAsInvalid()
	{
		Status = AvailabilityStatus.Invalid;

		foreach (var req in _requests)
		{
			var cancelRes = CancelRequest(req.Id);

			if (cancelRes.IsError)
				return cancelRes;
		}

		return Result.Success;
	}

	public ErrorOr<Success> MarkAsValid()
	{
		Status = AvailabilityStatus.Valid;

		return Result.Success;
	}

	public ErrorOr<Success> Request(Guid patientId)
	{
		var request = new Request(patientId);

		if (_requests.Any(r => r.PatientId == patientId))
			return AvailabilityErrors.PatientCannotRequestAppointmentTwice;

		_requests.Add(request);

		_domainEvents.Add(new AvailabilityRequestedEvent(this, request));

		return Result.Success;

	}

	public ErrorOr<Success> CancelRequest(Guid requestId)
	{
		var request = _requests.FirstOrDefault(r => r.Id == requestId);

		if (request == null)
			return AvailabilityErrors.PatientRequestWasNotFound;

		request.Cancel();

		_domainEvents.Add(new AvailabilityRequestCanceledEvent(this, request));

		return Result.Success;
	}

	public ErrorOr<Success> AcceptRequest(Guid requestId)
	{
		var request = _requests.FirstOrDefault(r => r.Id == requestId);

		if (request == null)
			return AvailabilityErrors.PatientRequestWasNotFound;

		var acceptRes = request.Accept();

		//if (acceptRes.IsError)
		//	return acceptRes;

		//foreach (var req in _requests.Where(r => r.Id != request.Id))
		//{
		//	var cancelRes = CancelRequest(req.Id);

		//	if (cancelRes.IsError)
		//		return cancelRes;
		//}

		_domainEvents.Add(new AvailabiltyRequestAcceptedEvent(this, requestId));

		return Result.Success;
	}

	public ErrorOr<Success> ConfirmRequest()
	{
		var request = _requests.SingleOrDefault(r => r.Status == RequestStatus.Accepted);

		if (request == null)
			return AvailabilityErrors.PatientRequestWasNotFound;

		var confirmRes = request.Confirm();

		if (confirmRes.IsError)
			return confirmRes;

		foreach (var req in _requests.Where(r => r.Id != request.Id))
		{
			var cancelRes = CancelRequest(req.Id);

			if (cancelRes.IsError)
				return cancelRes;
		}

		_domainEvents.Add(new AvailabiltyRequestConfirmedEvent(this, request.Id));

		return Result.Success;
	}

	public ErrorOr<Success> Cancel()
	{
		foreach(var req in _requests)
		{
			var cancelRes = CancelRequest(req.Id);

			if (cancelRes.IsError)
				return cancelRes;
		}

		_domainEvents.Add(new AvailabilityCanceledEvent(this));

		return Result.Success;
	}

	// For EF core 
	private Availability()
	{	
	}
}
