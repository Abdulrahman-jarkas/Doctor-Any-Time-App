using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.RoomAggregate;
using ErrorOr;
using MediatR;

namespace ClinicManagement.Application.Commands.AddRoom;

public class AddRoomCommandHandler(IClinicRepository clinicRepository) : IRequestHandler<AddRoomCommand, ErrorOr<Success>>
{
	public async Task<ErrorOr<Success>> Handle(AddRoomCommand request, CancellationToken cancellationToken)
	{

		var clinic = await clinicRepository.Get(request.ClinicId);

		if (clinic == null)
			return Error.NotFound("Clinic", "Clinic was not found");

		var res = clinic.AddRoom(new Room(clinic.Id, request.ServiceIds));

		if (res.IsError)
			return res;

		await clinicRepository.UpdateAsync(clinic);

		return Result.Success;
	}
}
