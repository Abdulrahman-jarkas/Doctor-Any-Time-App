using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.AppointmentAggregate.Events;
using AppointmentReservation.Domain.Common.EventualConsistency;
using MediatR;

namespace AppointmentReservation.Application.Doctor.Events;

public class AppointmentCanceledEventHandler : INotificationHandler<AppointmentCanceledEvent>
{
	private readonly IDoctorRepository _doctorRepo;

	public AppointmentCanceledEventHandler(IDoctorRepository doctorRepo)
	{
		_doctorRepo = doctorRepo;
	}

	public async Task Handle(AppointmentCanceledEvent notification, CancellationToken cancellationToken)
	{
		var doctor = await _doctorRepo.GetDoctorAsync(notification.Appointment.ClinicId, notification.Appointment.DoctorId);

		if (doctor == null)
			throw new EventualConsistencyException(AppointmentCanceledEvent.DoctorNotFound);

		var removeFromScheduleRes = doctor.RemoveFromSchedule(notification.Appointment.Date, notification.Appointment.Time);

		if (removeFromScheduleRes.IsError)
			throw new EventualConsistencyException(AppointmentCanceledEvent.UpdateDoctorScheduleFailed);

		await _doctorRepo.UpdateAsync(doctor);
	}
}