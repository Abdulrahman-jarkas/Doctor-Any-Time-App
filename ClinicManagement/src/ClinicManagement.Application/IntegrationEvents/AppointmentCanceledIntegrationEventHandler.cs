using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.Common.EventualConsistency;
using ErrorOr;
using MediatR;
using SharedKernel.AppointmentReservation;

namespace ClinicManagement.Application.IntegrationEvents;

public class AppointmentCanceledIntegrationEventHandler(IAppointmentRepository appointmentRepository)
	: INotificationHandler<AppointmentCanceledIntegrationEvent>
{
	public async Task Handle(AppointmentCanceledIntegrationEvent notification, CancellationToken cancellationToken)
	{
		var appointment = await appointmentRepository.Get(notification.AppointmentId);

		if (appointment == null)
			throw new EventualConsistencyException(Error.NotFound("AppointmentCanceledIntegrationEventHandler", "Appointment was not found"));

		await appointmentRepository.DeleteAsync(appointment);
	}
}