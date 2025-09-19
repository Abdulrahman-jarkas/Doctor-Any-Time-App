using AppointmentReservation.Infrastructure;
using AppointmentReservation.Application;

var builder = WebApplication.CreateBuilder(args);
{
	builder.Services.AddControllers();
	builder.Services.AddOpenApi();
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
