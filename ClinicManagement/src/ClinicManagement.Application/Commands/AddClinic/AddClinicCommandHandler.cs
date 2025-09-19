using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.ClinicAggregate;
using ErrorOr;
using MediatR;

namespace ClinicManagement.Application.Commands.AddClinic;

public class AddClinicCommandHandler(IClinicCenterRepository clinicCenterRepository) : IRequestHandler<AddClinicCommand, ErrorOr<Success>>
{
	public async Task<ErrorOr<Success>> Handle(AddClinicCommand request, CancellationToken cancellationToken)
	{
		var clinicCenter = await clinicCenterRepository.Get(request.ClinicCenterId);

		if (clinicCenter == null)
			return Error.NotFound("ClinicCenter", "Clinic center was not found");

		var addRes = clinicCenter.AddClinic(new Clinic(clinicCenter.Id));

		if (addRes.IsError)
			return addRes;

		await clinicCenterRepository.UpdateAsync(clinicCenter);

		return Result.Success;
	}
}
