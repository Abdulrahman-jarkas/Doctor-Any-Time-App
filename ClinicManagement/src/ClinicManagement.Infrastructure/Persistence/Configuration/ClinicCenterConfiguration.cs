using ClinicManagement.Domain.ClinicCenterAggregate;
using ClinicManagement.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Infrastructure.Persistence.Configuration;

public class ClinicCenterConfiguration : IEntityTypeConfiguration<ClinicCenter>
{
	public void Configure(EntityTypeBuilder<ClinicCenter> builder)
	{
		builder.HasKey(c => c.Id);

		builder.Property(c => c.Id)
			.ValueGeneratedNever();

		builder.ComplexProperty(p => p.Subscription, cp =>
		{
			cp.Property(propertyName => propertyName.SubscriptionType)
			.HasColumnName("SubscriptionType");
		});

		builder.Property<List<Guid>>("_clinicIds")
			.HasColumnName("ClinicIds")
			.HasListOfIdsConverter();
	}
}