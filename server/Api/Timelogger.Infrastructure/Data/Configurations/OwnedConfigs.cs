using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Timelogger.Domain.ValueObjects;

namespace Timelogger.Infrastructure.Data.Configurations
{
    internal class OwnedConfigs
    {
        internal static Action<OwnedNavigationBuilder<T, Audit>> AuditConfiguration<T>() where T : class
        {
            return builder =>
            {
                builder
                .Property(a => a.CreatedBy)
                .HasColumnName(nameof(Audit.CreatedBy))
                .IsRequired();
                builder
                .Property(a => a.CreatedDate)
                .HasColumnName(nameof(Audit.CreatedDate)).IsRequired();
                builder
                .Property(a => a.ModifiedBy)
                .HasColumnName(nameof(Audit.ModifiedBy));
                builder
                .Property(a => a.ModifiedDate)
                .HasColumnName(nameof(Audit.ModifiedDate));
            };
        }

        internal static Action<OwnedNavigationBuilder<T, WorkHours>> WorkHoursConfiguration<T>() where T : class
        {
            return builder =>
            {
                builder.Property(wh => wh.Value)
                .HasColumnName(nameof(WorkHours))
                .IsRequired();
            };
        }

        internal static Action<OwnedNavigationBuilder<T, Address>> AddressConfiguration<T>() where T : class
        {
            return builder =>
            {
                builder
                .Property(adr => adr.City)
                .IsRequired()
                .HasColumnName(nameof(Address.City));

                builder
                .Property(adr => adr.Country)
                .HasColumnName(nameof(Address.Country));

                builder
                .Property(adr => adr.Street)
                .HasColumnName(nameof(Address.Street));

                builder
                .Property(adr => adr.ZipCode)
                .IsRequired()
                .HasColumnName(nameof(Address.ZipCode));
            };
        }

        internal static Action<OwnedNavigationBuilder<T, Name>> NameConfiguration<T>() where T : class
        {
            return builder =>
            {
                builder
                .Property(n => n.FirstName)
                .IsRequired()
                .HasColumnName(nameof(Name.FirstName));

                builder
                .Property(n => n.LastName)
                .IsRequired()
                .HasColumnName(nameof(Name.LastName));
            };
        }
    }
}
