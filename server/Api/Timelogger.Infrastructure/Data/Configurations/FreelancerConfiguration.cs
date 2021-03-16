using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timelogger.Domain.Entities;

namespace Timelogger.Infrastructure.Configurations
{
    //internal class FreelancerConfiguration : IEntityTypeConfiguration<Freelancer>
    //{
    //    public void Configure(EntityTypeBuilder<Freelancer> builder)
    //    {
    //        builder.ToTable("Freelancers");
    //        builder.HasKey(f => f.Id);
    //        builder.OwnsOne(f => f.Address, OwnedConfigs.AddressConfiguration<Freelancer>());
    //        builder.OwnsOne(f => f.Name, OwnedConfigs.NameConfiguration<Freelancer>());
    //        builder.OwnsOne(f => f.Audit, OwnedConfigs.AuditConfiguration<Freelancer>());
    //        builder.HasMany(f => f.Clients).WithOne().HasForeignKey(c => c.FreelancerId);
    //        builder.HasMany(f => f.Projects).WithOne().HasForeignKey(p => p.FreelancerId);
    //    }
    //}
}
