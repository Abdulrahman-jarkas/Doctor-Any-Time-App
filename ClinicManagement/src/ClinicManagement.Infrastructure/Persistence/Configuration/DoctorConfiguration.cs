using ClinicManagement.Domain.DoctorAggregate;
using ClinicManagement.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Infrastructure.Persistence.Configuration;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
	public void Configure(EntityTypeBuilder<Doctor> builder)
	{
		builder.HasKey(c => c.Id);

		builder.Property(c => c.Id)
			.ValueGeneratedNever();

		builder.Property<List<Guid>>("_serviceIds")
			.HasColumnName("ServiceIds")
			.HasListOfIdsConverter();

	}
}