using AppointmentReservation.Domain.ClinicAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentReservation.Infrastructure.Persistence.Configuration;

public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
{
	public void Configure(EntityTypeBuilder<Clinic> builder)
	{
		builder.HasKey(c => c.Id);

		builder.Property(c => c.Id)
			.ValueGeneratedNever();

		builder.Property(c => c.ClinicCenterId);
		builder.Property(c => c.MaxAppointmentsPerDay);
	}
}
