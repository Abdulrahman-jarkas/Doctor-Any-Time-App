using AppointmentReservation.Domain.DoctorAggregate;
using AppointmentReservation.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentReservation.Infrastructure.Persistence.Configuration;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
	public void Configure(EntityTypeBuilder<Doctor> builder)
	{
		builder.HasKey(d => d.Id);

		builder.Property(d => d.Id)
			.ValueGeneratedNever();

		builder.Property<List<Guid>>("_serviceIds")
			.HasColumnName("ServiceIds")
			.HasListOfIdsConverter();

		builder.OwnsOne(d => d.Schedule, sb =>
		{
			sb.Property(s => s.Calender)
				.HasColumnName("ScheduleCalendar")
				.HasValueJsonConverter();
		});

		builder.Property(c => c.ClinicId);
	}
}