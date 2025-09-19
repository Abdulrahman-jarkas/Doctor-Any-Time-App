using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.RoomAggregate;
using ErrorOr;
using MediatR;

namespace ClinicManagement.Application.Commands.RemoveRoom;

public class RemoveRoomCommandHandler(
	IClinicRepository clinicRepository,
	IAppointmentRepository appointmentRepository
	) : IRequestHandler<RemoveRoomCommand, ErrorOr<Success>>
{
	public async Task<ErrorOr<Success>> Handle(RemoveRoomCommand request, CancellationToken cancellationToken)
	{
		var clinic = await clinicRepository.Get(request.ClinicId);

		if (clinic == null)
			return Error.NotFound("Clinic", "Clinic was not found");

		var isThereScheduledAppointments = await appointmentRepository.IsThereAnyAppointmentsAsync(request.ClinicId, null, request.RoomId);

		if (isThereScheduledAppointments)
			return RoomErrors.CannotDeleteRoomWithPendingAppointments;

		var res = clinic.RemoveRoom(request.RoomId);

		if (res.IsError)
			return res;

		await clinicRepository.UpdateAsync(clinic);

		return Result.Success;
	}
}
