using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.ClinicCenterAggregate;
using ClinicManagement.Domain.DoctorAggregate;
using ErrorOr;
using MediatR;

namespace ClinicManagement.Application.Commands.RemoveDoctor;

public class RemoveDoctorCommandHandler(
	IClinicRepository clinicRepository,
	IAppointmentRepository appointmentRepository
	) : IRequestHandler<RemoveDoctorCommand, ErrorOr<Success>>
{
	public async Task<ErrorOr<Success>> Handle(RemoveDoctorCommand request, CancellationToken cancellationToken)
	{
		var clinic = await clinicRepository.Get(request.ClinicId);

		if (clinic == null)
			return Error.NotFound("Clinic", "Clinic was not found");

		var isThereScheduledAppointments = await appointmentRepository.IsThereAnyAppointmentsAsync(request.ClinicId, request.DoctorId);

		if (isThereScheduledAppointments)
			return DoctorErrors.CannotDeleteDoctorWithPendingAppointments;

		var res = clinic.RemoveDoctor(request.DoctorId);

		if (res.IsError)
			return res;

		await clinicRepository.UpdateAsync(clinic);

		return Result.Success;
	}
}
