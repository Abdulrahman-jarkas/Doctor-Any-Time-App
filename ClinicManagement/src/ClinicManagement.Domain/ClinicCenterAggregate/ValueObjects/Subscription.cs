using ClinicManagement.Core.Common;

namespace ClinicManagement.Domain.ClinicCenterAggregate.ValueObjects;

public enum SubscriptionType
{
	Free,
	Standard,
	Premium
}

public class Subscription : ValueObject
{
	public SubscriptionType SubscriptionType { get; private set; }

	public Subscription(SubscriptionType subscriptionType)
	{
		SubscriptionType = subscriptionType;
	}

	public int MaxClinics => SubscriptionType switch
		{
			SubscriptionType.Free => 1,
			SubscriptionType.Standard => 5,
			SubscriptionType.Premium => int.MaxValue,
			_ => throw new ArgumentOutOfRangeException(nameof(SubscriptionType), SubscriptionType, null)
		};

	public int MaxAppointmentsPerDay => SubscriptionType switch
		{
			SubscriptionType.Free => 5,
			SubscriptionType.Standard => 50,
			SubscriptionType.Premium => int.MaxValue,
			_ => throw new ArgumentOutOfRangeException(nameof(SubscriptionType), SubscriptionType, null)
		};

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return SubscriptionType;
	}

	private Subscription(){}
}
