using AppointmentReservation.Domain.AvailabilityAggregate;
using AppointmentReservation.Infrastructure.Persistence.Repositories;
using ErrorOr;
using MediatR;

namespace AppointmentReservation.Application.Clinic.Commands.CancelPublishedAppointment;

public class CancelAvailabilityCommandHandler(IAvailabilityRepository availabilityRepository)
	: IRequestHandler<CancelAvailabilityCommand, ErrorOr<Success>>
{
	public async Task<ErrorOr<Success>> Handle(CancelAvailabilityCommand request, CancellationToken cancellationToken)
	{
		var availability = await availabilityRepository.Get(request.AvailabilityId);

		if (availability == null || availability.ClinicId != request.ClinicId)
			return AvailabilityErrors.AvailabilityNotFound;

		if (availability.Status == AvailabilityStatus.Invalid)
			return AvailabilityErrors.AvailabilityNotValidForNow;

		var cancelRes = availability.Cancel();

		if (cancelRes.IsError)
			return cancelRes;

		await availabilityRepository.DeleteAsync(availability);

		return Result.Success;
	}
}