using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timelogger.Domain.Entities;

namespace Timelogger.Infrastructure.Data.Configurations
{
    internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.Description)
                .HasMaxLength(500)
                .IsRequired(false);
            builder.Property(p => p.StartDate).IsRequired();
            builder.OwnsOne(p => p.Audit, OwnedConfigs.AuditConfiguration<Project>());
            builder.HasMany(p => p.WorkEntries).WithOne().HasForeignKey(we => we.ProjectId);
            builder.HasOne(p => p.Client).WithMany().HasForeignKey(p => p.ClientId);
        }
    }
}
