using MassTransit;
using MediatR;
using SharedKernel.ClinicManagement;

namespace AppointmentReservation.Infrastructure.Consumers;

public class SubscriptionChangedConsumer(IPublisher publisher) : IConsumer<SubscriptionChangedIntegrationEvent>
{
	public async Task Consume(ConsumeContext<SubscriptionChangedIntegrationEvent> context)
	{
			await publisher.Publish(context.Message);
	}
}