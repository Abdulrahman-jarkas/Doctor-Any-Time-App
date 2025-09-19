using AppointmentReservation.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace AppointmentReservation.Infrastructure;

public static class RequestPipeline
{
	public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder app)
	{
		app.UseMiddleware<EventualConsistencyMiddleware>();
		return app;
	}
}