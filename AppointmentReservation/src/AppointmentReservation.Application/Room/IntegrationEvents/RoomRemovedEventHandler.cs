using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.ClinicAggregate;
using AppointmentReservation.Domain.Common.EventualConsistency;
using AppointmentReservation.Infrastructure.Persistence.Repositories;
using MediatR;
using SharedKernel.ClinicManagement;

namespace AppointmentReservation.Application.Room.IntegrationEvents;

public class RoomRemovedEventHandler(
	IAppointmentRepository appointmentRepository,
	IRoomRepository roomRepository
	) : INotificationHandler<RoomRemovedIntegrationEvent>
{
	public async Task Handle(RoomRemovedIntegrationEvent notification, CancellationToken cancellationToken)
	{
		var room = await roomRepository.GetRoomByIdAsync(notification.RoomId);

		if (room == null)
			throw new EventualConsistencyException(ClinicErrors.RoomNotFound);

		var isThereAnyAppointments = await appointmentRepository
			.IsThereAnyAppointmentsAsync(notification.ClinicId, null, notification.RoomId);

		if (isThereAnyAppointments)
			throw new EventualConsistencyException(ClinicErrors.DeleteRoomFailed);

		await roomRepository.DeleteAsync(room);
	}
}