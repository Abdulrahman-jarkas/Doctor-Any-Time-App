using ClinicManagement.Application.Common.Interfaces;
using ClinicManagement.Domain.ClinicCenterAggregate;
using ClinicManagement.Domain.ClinicCenterAggregate.ValueObjects;

namespace ClinicManagement.Infrastructure.Persistence;

public static class SeedData
{
	private static readonly Guid clinicCenterId = Guid.Parse("c1d2b349-77f1-425d-a9f5-7137002c0d91");
	private static readonly Subscription subscription = new Subscription(SubscriptionType.Free);

	public static async Task InitializeAsync(IClinicCenterRepository clinicCenterRepo)
	{

		// Avoid reseeding if data already exists
		if (await clinicCenterRepo.AnyAsync())
			return;

		var clinicCenter = new ClinicCenter(subscription, clinicCenterId);

		await clinicCenterRepo.AddAsync(clinicCenter);
	}
}