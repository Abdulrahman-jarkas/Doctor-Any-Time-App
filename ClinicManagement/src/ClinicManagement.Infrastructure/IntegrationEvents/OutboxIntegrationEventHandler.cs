using ClinicManagement.Domain.ClinicAggregate.Events;
using ClinicManagement.Domain.ClinicCenterAggregate.Events;
using ClinicManagement.Infrastructure.Persistence;
using MassTransit;
using MediatR;
using SharedKernel;
using SharedKernel.ClinicManagement;
using System.Text.Json;

namespace ClinicManagement.Infrastructure.IntegrationEvents;

public class OutboxIntegrationEventHandler(ClinicManagementDbContext context, IPublishEndpoint publishEndpoint) :
	  INotificationHandler<ClinicAddedEvent>,
	  INotificationHandler<ClinicRemovedEvent>,
	  INotificationHandler<RoomAddedEvent>,
	  INotificationHandler<RoomRemovedEvent>,
	  INotificationHandler<DoctorAddedEvent>,
	  INotificationHandler<DoctorRemovedEvent>,
	  INotificationHandler<SubscriptionChangedEvent>
{
	public async Task Handle(ClinicAddedEvent notification, CancellationToken cancellationToken)
	{
		var res = new ClinicAddedIntegrationEvent(notification.Clinic.ClinicCenterId, notification.Clinic.Id, notification.MaxAppointmentPerDay);

		await publishEndpoint.Publish(res);

		await context.SaveChangesAsync();
	}

	public async Task Handle(ClinicRemovedEvent notification, CancellationToken cancellationToken)
	{
		var res = new ClinicRemovedIntegrationEvent(notification.ClinicId);

		await publishEndpoint.Publish(res);

		await context.SaveChangesAsync();
	}

	public async Task Handle(RoomAddedEvent notification, CancellationToken cancellationToken)
	{
		var res = new RoomAddedIntegrationEvent(notification.Room.ClinicId, notification.Room.Id, notification.Room.ServiceIds.ToList());

		await publishEndpoint.Publish(res);

		await context.SaveChangesAsync();
	}

	public async Task Handle(RoomRemovedEvent notification, CancellationToken cancellationToken)
	{
		var res = new RoomRemovedIntegrationEvent(notification.ClinicId, notification.RoomId);

		await publishEndpoint.Publish(res);

		await context.SaveChangesAsync();
	}

	public async Task Handle(DoctorAddedEvent notification, CancellationToken cancellationToken)
	{
		var res = new DoctorAddedIntegrationEvent(notification.doctor.ClinicId, notification.doctor.Id, notification.doctor.ServiceIds.ToList());

		await publishEndpoint.Publish(res);

		await context.SaveChangesAsync();
	}

	public async Task Handle(DoctorRemovedEvent notification, CancellationToken cancellationToken)
	{
		var res = new DoctorRemovedIntegrationEvent(notification.ClinicId, notification.DoctorId);

		await publishEndpoint.Publish(res);

		await context.SaveChangesAsync();
	}

	public async Task Handle(SubscriptionChangedEvent notification, CancellationToken cancellationToken)
	{
		var res = new SubscriptionChangedIntegrationEvent(notification.ClinicCenterId, notification.Subscription.MaxAppointmentsPerDay);

		await publishEndpoint.Publish(res);

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