using AppointmentReservation.Domain.AppointmentAggregate.Events;
using AppointmentReservation.Infrastructure.Persistence;
using MassTransit;
using MediatR;
using SharedKernel;
using SharedKernel.AppointmentReservation;
using SharedKernel.ClinicManagement;
using System.Text.Json;

namespace AppointmentReservation.Infrastructure.IntegrationEvents;

public class OutboxIntegrationEventHandler(AppointmentsReservationDbContext context, IPublishEndpoint publishEndpoint) :
	  INotificationHandler<AppointmentCreatedEvent>,
	  INotificationHandler<AppointmentCanceledEvent>,
	  INotificationHandler<AppointmentCompletedEvent>
{
	public async Task Handle(AppointmentCreatedEvent notification, CancellationToken cancellationToken)
	{
		var integrationEvent = new AppointmentCreatedIntegrationEvent(
			AppointmentId: notification.Appointment.Id,
			ClinicId: notification.Appointment.ClinicId,
			RoomId: notification.Appointment.RoomId,
			DoctorId: notification.Appointment.DoctorId);

		await publishEndpoint.Publish(integrationEvent);

		await context.SaveChangesAsync();
	}

	public async Task Handle(AppointmentCanceledEvent notification, CancellationToken cancellationToken)
	{
		var integrationEvent = new AppointmentCanceledIntegrationEvent(notification.Appointment.Id);

		await publishEndpoint.Publish(integrationEvent);

		await context.SaveChangesAsync();
	}

	public async Task Handle(AppointmentCompletedEvent notification, CancellationToken cancellationToken)
	{
		var integrationEvent = new AppointmentCompletedIntegrationEvent(notification.Appointment.Id);

		await publishEndpoint.Publish(integrationEvent);

		await context.SaveChangesAsync();
	}

	//private async Task AddOutboxIntegrationEventAsync(IIntegrationEvent integrationEvent)
	//{
	//	Console.WriteLine(JsonSerializer.Serialize(integrationEvent));
	//	await context.OutboxIntegrationEvents.AddAsync(new OutboxIntegrationEvent(
	//		EventName: integrationEvent.GetType().Name,
	//		EventContent: JsonSerializer.Serialize(integrationEvent)));

	//	await context.SaveChangesAsync();
	//}
}