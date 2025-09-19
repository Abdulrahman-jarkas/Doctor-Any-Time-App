using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.Common.Entities;
using MediatR;
using SharedKernel.AppointmentReservation;

namespace ClinicManagement.Application.IntegrationEvents;


public class AppointmentCreatedIntegrationEventHandler(IAppointmentRepository appointmentRepository)
	: INotificationHandler<AppointmentCreatedIntegrationEvent>
{
	public async Task Handle(AppointmentCreatedIntegrationEvent notification, CancellationToken cancellationToken)
	{
		var appointment = await appointmentRepository.Get(notification.AppointmentId);

		if (appointment == null)
		{
			var res = new Appointment(notification.AppointmentId, notification.ClinicId, notification.DoctorId, notification.RoomId);

			await appointmentRepository.AddAsync(res);
		}
	}
}