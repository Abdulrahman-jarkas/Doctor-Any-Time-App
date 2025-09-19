using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Infrastructure.Consumers;
using ClinicManagement.Infrastructure.IntegrationEvents;
using ClinicManagement.Infrastructure.Persistence;
using ClinicManagement.Infrastructure.Persistence.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ClinicManagement.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services
			.AddMediatR()
			.AddConfigurations(configuration)
			.AddBackgroundServices()
			.AddPersistence(configuration);

		return services;
	}

	public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<ClinicManagementDbContext>(options =>
			options.UseSqlServer(configuration.GetConnectionString("Database")));

		services.AddScoped<IClinicCenterRepository, ClinicCenterRepository>();
		services.AddScoped<IClinicRepository, ClinicRepository>();
		services.AddScoped<IDoctorRepository, DoctorRepository>();
		services.AddScoped<IRoomRepository, RoomRepository>();
		services.AddScoped<IAppointmentRepository, AppointmentRepository>();

		return services;
	}

	public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<MessageBrokerSettings>(configuration.GetSection(MessageBrokerSettings.Section));

		services.AddMassTransit(x =>
		{
			x.SetKebabCaseEndpointNameFormatter();

			x.AddConsumer<AppointmentCreatedConsumer>();
			x.AddConsumer<AppointmentCanceledConsumer>();
			x.AddConsumer<AppointmentCompletedConsumer>();

			x.AddEntityFrameworkOutbox<ClinicManagementDbContext>(o =>
			{
				o.DuplicateDetectionWindow = TimeSpan.FromSeconds(30);
				o.UseSqlServer();
				o.UseBusOutbox();
			});

			x.AddConfigureEndpointsCallback((context, name, cfg) =>
			{
				cfg.UseEntityFrameworkOutbox<ClinicManagementDbContext>(context);
			});

			x.UsingRabbitMq((context, cfg) =>
			{
				var settings = context.GetRequiredService<IOptions<MessageBrokerSettings>>().Value;

				cfg.Host(settings.HostName, "/", h =>
				{
					h.Username(settings.UserName);
					h.Password(settings.Password);
				});

				cfg.UseMessageRetry(r =>
				{
					r.Immediate(2);
				});

				cfg.ConfigureEndpoints(context);
			});
		});

		return services;
	}

	public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
	{
		return services;
	}

	public static IServiceCollection AddMediatR(this IServiceCollection services)
	{
		services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));

		return services;
	}

	public static async Task<IServiceProvider> SeedDataAsync(this IServiceProvider serviceProvider)
	{
		using (var scope = serviceProvider.CreateScope())
		{
			var services = scope.ServiceProvider;

			var clinicCenterRepo = services.GetRequiredService<IClinicCenterRepository>();


			await SeedData.InitializeAsync(clinicCenterRepo);
		}

		return serviceProvider;
	}
}