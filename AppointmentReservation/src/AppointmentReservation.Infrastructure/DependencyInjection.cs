using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Infrastructure.Consumers;
using AppointmentReservation.Infrastructure.IntegrationEvents;
using AppointmentReservation.Infrastructure.Persistence;
using AppointmentReservation.Infrastructure.Persistence.Configuration;
using AppointmentReservation.Infrastructure.Persistence.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AppointmentReservation.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services
			.AddConfigurations(configuration)
			.AddMediatR()
			.AddPersistence(configuration)
			.AddBackgroundServices();

		return services;
	}

	public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<AppointmentsReservationDbContext>(options =>
			options.UseSqlServer(configuration.GetConnectionString("Database")));

		services.AddScoped<IAppointmentRepository, AppointmentRepository>();
		services.AddScoped<IClinicRepository, ClinicRepository>();
		services.AddScoped<IDoctorRepository, DoctorRepository>();
		services.AddScoped<IRoomRepository, RoomRepository>();
		services.AddScoped<IPatientRepository, PatientRepository>();
		services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();

		return services;
	}

	public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<MessageBrokerSettings>(configuration.GetSection(MessageBrokerSettings.Section));

		services.AddMassTransit(x =>
		{
			x.SetKebabCaseEndpointNameFormatter();

			x.AddConsumer<ClinicAddedConsumer>();
			x.AddConsumer<ClinicRemovedConsumer>();
			x.AddConsumer<RoomAddedConsumer>();
			x.AddConsumer<RoomRemovedConsumer>();
			x.AddConsumer<DoctorAddedConsumer>();
			x.AddConsumer<DoctorRemovedConsumer>();
			x.AddConsumer<SubscriptionChangedConsumer>();

			x.AddEntityFrameworkOutbox<AppointmentsReservationDbContext>(o =>
			{
				o.DuplicateDetectionWindow = TimeSpan.FromSeconds(30);
				o.UseSqlServer();
				o.UseBusOutbox();
			});

			x.AddConfigureEndpointsCallback((context, name, cfg) =>
			{
				cfg.UseEntityFrameworkOutbox<AppointmentsReservationDbContext>(context);
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
			var patientRepo = scope
				.ServiceProvider
				.GetRequiredService<IPatientRepository>();

			await SeedData.InitializeAsync(patientRepo);
		}

		return serviceProvider;
	}
}