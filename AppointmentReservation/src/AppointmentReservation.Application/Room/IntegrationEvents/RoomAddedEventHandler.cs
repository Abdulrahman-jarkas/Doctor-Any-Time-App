using AppointmentReservation.Domain.Common.Entities;
using AppointmentReservation.Infrastructure.Persistence.Repositories;
using MediatR;
using SharedKernel.ClinicManagement;

namespace AppointmentReservation.Application.Room.IntegrationEvents;

public class RoomAddedEventHandler(IRoomRepository roomRepository) : INotificationHandler<RoomAddedIntegrationEvent>
{
	public async Task Handle(RoomAddedIntegrationEvent notification, CancellationToken cancellationToken)
	{
		var room = new Domain.RoomAggregate.Room(
			notification.ClinicId,
			Schedule.Empty(),
			notification.ServiceIds,
			notification.RoomId);

		await roomRepository.AddAsync(room);
	}
}