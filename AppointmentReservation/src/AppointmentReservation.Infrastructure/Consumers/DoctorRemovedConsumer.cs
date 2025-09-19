using AppointmentReservation.Infrastructure.Persistence;
using MassTransit;
using MediatR;
using SharedKernel.ClinicManagement;

namespace AppointmentReservation.Infrastructure.Consumers;

public class DoctorRemovedConsumer(IPublisher publisher) : IConsumer<DoctorRemovedIntegrationEvent>
{
	public async Task Consume(ConsumeContext<DoctorRemovedIntegrationEvent> context)
	{
		
			await publisher.Publish(context.Message);
	}
}
