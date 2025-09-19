//using MediatR;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using SharedKernel;
//using System.Text.Json;
//using System.Text;
//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;

//namespace AppointmentReservation.Infrastructure.IntegrationEvents;

//public class IntegrationEventConsumer : IIntegrationEventConsumer
//{
//	private IConnection? _connection;
//	private IChannel? _channel;
//	private readonly MessageBrokerSettings _messageBrokerSettings;
//	private readonly IConnectionFactory _connectionFactory;
//	private readonly IServiceScopeFactory _serviceScopeFactory;
//	private readonly ILogger<IntegrationEventConsumer> _logger;
//	private readonly CancellationTokenSource _cts;

//	public IntegrationEventConsumer(
//		IOptions<MessageBrokerSettings> messageBrokerOptions,
//		ILogger<IntegrationEventConsumer> logger,
//		IServiceScopeFactory serviceScopeFactory)
//	{
//		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
//		_serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
//		_messageBrokerSettings = messageBrokerOptions?.Value ?? throw new ArgumentNullException(nameof(messageBrokerOptions));

//		_cts = new CancellationTokenSource();

//		_connectionFactory = new ConnectionFactory
//		{
//			HostName = _messageBrokerSettings.HostName,
//			Port = _messageBrokerSettings.Port,
//			UserName = _messageBrokerSettings.UserName,
//			Password = _messageBrokerSettings.Password
//		};
//	}

//	public async Task InitializeAsync(CancellationToken cancellationToken = default)
//	{
//		_connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);
//		_channel = await _connection.CreateChannelAsync();

//		await _channel.ExchangeDeclareAsync(
//			exchange: _messageBrokerSettings.ExchangeName,
//			type: ExchangeType.Fanout,
//			durable: true,
//			cancellationToken: cancellationToken);

//		await _channel.QueueDeclareAsync(
//			queue: _messageBrokerSettings.QueueName,
//			durable: true,
//			exclusive: false,
//			autoDelete: false,
//			cancellationToken: cancellationToken);

//		await _channel.QueueBindAsync(
//			queue: _messageBrokerSettings.QueueName,
//			exchange: _messageBrokerSettings.ExchangeName,
//			routingKey: string.Empty,
//			cancellationToken: cancellationToken);

//		var consumer = new AsyncEventingBasicConsumer(_channel);
//		consumer.ReceivedAsync += PublishIntegrationEventAsync;

//		await _channel.BasicConsumeAsync(
//			queue: _messageBrokerSettings.QueueName,
//			autoAck: false,
//			consumer: consumer,
//			cancellationToken: cancellationToken);

//		_logger.LogInformation("IntegrationEventConsumer initialized and ready to receive events.");
//	}

//	private async Task PublishIntegrationEventAsync(object sender, BasicDeliverEventArgs eventArgs)
//	{
//		if (_cts.IsCancellationRequested)
//		{
//			_logger.LogWarning("Cancellation requested, skipping event consumption.");
//			return;
//		}

//		try
//		{
//			_logger.LogDebug("Received message with DeliveryTag: {DeliveryTag}", eventArgs.DeliveryTag);

//			var body = eventArgs.Body.ToArray();
//			var message = Encoding.UTF8.GetString(body);

//			if (string.IsNullOrWhiteSpace(message))
//			{
//				_logger.LogWarning("Received empty message. Rejecting.");
//				await _channel!.BasicNackAsync(eventArgs.DeliveryTag, multiple: false, requeue: false);
//				return;
//			}

//			using var scope = _serviceScopeFactory.CreateScope();
//			var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

//			var integrationEvent = JsonSerializer.Deserialize<IIntegrationEvent>(message, new JsonSerializerOptions
//			{
//				PropertyNameCaseInsensitive = true
//			});

//			if (integrationEvent == null)
//			{
//				_logger.LogWarning("Deserialization failed. Rejecting message.");
//				await _channel!.BasicNackAsync(eventArgs.DeliveryTag, multiple: false, requeue: false);
//				return;
//			}

//			_logger.LogInformation("Publishing integration event: {EventType}", integrationEvent.GetType().Name);
//			await publisher.Publish(integrationEvent);

//			await _channel!.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
//			_logger.LogInformation("Event processed and acknowledged successfully.");
//		}
//		catch (Exception ex)
//		{
//			_logger.LogError(ex, "Error occurred while processing integration event.");
//			// Option: move message to a dead-letter queue (DLQ) instead of requeueing indefinitely
//			await _channel!.BasicNackAsync(eventArgs.DeliveryTag, multiple: false, requeue: false);
//		}
//	}

//	public async ValueTask DisposeAsync()
//	{
//		_cts.Cancel();
//		_cts.Dispose();

//		if (_channel != null)
//		{
//			await _channel.DisposeAsync();
//		}

//		if (_connection != null)
//		{
//			await _connection.DisposeAsync();
//		}

//		_logger.LogInformation("IntegrationEventConsumer disposed.");
//	}
//}
