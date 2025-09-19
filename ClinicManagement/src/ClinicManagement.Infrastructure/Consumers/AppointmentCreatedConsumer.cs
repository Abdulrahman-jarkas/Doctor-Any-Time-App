using MassTransit;
using MediatR;
using SharedKernel.AppointmentReservation;

namespace ClinicManagement.Infrastructure.Consumers;

public class AppointmentCreatedConsumer(IPublisher publisher) : IConsumer<AppointmentCreatedIntegrationEvent>
{
	public async Task Consume(ConsumeContext<AppointmentCreatedIntegrationEvent> context)
	{
		await publisher.Publish(context.Message);
	}
}