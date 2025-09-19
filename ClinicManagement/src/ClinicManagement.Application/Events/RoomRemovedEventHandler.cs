using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.ClinicAggregate.Events;
using ClinicManagement.Domain.Common.EventualConsistency;
using MediatR;

namespace ClinicManagement.Application.Events;

public class RoomRemovedEventHandler(IRoomRepository roomRepository) : INotificationHandler<RoomRemovedEvent>
{
	public async Task Handle(RoomRemovedEvent notification, CancellationToken cancellationToken)
	{
		var room = await roomRepository.Get(notification.RoomId);

		if(room == null)
			throw new EventualConsistencyException(RoomRemovedEvent.RoomNotFound);

		await roomRepository.DeleteAsync(room);
	}
}