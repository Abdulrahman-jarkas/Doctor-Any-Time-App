using AppointmentReservation.Domain.RoomAggregate;
using AppointmentReservation.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentReservation.Infrastructure.Persistence.Configuration;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
	public void Configure(EntityTypeBuilder<Room> builder)
	{
		builder.HasKey(r => r.Id);

		builder.Property(r => r.Id)
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

		builder.Property(r => r.ClinicId);
	}
}
