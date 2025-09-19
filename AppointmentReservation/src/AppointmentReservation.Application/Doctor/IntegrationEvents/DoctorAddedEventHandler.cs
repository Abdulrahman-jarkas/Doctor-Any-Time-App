using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.Common.Entities;
using MediatR;
using SharedKernel.ClinicManagement;
using DoctorEntity = AppointmentReservation.Domain.DoctorAggregate.Doctor;


namespace AppointmentReservation.Application.Doctor.IntegrationEvents;

public class DoctorAddedEventHandler(IDoctorRepository doctorRepository) : INotificationHandler<DoctorAddedIntegrationEvent>
{
	public async Task Handle(DoctorAddedIntegrationEvent notification, CancellationToken cancellationToken)
	{
		var doctor = new DoctorEntity(
			Schedule.Empty(),
			notification.ServiceIds,
			notification.ClinicId,
			notification.DoctorId);

		await doctorRepository.AddAsync(doctor);
	}
}
