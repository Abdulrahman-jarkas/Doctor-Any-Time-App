using MassTransit;
using MediatR;
using SharedKernel.AppointmentReservation;

namespace ClinicManagement.Infrastructure.Consumers;

public class AppointmentCanceledConsumer(IPublisher publisher) : IConsumer<AppointmentCanceledIntegrationEvent>
{
	public async Task Consume(ConsumeContext<AppointmentCanceledIntegrationEvent> context)
	{
		await publisher.Publish(context.Message);
	}
}
