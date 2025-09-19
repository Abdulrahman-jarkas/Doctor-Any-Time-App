using AppointmentReservation.Infrastructure.Persistence;
using MassTransit;
using MediatR;
using SharedKernel.ClinicManagement;

namespace AppointmentReservation.Infrastructure.Consumers;

public class RoomRemovedConsumer(IPublisher publisher) : IConsumer<RoomRemovedIntegrationEvent>
{
	public async Task Consume(ConsumeContext<RoomRemovedIntegrationEvent> context)
	{
			await publisher.Publish(context.Message);
	}
}
