using AppointmentReservation.Infrastructure.Persistence;
using MassTransit;
using MediatR;
using SharedKernel.ClinicManagement;

namespace AppointmentReservation.Infrastructure.Consumers;

public class ClinicRemovedConsumer(IPublisher publisher) : IConsumer<ClinicRemovedIntegrationEvent>
{
	public async Task Consume(ConsumeContext<ClinicRemovedIntegrationEvent> context)
	{
			await publisher.Publish(context.Message);
	}
}
