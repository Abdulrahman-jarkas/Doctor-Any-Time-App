using ClinicManagement.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Infrastructure.Persistence.Configuration;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
	public void Configure(EntityTypeBuilder<Appointment> builder)
	{
		builder.HasKey(c => c.Id);

		builder.Property(c => c.Id)
			.ValueGeneratedNever();

		builder.Property(c => c.ClinicId);
		builder.Property(c => c.DoctorId);
		builder.Property(c => c.RoomId);
	}
}
