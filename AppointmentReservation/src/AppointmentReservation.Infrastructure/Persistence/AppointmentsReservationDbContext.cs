using AppointmentReservation.Core.Common;
using AppointmentReservation.Domain.AppointmentAggregate;
using AppointmentReservation.Domain.AvailabilityAggregate;
using AppointmentReservation.Domain.ClinicAggregate;
using AppointmentReservation.Domain.Common;
using AppointmentReservation.Domain.DoctorAggregate;
using AppointmentReservation.Domain.PatientAggregate;
using AppointmentReservation.Domain.RoomAggregate;
using AppointmentReservation.Infrastructure.Middleware;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AppointmentReservation.Infrastructure.Persistence;

public class AppointmentsReservationDbContext : DbContext
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IPublisher _publisher;

	public DbSet<Appointment> Appointments { get; set; }
	public DbSet<Doctor> Doctors { get; set; }
	public DbSet<Clinic> Clinics { get; set; }
	public DbSet<Room> Rooms { get; set; }
	public DbSet<Patient> Patients { get; set; }
	public DbSet<Availability> Availabilities { get; set; }

	public AppointmentsReservationDbContext(
		DbContextOptions options,
		IHttpContextAccessor httpContextAccessor,
		IPublisher publisher) : base(options)
	{
		_httpContextAccessor = httpContextAccessor;
		_publisher = publisher;
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.AddOutboxStateEntity();
		modelBuilder.AddOutboxMessageEntity();
		modelBuilder.AddInboxStateEntity();

		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		base.OnModelCreating(modelBuilder);
	}

	public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		var domainEvents = ChangeTracker.Entries<AggregateRoot>()
		   .Select(entry => entry.Entity.PopDomainEvents())
		   .SelectMany(x => x)
		   .ToList();

		if (IsUserWaitingOnline())
		{
			AddDomainEventsToOfflineProcessingQueue(domainEvents);
			return await base.SaveChangesAsync(cancellationToken);
		}

		await PublishDomainEvents(domainEvents);
		return await base.SaveChangesAsync(cancellationToken);
	}

	private bool IsUserWaitingOnline() => _httpContextAccessor.HttpContext is not null;

	private async Task PublishDomainEvents(List<IDomainEvent> domainEvents)
	{
		foreach (var domainEvent in domainEvents)
		{
			await _publisher.Publish(domainEvent);
		}
	}

	private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
	{
		Queue<IDomainEvent> domainEventsQueue = _httpContextAccessor.HttpContext.Items.TryGetValue(EventualConsistencyMiddleware.DomainEventsKey, out var value) &&
			value is Queue<IDomainEvent> existingDomainEvents
			? existingDomainEvents
		: new();

		domainEvents.ForEach(domainEventsQueue.Enqueue);
		_httpContextAccessor.HttpContext.Items[EventualConsistencyMiddleware.DomainEventsKey] = domainEventsQueue;
	}
}
