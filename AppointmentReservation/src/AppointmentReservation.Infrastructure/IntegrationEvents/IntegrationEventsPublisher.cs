//using System.Text;
//using System.Text.Json;
//using Microsoft.Extensions.Options;
//using RabbitMQ.Client;
//using SharedKernel;

//namespace AppointmentReservation.Infrastructure.IntegrationEvents;

//public sealed class IntegrationEventsPublisher : IIntegrationEventsPublisher, IAsyncDisposable
//{
//	private readonly MessageBrokerSettings _settings;
//	private readonly IConnectionFactory _connectionFactory;
//	private IConnection? _connection;
//	private IChannel? _channel;
//	private bool _initialized;

//	public IntegrationEventsPublisher(IOptions<MessageBrokerSettings> options)
//	{
//		_settings = options.Value ?? throw new ArgumentNullException(nameof(options));

//		_connectionFactory = new ConnectionFactory
//		{
//			HostName = _settings.HostName,
//			Port = _settings.Port,
//			UserName = _settings.UserName,
//			Password = _settings.Password
//		};
//	}

//	public async Task EnsureInitializedAsync()
//	{
//		if (_initialized) return;

//		_connection = await _connectionFactory.CreateConnectionAsync();
//		_channel = await _connection.CreateChannelAsync();

//		await _channel.ExchangeDeclareAsync(
//			exchange: _settings.ExchangeName,
//			type: ExchangeType.Fanout,
//			durable: true);

//		_initialized = true;
//	}

//	public async Task PublishEventAsync(IIntegrationEvent integrationEvent)
//	{
//		if (integrationEvent is null) throw new ArgumentNullException(nameof(integrationEvent));

//		await EnsureInitializedAsync();

//		var json = JsonSerializer.Serialize(integrationEvent);
//		var body = Encoding.UTF8.GetBytes(json);

//		await _channel!.BasicPublishAsync(
//			exchange: _settings.ExchangeName,
//			routingKey: string.Empty,
//			body: body);
//	}

//	public async ValueTask DisposeAsync()
//	{
//		if (_channel is not null)
//		{
//			await _channel.CloseAsync();
//			await _channel.DisposeAsync();
//		}

//		if (_connection is not null)
//		{
//			await _connection.CloseAsync();
//			await _connection.DisposeAsync();
//		}
//	}
//}