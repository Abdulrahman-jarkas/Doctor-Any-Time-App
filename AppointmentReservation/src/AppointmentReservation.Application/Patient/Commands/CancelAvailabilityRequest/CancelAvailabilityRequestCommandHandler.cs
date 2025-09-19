using AppointmentReservation.Infrastructure.Persistence.Repositories;
using ErrorOr;
using MediatR;

namespace AppointmentReservation.Application.Patient.Commands.CancelAvailabilityRequest;

public class CancelAvailabilityRequestCommandHandler : IRequestHandler<CancelAvailabilityRequestCommand, ErrorOr<Success>>
{
	private readonly IAvailabilityRepository _availabilityRepository;

	public CancelAvailabilityRequestCommandHandler(IAvailabilityRepository availabilityRepository)
	{
		_availabilityRepository = availabilityRepository;
	}

	public async Task<ErrorOr<Success>> Handle(CancelAvailabilityRequestCommand request, CancellationToken cancellationToken)
	{
		var availability = await _availabilityRepository.Get(request.AvailabilityId);

		if (availability == null)
			return Error.NotFound("Availability.NotFound", "Availability not found");

		var cancelReqResult = availability.CancelRequest(request.RequestId);

		if (cancelReqResult.IsError)
			return cancelReqResult.Errors;

		await _availabilityRepository.UpdateAsync(availability);

		return Result.Success;
	}
}