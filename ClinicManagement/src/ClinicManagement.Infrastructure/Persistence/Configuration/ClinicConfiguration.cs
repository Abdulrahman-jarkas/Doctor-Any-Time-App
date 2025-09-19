using ClinicManagement.Domain.ClinicAggregate;
using ClinicManagement.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Infrastructure.Persistence.Configuration;

public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
{
	public void Configure(EntityTypeBuilder<Clinic> builder)
	{
		builder.HasKey(c => c.Id);

		builder.Property(c => c.Id)
			.ValueGeneratedNever();

		builder.Property<List<Guid>>("_doctorIds")
			.HasColumnName("DoctorIds")
			.HasListOfIdsConverter();


		builder.Property<List<Guid>>("_roomIds")
			.HasColumnName("RoomIds")
			.HasListOfIdsConverter();
	}
}
