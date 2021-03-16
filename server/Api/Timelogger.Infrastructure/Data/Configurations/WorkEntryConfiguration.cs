using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timelogger.Domain.Entities;

namespace Timelogger.Infrastructure.Data.Configurations
{
    internal class WorkEntryConfiguration : IEntityTypeConfiguration<WorkEntry>
    {
        public void Configure(EntityTypeBuilder<WorkEntry> builder)
        {
            builder.ToTable("WorkEntries");
            builder.HasKey(we => we.Id);
            builder.Property(we => we.Title)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(we => we.Details)
                .IsRequired()
                .HasMaxLength(500);
            builder.OwnsOne(we => we.Audit, OwnedConfigs.AuditConfiguration<WorkEntry>());
            builder.OwnsOne(we => we.Hours, OwnedConfigs.WorkHoursConfiguration<WorkEntry>());
        }
    }
}
