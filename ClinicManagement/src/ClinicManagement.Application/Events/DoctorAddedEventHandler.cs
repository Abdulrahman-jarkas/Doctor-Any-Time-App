using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.ClinicAggregate.Events;
using MediatR;

namespace ClinicManagement.Application.Events;

public class DoctorAddedEventHandler(IDoctorRepository doctorRepository) : INotificationHandler<DoctorAddedEvent>
{
	public async Task Handle(DoctorAddedEvent notification, CancellationToken cancellationToken)
	{
		await doctorRepository.AddAsync(notification.doctor);
	}
}