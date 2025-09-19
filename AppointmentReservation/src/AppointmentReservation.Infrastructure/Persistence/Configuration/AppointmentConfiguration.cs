using AppointmentReservation.Domain.AppointmentAggregate;
using AppointmentReservation.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentReservation.Infrastructure.Persistence.Configuration;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
	public void Configure(EntityTypeBuilder<Appointment> builder)
	{
		builder.HasKey(c => c.Id);

		builder.Property(c => c.Id)
			.ValueGeneratedNever();

		builder.Property(p => p.ServiceIds)
			.HasColumnName("ServiceIds")
			.HasListOfIdsConverter();

		builder.OwnsOne(s => s.Time);

		builder.Property(c => c.Date);
		builder.Property(c => c.ClinicId);
		builder.Property(c => c.DoctorId);
		builder.Property(c => c.PatientId);
		builder.Property(c => c.RoomId);
	}
}
