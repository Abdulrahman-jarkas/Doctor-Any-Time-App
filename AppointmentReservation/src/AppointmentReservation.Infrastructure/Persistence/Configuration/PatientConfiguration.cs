using AppointmentReservation.Domain.PatientAggregate;
using AppointmentReservation.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentReservation.Infrastructure.Persistence.Configuration;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
	public void Configure(EntityTypeBuilder<Patient> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Id)
			.ValueGeneratedNever();

		builder.OwnsOne(d => d.Schedule, sb =>
		{
			sb.Property(s => s.Calender)
				.HasColumnName("ScheduleCalendar")
				.HasValueJsonConverter();
		});
	}
}
