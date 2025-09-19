using AppointmentReservation.Infrastructure;
using ClinicManagement.Infrastructure;
using ClinicManagement.Application;


var builder = WebApplication.CreateBuilder(args);
{
	builder.Services.AddOpenApi();

	builder.Services.AddControllers();

	builder.Services.AddProblemDetails();
	builder.Services.AddHttpContextAccessor();

	builder.Services
		.AddApplication()
		.AddInfrastructure(builder.Configuration);
}



var app = builder.Build();
{
	app.UseExceptionHandler();
	app.AddInfrastructureMiddleware();

	if (app.Environment.IsDevelopment())
	{
		app.MapOpenApi();
		await app.Services.SeedDataAsync();
	}

	app.UseHttpsRedirection();
	app.MapControllers();

	app.Run();
}