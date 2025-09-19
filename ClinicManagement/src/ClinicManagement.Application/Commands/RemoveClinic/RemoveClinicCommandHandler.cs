using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.ClinicCenterAggregate;
using ErrorOr;
using MediatR;

namespace ClinicManagement.Application.Commands.RemoveClinic;

public class RemoveClinicCommandHandler(
	IClinicCenterRepository clinicCenterRepository,
	IAppointmentRepository appointmentRepository
	) : IRequestHandler<RemoveClinicCommand, ErrorOr<Success>>
{
	public async Task<ErrorOr<Success>> Handle(RemoveClinicCommand request, CancellationToken cancellationToken)
	{
		var clinicCenter = await clinicCenterRepository.Get(request.ClinicCenterId);

		if (clinicCenter == null)
			return Error.NotFound("ClinicCenter", "Clinic center was not found");

		var isThereScheduledAppointments = await appointmentRepository.IsThereAnyAppointmentsAsync(request.ClinicId);

		if (isThereScheduledAppointments)
			return ClinicCenterErrors.CannotDeleteClinicWithPendingAppointments;

		var addRes = clinicCenter.RemoveClinic(request.ClinicId);

		if (addRes.IsError)
			return addRes;

		await clinicCenterRepository.UpdateAsync(clinicCenter);

		return Result.Success;
	}
}
