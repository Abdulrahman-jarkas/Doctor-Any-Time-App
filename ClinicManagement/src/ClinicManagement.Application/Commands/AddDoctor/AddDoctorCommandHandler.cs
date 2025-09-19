using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.DoctorAggregate;
using ErrorOr;
using MediatR;

namespace ClinicManagement.Application.Commands.AddDoctor;

public class AddDoctorCommandHandler(IClinicRepository clinicRepository) : IRequestHandler<AddDoctorCommand, ErrorOr<Success>>
{
	public async Task<ErrorOr<Success>> Handle(AddDoctorCommand request, CancellationToken cancellationToken)
	{
		var clinic = await clinicRepository.Get(request.ClinicId);

		if (clinic == null)
			return Error.NotFound("Clinic", "Clinic was not found");

		var res = clinic.AddDoctor(new Doctor(clinic.Id, request.ServiceIds));

		if (res.IsError)
			return res;

		await clinicRepository.UpdateAsync(clinic);

		return Result.Success;
	}
}
