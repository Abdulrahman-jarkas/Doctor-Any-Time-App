using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.AppointmentAggregate.Events;
using AppointmentReservation.Domain.Common.EventualConsistency;
using MediatR;

namespace AppointmentReservation.Application.Patient.Events;

public class AppointmentCanceledEventHandler : INotificationHandler<AppointmentCanceledEvent>
{
	private readonly IPatientRepository _patientRepository;

	public AppointmentCanceledEventHandler(IPatientRepository patientRepository)
	{
		_patientRepository = patientRepository;
	}

	public async Task Handle(AppointmentCanceledEvent notification, CancellationToken cancellationToken)
	{
		var patient = await _patientRepository.GetAsync(notification.Appointment.PatientId);

		if (patient == null)
			throw new EventualConsistencyException(AppointmentCanceledEvent.PatientNotFound);

		var removeFromScheduleRes = patient.RemoveFromSchedule(notification.Appointment.Date, notification.Appointment.Time);

		if (removeFromScheduleRes.IsError)
			throw new EventualConsistencyException(AppointmentCanceledEvent.UpdatePatientScheduleFailed);

		await _patientRepository.UpdateAsync(patient);
	}
}