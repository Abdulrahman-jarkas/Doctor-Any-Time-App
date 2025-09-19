using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.ClinicAggregate.Events;
using ClinicManagement.Domain.Common.EventualConsistency;
using MediatR;

namespace ClinicManagement.Application.Events;

public class DoctorRemovedEventHandler(IDoctorRepository doctorRepository) : INotificationHandler<DoctorRemovedEvent>
{
	public async Task Handle(DoctorRemovedEvent notification, CancellationToken cancellationToken)
	{
		var doctor = await doctorRepository.Get(notification.DoctorId);

		if (doctor == null)
			throw new EventualConsistencyException(DoctorRemovedEvent.DoctorNotFound);

		await doctorRepository.Delete(doctor);
	}
}
