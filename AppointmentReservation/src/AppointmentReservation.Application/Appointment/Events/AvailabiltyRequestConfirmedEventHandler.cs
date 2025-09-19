using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.AvailabilityAggregate.Events;
using AppointmentEntity = AppointmentReservation.Domain.AppointmentAggregate.Appointment;
using AppointmentReservation.Domain.Common.EventualConsistency;
using MediatR;
using AppointmentReservation.Infrastructure.Persistence.Repositories;

namespace AppointmentReservation.Application.Appointment.Events;

public class AvailabiltyRequestConfirmedEventHandler(
	IAppointmentRepository appointmentRepository,
	IRoomRepository roomRepository
	) : INotificationHandler<AvailabiltyRequestConfirmedEvent>
{
	public async Task Handle(AvailabiltyRequestConfirmedEvent notification, CancellationToken cancellationToken)
	{
		var request = notification.Availability.Requests.FirstOrDefault(request => request.Id == notification.RequestId);

		if(request == null)
			throw new EventualConsistencyException(AvailabiltyRequestConfirmedEvent.RequestNotFound);

		var room = await roomRepository.GetAvailabileRoomAsync(
			notification.Availability.ClinicId,
			notification.Availability.Date,
			notification.Availability.Time,
			notification.Availability.ServiceIds);

		if(room == null)
			throw new EventualConsistencyException(
				AvailabiltyRequestConfirmedEvent.NoRoomsAvailabileAtThisTimeForThisServices);

		var appointment = new AppointmentEntity(
			notification.Availability.ClinicId,
			notification.Availability.DoctorId,
			room.Id,
			request.PatientId,
			notification.Availability.ServiceIds.ToList(),
			notification.Availability.Date,
			notification.Availability.Time
			);

		await appointmentRepository.AddAsync(appointment);
	}
}
