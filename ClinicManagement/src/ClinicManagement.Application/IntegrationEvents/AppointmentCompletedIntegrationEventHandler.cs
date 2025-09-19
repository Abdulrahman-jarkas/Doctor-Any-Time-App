using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.Common.EventualConsistency;
using ErrorOr;
using MediatR;
using SharedKernel.AppointmentReservation;

namespace ClinicManagement.Application.IntegrationEvents;

public class AppointmentCompletedIntegrationEventHandler(IAppointmentRepository appointmentRepository)
	: INotificationHandler<AppointmentCompletedIntegrationEvent>
{
	public async Task Handle(AppointmentCompletedIntegrationEvent notification, CancellationToken cancellationToken)
	{
		var appointment = await appointmentRepository.Get(notification.AppointmentId);

		if (appointment == null)
			throw new EventualConsistencyException(Error.NotFound("AppointmentCompletedIntegrationEventHandler", "Appointment was not found"));

		await appointmentRepository.DeleteAsync(appointment);
	}
}

