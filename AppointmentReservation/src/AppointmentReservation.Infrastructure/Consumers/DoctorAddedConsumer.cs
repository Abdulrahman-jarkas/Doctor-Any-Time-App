using MassTransit;
using MediatR;
using SharedKernel.ClinicManagement;

namespace AppointmentReservation.Infrastructure.Consumers;

public class DoctorAddedConsumer(IPublisher publisher) : IConsumer<DoctorAddedIntegrationEvent>
{
	public async Task Consume(ConsumeContext<DoctorAddedIntegrationEvent> context)
	{
			await publisher.Publish(context.Message);
	}
}
