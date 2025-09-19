using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.ClinicCenterAggregate.Events;
using ClinicManagement.Domain.Common.EventualConsistency;
using MediatR;

namespace ClinicManagement.Application.Events;

public class ClinicRemovedEventHandler(IClinicRepository clinicRepository) : INotificationHandler<ClinicRemovedEvent>
{
	public async Task Handle(ClinicRemovedEvent notification, CancellationToken cancellationToken)
	{
		var clinic = await clinicRepository.Get(notification.ClinicId);

		if(clinic == null)
			throw new EventualConsistencyException(ClinicRemovedEvent.ClinicNotFound);

		var doctorIds = clinic.DoctorIds.ToArray();
		var roomIds = clinic.RoomIds.ToArray();

		foreach (var doctorId in doctorIds)
		{
			var removeRes = clinic.RemoveDoctor(doctorId);

			if (removeRes.IsError)
				throw new EventualConsistencyException(ClinicRemovedEvent.FailedToDeleteDoctor);
		}

		foreach (var roomId in roomIds)
		{
			var removeRes = clinic.RemoveRoom(roomId);

			if (removeRes.IsError)
				throw new EventualConsistencyException(ClinicRemovedEvent.FailedToDeleteDoctor);
		}

		await clinicRepository.DeleteAsync(clinic);
	}
}
