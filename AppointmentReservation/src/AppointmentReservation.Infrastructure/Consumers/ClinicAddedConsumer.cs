using AppointmentReservation.Infrastructure.Persistence;
using MassTransit;
using MediatR;
using SharedKernel.ClinicManagement;

namespace AppointmentReservation.Infrastructure.Consumers;

public class ClinicAddedConsumer(IPublisher publisher, AppointmentsReservationDbContext dbContext) : IConsumer<ClinicAddedIntegrationEvent>
{
	public async Task Consume(ConsumeContext<ClinicAddedIntegrationEvent> context)
	{
		//var transaction = await dbContext.Database.BeginTransactionAsync();
		//try
		//{
			await publisher.Publish(context.Message);
		//	await transaction.CommitAsync();
		//}
		//catch
		//{
		//}
		//finally
		//{
		//	await transaction.DisposeAsync();
		//}
	}
}
