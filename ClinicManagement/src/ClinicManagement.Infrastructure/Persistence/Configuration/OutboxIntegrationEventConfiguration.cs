//using ClinicManagement.Infrastructure.IntegrationEvents;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace ClinicManagement.Infrastructure.Persistence.Configuration;

//public class OutboxIntegrationEventConfiguration : IEntityTypeConfiguration<OutboxIntegrationEvent>
//{
//	public void Configure(EntityTypeBuilder<OutboxIntegrationEvent> builder)
//	{
//		builder
//			.Property<int>("Id")
//			.ValueGeneratedOnAdd();

//		builder.HasKey("Id");

//		builder.Property(o => o.EventName);

//		builder.Property(o => o.EventContent);
//	}
//}
