using AppointmentReservation.Domain.AvailabilityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentReservation.Infrastructure.Persistence.Configuration;

public class AvailabilityRequestConfiguration : IEntityTypeConfiguration<Request>
{
	public void Configure(EntityTypeBuilder<Request> builder)
	{
		builder.HasKey(c => c.Id);

		builder.Property(c => c.Id)
			.ValueGeneratedNever();

		builder.Property(c => c.PatientId);
		builder.Property(c => c.Status);
	}
}