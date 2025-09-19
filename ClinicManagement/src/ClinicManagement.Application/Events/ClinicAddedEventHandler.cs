using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.ClinicCenterAggregate.Events;
using MediatR;

namespace ClinicManagement.Application.Events;

public class ClinicAddedEventHandler(IClinicRepository clinicRepository) : INotificationHandler<ClinicAddedEvent>
{
	public async Task Handle(ClinicAddedEvent notification, CancellationToken cancellationToken)
	{
		await clinicRepository.AddAsync(notification.Clinic);
	}
}
