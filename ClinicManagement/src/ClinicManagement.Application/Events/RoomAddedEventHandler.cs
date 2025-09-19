using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.ClinicAggregate.Events;
using MediatR;

namespace ClinicManagement.Application.Events;

public class RoomAddedEventHandler(IRoomRepository roomRepository) : INotificationHandler<RoomAddedEvent>
{
	public async Task Handle(RoomAddedEvent notification, CancellationToken cancellationToken)
	{
		await roomRepository.AddAsync(notification.Room);
	}
}
