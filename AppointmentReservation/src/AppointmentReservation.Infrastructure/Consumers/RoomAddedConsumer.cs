using MassTransit;
using MediatR;
using SharedKernel.ClinicManagement;

namespace AppointmentReservation.Infrastructure.Consumers;

public class RoomAddedConsumer(IPublisher publisher) : IConsumer<RoomAddedIntegrationEvent>
{
	public async Task Consume(ConsumeContext<RoomAddedIntegrationEvent> context)
	{
			await publisher.Publish(context.Message);
	}
}
