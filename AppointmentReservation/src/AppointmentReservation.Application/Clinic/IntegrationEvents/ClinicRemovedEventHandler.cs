using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.AppointmentAggregate;
using AppointmentReservation.Domain.AvailabilityAggregate;
using AppointmentReservation.Domain.ClinicAggregate;
using AppointmentReservation.Domain.Common.EventualConsistency;
using AppointmentReservation.Infrastructure.Persistence.Repositories;
using ErrorOr;
using MediatR;
using SharedKernel.ClinicManagement;

namespace AppointmentReservation.Application.Clinic.IntegrationEvents;

public class ClinicRemovedEventHandler(
	IClinicRepository clinicRepository,
	IAppointmentRepository appointmentRepository
	) : INotificationHandler<ClinicRemovedIntegrationEvent>
{
	public async Task Handle(ClinicRemovedIntegrationEvent notification, CancellationToken cancellationToken)
	{
		var clinic = await clinicRepository.GetClinicAsync(notification.ClinicId);

		if (clinic == null)
			throw new EventualConsistencyException(ClinicErrors.ClinicNotFound);

		var isThereAnyAppointments = await appointmentRepository
			.IsThereAnyAppointmentsAsync(notification.ClinicId);

		if (isThereAnyAppointments)
			throw new EventualConsistencyException(ClinicErrors.DeleteClinicFailed);

		await clinicRepository.DeleteAsync(clinic);
	}
}
