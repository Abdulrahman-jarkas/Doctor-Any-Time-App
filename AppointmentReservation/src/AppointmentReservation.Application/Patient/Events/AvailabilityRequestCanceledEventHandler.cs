using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.AvailabilityAggregate.Events;
using AppointmentReservation.Domain.Common.EventualConsistency;
using MediatR;

namespace AppointmentReservation.Application.Patient.Events;

public class AvailabilityRequestCanceledEventHandler : INotificationHandler<AvailabilityRequestCanceledEvent>
{
	private readonly IPatientRepository _patientRepository;

	public AvailabilityRequestCanceledEventHandler(IPatientRepository patientRepository)
	{
		_patientRepository = patientRepository;
	}

	public async Task Handle(AvailabilityRequestCanceledEvent notification, CancellationToken cancellationToken)
	{
		var patient = await _patientRepository.GetAsync(notification.Request.PatientId);

		if (patient == null)
			throw new EventualConsistencyException(AvailabilityRequestCanceledEvent.PatientNotFound);

		var removeFromScheduleRes = patient.RemoveFromSchedule(notification.Availability.Date, notification.Availability.Time);

		if (removeFromScheduleRes.IsError)
			throw new EventualConsistencyException(AvailabilityRequestCanceledEvent.UpdatePatientScheduleFailed);

		await _patientRepository.UpdateAsync(patient);
	}
}