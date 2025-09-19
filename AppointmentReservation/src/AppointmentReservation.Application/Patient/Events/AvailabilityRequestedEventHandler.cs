using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.AvailabilityAggregate.Events;
using AppointmentReservation.Domain.Common.EventualConsistency;
using MediatR;

namespace AppointmentReservation.Application.Patient.Events;

public class AvailabilityRequestedEventHandler : INotificationHandler<AvailabilityRequestedEvent>
{
	private readonly IPatientRepository _patientRepository;

	public AvailabilityRequestedEventHandler(IPatientRepository patientRepository)
	{
		_patientRepository = patientRepository;
	}

	public async Task Handle(AvailabilityRequestedEvent notification, CancellationToken cancellationToken)
	{
		var patient = await _patientRepository.GetAsync(notification.Request.PatientId);

		if (patient == null)
			throw new EventualConsistencyException(AvailabilityRequestedEvent.PatientNotFound);

		var addToScheduleRes = patient.AddToSchedule(notification.Availability.Date, notification.Availability.Time);

		if (addToScheduleRes.IsError)
			throw new EventualConsistencyException(AvailabilityRequestedEvent.UpdatePatientScheduleFailed);

		await _patientRepository.UpdateAsync(patient);
	}
}
