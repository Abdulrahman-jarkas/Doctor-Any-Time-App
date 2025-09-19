using AppointmentReservation.Application.Common.Interfaces;
using MediatR;
using SharedKernel.ClinicManagement;
using ClinicEntity = AppointmentReservation.Domain.ClinicAggregate.Clinic;


namespace AppointmentReservation.Application.Clinic.IntegrationEvents;

public class ClinicAddedEventHandler(IClinicRepository clinicRepository) : INotificationHandler<ClinicAddedIntegrationEvent>
{
	public async Task Handle(ClinicAddedIntegrationEvent notification, CancellationToken cancellationToken)
	{
		var clinic = new ClinicEntity(
			notification.ClinicCenterId,
			notification.MaxAppointmentPerDay,
			notification.ClinicId);

		await clinicRepository.AddAsync(clinic);
	}
}