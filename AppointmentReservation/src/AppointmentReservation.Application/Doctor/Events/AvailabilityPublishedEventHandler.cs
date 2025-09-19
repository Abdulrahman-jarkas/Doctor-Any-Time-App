using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.AvailabilityAggregate.Events;
using AppointmentReservation.Domain.Common.EventualConsistency;
using MediatR;

namespace AppointmentReservation.Application.Doctor.Events;

public class AvailabilityPublishedEventHandler : INotificationHandler<AvailabilityPublishedEvent>
{
	private readonly IDoctorRepository _doctorRepo;

	public AvailabilityPublishedEventHandler(IDoctorRepository doctorRepo)
	{
		_doctorRepo = doctorRepo;
	}

	public async Task Handle(AvailabilityPublishedEvent notification, CancellationToken cancellationToken)
	{
		var doctor = await _doctorRepo.GetDoctorAsync(notification.Availability.ClinicId, notification.Availability.DoctorId);

		if (doctor == null)
			throw new EventualConsistencyException(AvailabilityPublishedEvent.DoctorNotFound);

		var addToScheduleRes = doctor.AddToSchedule(notification.Availability.Date, notification.Availability.Time);

		if(addToScheduleRes.IsError)
			throw new EventualConsistencyException(AvailabilityPublishedEvent.UpdateDoctorScheduleFailed);

		await _doctorRepo.UpdateAsync(doctor);
	}
}