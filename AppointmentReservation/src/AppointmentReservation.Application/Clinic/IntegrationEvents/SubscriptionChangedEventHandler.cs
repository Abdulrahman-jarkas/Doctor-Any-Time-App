using AppointmentReservation.Application.Common.Interfaces;
using AppointmentReservation.Domain.Common.EventualConsistency;
using ErrorOr;
using MediatR;
using SharedKernel.ClinicManagement;

namespace AppointmentReservation.Application.Clinic.IntegrationEvents;

public class SubscriptionChangedEventHandler(IClinicRepository clinicRepository)
	: INotificationHandler<SubscriptionChangedIntegrationEvent>
{
	public async Task Handle(SubscriptionChangedIntegrationEvent notification, CancellationToken cancellationToken)
	{
		var clinics = await clinicRepository.GetByClinicCenter(notification.ClinicCenterId);

		if (clinics.Count <= 0)
			throw new EventualConsistencyException(
				Error.Conflict(
					"SubscriptionChangedEventHandler.NoClinicsForClinicCenter",
					"Can not find any clinics"));

		foreach(var clinic in clinics)
		{
			var res  = clinic.SetMaxAppointmentPerDay(notification.MaxAppointmentPerDay);

			if(res.IsError)
				throw new EventualConsistencyException(res.FirstError);

			await clinicRepository.UpdateAsync(clinic);
		}
	}
}
