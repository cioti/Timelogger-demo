using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Domain.Abstractions;
using Timelogger.Domain.Entities;

namespace Timelogger.Infrastructure.Data
{
    public class ApiDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public ApiDbContext(DbContextOptions<ApiDbContext> options, ICurrentUserService currentUserService = null) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Project> Projects { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Audit.WasCreatedBy(_currentUserService?.UserId ?? "SYSTEM", DateTime.UtcNow);
                        break;
                    case EntityState.Modified:
                        entry.Entity.Audit.WasModifiedBy(_currentUserService?.UserId ?? "SYSTEM", DateTime.UtcNow);
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
