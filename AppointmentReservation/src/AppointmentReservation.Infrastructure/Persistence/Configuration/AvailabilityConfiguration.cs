using AppointmentReservation.Domain.AvailabilityAggregate;
using AppointmentReservation.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentReservation.Infrastructure.Persistence.Configuration;

public class AvailabilityConfiguration : IEntityTypeConfiguration<Availability>
{
	public void Configure(EntityTypeBuilder<Availability> builder)
	{
		builder.HasKey(c => c.Id);

		builder.Property(c => c.Id)
			.ValueGeneratedNever();

		builder.Property<List<Guid>>("_serviceIds")
			.HasColumnName("ServiceIds")
			.HasListOfIdsConverter();

		builder.HasMany(r => r.Requests)
			.WithOne(request => request.Availability)
			.OnDelete(DeleteBehavior.Cascade);

		builder.OwnsOne(s => s.Time);

		builder.Property(c => c.ClinicId);
		builder.Property(c => c.DoctorId);
		builder.Property(c => c.Date);
	}
}
