using ClinicManagement.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace ClinicManagement.Application.Commands.ChangeSubscription;

public class ChangeSubscriptionCommandHandler(IClinicCenterRepository clinicCenterRepository) : IRequestHandler<ChangeSubscriptionCommand, ErrorOr<Success>>
{
	public async Task<ErrorOr<Success>> Handle(ChangeSubscriptionCommand request, CancellationToken cancellationToken)
	{
		var clinicCenter = await clinicCenterRepository.Get(request.ClinicCenterId);

		if (clinicCenter == null)
			return Error.NotFound("ClinicCenter", "Clinic center was not found");

		clinicCenter.ChangeSubscription(request.SubscriptionType);

		await clinicCenterRepository.UpdateAsync(clinicCenter);

		return Result.Success;
	}
}
