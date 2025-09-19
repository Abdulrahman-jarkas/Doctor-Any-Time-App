using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.AvailabilityAggregate.Events;
using AppointmentReservation.Domain.Common.EventualConsistency;
using MediatR;

namespace AppointmentReservation.Application.Doctor.Events;

public class AvailabilityCanceledEventHandler : INotificationHandler<AvailabilityCanceledEvent>
{
	private readonly IDoctorRepository _doctorRepo;

	public AvailabilityCanceledEventHandler(IDoctorRepository doctorRepo)
	{
		_doctorRepo = doctorRepo;
	}

	public async Task Handle(AvailabilityCanceledEvent notification, CancellationToken cancellationToken)
	{
		var doctor = await _doctorRepo.GetDoctorAsync(notification.Availability.ClinicId, notification.Availability.DoctorId);

		if (doctor == null)
			throw new EventualConsistencyException(AvailabilityCanceledEvent.DoctorNotFound);

		var removeFromScheduleRes = doctor.RemoveFromSchedule(notification.Availability.Date, notification.Availability.Time);

		if (removeFromScheduleRes.IsError)
			throw new EventualConsistencyException(AvailabilityCanceledEvent.UpdateDoctorScheduleFailed);

		await _doctorRepo.UpdateAsync(doctor);
	}
}