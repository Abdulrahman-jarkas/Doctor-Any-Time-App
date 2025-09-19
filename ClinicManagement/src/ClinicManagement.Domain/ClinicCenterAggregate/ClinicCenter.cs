using ClinicManagement.Core.Common;
using ClinicManagement.Domain.ClinicAggregate;
using ClinicManagement.Domain.ClinicCenterAggregate.Events;
using ClinicManagement.Domain.ClinicCenterAggregate.ValueObjects;
using ErrorOr;

namespace ClinicManagement.Domain.ClinicCenterAggregate;

public class ClinicCenter : AggregateRoot
{
	public Subscription Subscription { get; private set; }

	private readonly List<Guid> _clinicIds = new();
	public IReadOnlyList<Guid> ClinicIds => _clinicIds.AsReadOnly();

	public ClinicCenter(Subscription subscription, Guid? id = null) : base(id ?? Guid.NewGuid())
	{
		Subscription = subscription;
	}

	public ErrorOr<Success> AddClinic(Clinic clinic)
	{
		if (_clinicIds.Count >= Subscription.MaxClinics)
			return ClinicCenterErrors.ExceedMaxNumberOfClinics;

		if (_clinicIds.Contains(clinic.Id))
			return ClinicCenterErrors.ClinicAlreadyExist;

		_clinicIds.Add(clinic.Id);

		_domainEvents.Add(new ClinicAddedEvent(clinic, Subscription.MaxAppointmentsPerDay));

		return Result.Success;
	}

	public ErrorOr<Success> RemoveClinic(Guid clinicId)
	{
		if (!_clinicIds.Contains(clinicId))
			return ClinicCenterErrors.NoClinicToRemove;

		_clinicIds.Remove(clinicId);

		_domainEvents.Add(new ClinicRemovedEvent(clinicId));

		return Result.Success;
	}

	public void ChangeSubscription(SubscriptionType subscriptionType)
	{
		Subscription = new Subscription(subscriptionType);

		_domainEvents.Add(new SubscriptionChangedEvent(Id, Subscription));
	}

	private ClinicCenter() { }
}