using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.AvailabilityAggregate;
using AppointmentReservation.Domain.Common.EventualConsistency;
using AppointmentReservation.Domain.DoctorAggregate;
using AppointmentReservation.Infrastructure.Persistence.Repositories;
using ErrorOr;
using MediatR;
using SharedKernel.ClinicManagement;

namespace AppointmentReservation.Application.Doctor.IntegrationEvents;

public class DoctorRemovedEventHandler(
	IDoctorRepository doctorRepository,
	IAppointmentRepository appointmentRepository,
	IAvailabilityRepository availabilityRepository
	) : INotificationHandler<DoctorRemovedIntegrationEvent>
{
	public async Task Handle(DoctorRemovedIntegrationEvent notification, CancellationToken cancellationToken)
	{
		var doctor = await doctorRepository.GetDoctorAsync(notification.ClinicId, notification.DoctorId);

		if (doctor == null)
			throw new EventualConsistencyException(DoctorErrors.DoctorNotFound);

		var isThereAnyAppointments = await appointmentRepository
			.IsThereAnyAppointmentsAsync(notification.ClinicId, doctor.Id, null);

		if (isThereAnyAppointments)
			throw new EventualConsistencyException(DoctorErrors.DeleteDoctorFailed);

		var availabilities = await availabilityRepository
			.GetDoctorAvailabilities(doctor.Id, AvailabilityStatus.Valid);

		foreach (var availability in availabilities)
		{
			var cancelRes = availability.Cancel();

			if (cancelRes.IsError)
				throw new EventualConsistencyException(Error.Unexpected("DoctorRemovedEventHandler", "Failed to cancel availability."));

			await availabilityRepository.DeleteAsync(availability);
		}

		await doctorRepository.RemoveDoctor(doctor);
	}
}