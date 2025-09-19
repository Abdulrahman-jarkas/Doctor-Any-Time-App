using MassTransit;
using MediatR;
using SharedKernel.AppointmentReservation;

namespace ClinicManagement.Infrastructure.Consumers;

public class AppointmentCompletedConsumer(IPublisher publisher) : IConsumer<AppointmentCompletedIntegrationEvent>
{
	public async Task Consume(ConsumeContext<AppointmentCompletedIntegrationEvent> context)
	{
		await publisher.Publish(context.Message);
	}
}
