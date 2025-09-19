//using ClinicManagement.Infrastructure.Persistence;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using SharedKernel;
//using System.Text.Json;

//namespace ClinicManagement.Infrastructure.IntegrationEvents;

//public class PublishIntegrationEventsBackgroundService : IHostedService
//{
//	private Task? _doWorkTask = null;
//	private PeriodicTimer? _timer = null!;
//	private readonly IIntegrationEventsPublisher _integrationEventPublisher;
//	private readonly IServiceScopeFactory _serviceScopeFactory;
//	private readonly ILogger<PublishIntegrationEventsBackgroundService> _logger;
//	private readonly CancellationTokenSource _cts;

//	public PublishIntegrationEventsBackgroundService(
//		IIntegrationEventsPublisher integrationEventPublisher,
//		IServiceScopeFactory serviceScopeFactory,
//		ILogger<PublishIntegrationEventsBackgroundService> logger)
//	{
//		_integrationEventPublisher = integrationEventPublisher;
//		_serviceScopeFactory = serviceScopeFactory;
//		_logger = logger;
//		_cts = new CancellationTokenSource();
//	}

//	public Task StartAsync(CancellationToken cancellationToken)
//	{
//		_doWorkTask = DoWorkAsync();

//		return Task.CompletedTask;
//	}

//	private async Task DoWorkAsync()
//	{
//		_logger.LogInformation("Starting integration event publisher background service.");

//		_timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

//		while (await _timer.WaitForNextTickAsync(_cts.Token))
//		{
//			try
//			{
//				await PublishIntegrationEventsFromDbAsync();
//			}
//			catch (Exception e)
//			{
//				_logger.LogError(e, "Exception occurred while publishing integration events.");
//			}
//		}
//	}

//	private async Task PublishIntegrationEventsFromDbAsync()
//	{
//		using var scope = _serviceScopeFactory.CreateScope();
//		var dbContext = scope.ServiceProvider.GetRequiredService<ClinicManagementDbContext>();

//		var outboxIntegrationEvents = dbContext.OutboxIntegrationEvents.ToList();

//		_logger.LogInformation("Read a total of {NumEvents} outbox integration events", outboxIntegrationEvents.Count);

//		outboxIntegrationEvents.ForEach(async outboxIntegrationEvent => {
//			var integrationEvent = JsonSerializer.Deserialize<IIntegrationEvent>(outboxIntegrationEvent.EventContent);

//			if (integrationEvent == null)
//				throw new NullReferenceException();

//			_logger.LogInformation("Publishing event of type: {EventType}", integrationEvent.GetType().Name);

//			await _integrationEventPublisher.PublishEventAsync(integrationEvent);

//			_logger.LogInformation("Integration event published successfully");
//		});

//		dbContext.RemoveRange(outboxIntegrationEvents);
//		await dbContext.SaveChangesAsync();
//	}

//	public async Task StopAsync(CancellationToken cancellationToken)
//	{
//		if (_doWorkTask is null)
//		{
//			return;
//		}

//		_cts.Cancel();
//		await _doWorkTask;

//		_timer?.Dispose();
//		_cts.Dispose();
//	}
//}