using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timelogger.Domain.Entities;

namespace Timelogger.Infrastructure.Data.Configurations
{
    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");
            builder.HasKey(c => c.Id);
            builder.OwnsOne(c => c.Address, OwnedConfigs.AddressConfiguration<Client>());
            builder.OwnsOne(c => c.Name, OwnedConfigs.NameConfiguration<Client>());
        }
    }
}
