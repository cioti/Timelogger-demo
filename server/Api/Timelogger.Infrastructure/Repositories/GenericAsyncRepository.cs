using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Domain.Abstractions;
using Timelogger.Domain.Entities;
using Timelogger.Infrastructure.Data;

namespace Timelogger.Infrastructure.Repositories
{
    public class GenericAsyncRepository<TEntity> : IGenericAsyncRepository<TEntity>
        where TEntity : BaseEntity, IAggregateRoot
    {
        private readonly ApiDbContext _dbContext;
        private readonly SpecificationEvaluator<TEntity> _specificationEvaluator;

        public GenericAsyncRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
            _specificationEvaluator = new SpecificationEvaluator<TEntity>();
        }

        public async Task<TEntity> FindAsync<TKey>(TKey id, CancellationToken cancellationToken = default)
        {
            return await SetContextEntity().FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<TEntity> GetBySpecAsync<Spec>(Spec specification, CancellationToken cancellationToken = default)
            where Spec : ISpecification<TEntity>
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<TResult> GetBySpecAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<TEntity>> ListBySpecAsync<Spec>(Spec specification,bool trackChanges = true, CancellationToken cancellationToken = default)
            where Spec : ISpecification<TEntity>
        {
            IQueryable<TEntity> query = ApplySpecification(specification);
            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await SetContextEntity().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return entity;
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            SetContextEntity().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private DbSet<TEntity> SetContextEntity()
        {
            return _dbContext.Set<TEntity>();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            return _specificationEvaluator.GetQuery(SetContextEntity().AsQueryable(), specification);
        }

        private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<TEntity, TResult> specification)
        {
            if (specification is null) throw new ArgumentNullException("Specification is required");
            if (specification.Selector is null) throw new SelectorNotFoundException();

            return _specificationEvaluator.GetQuery(SetContextEntity().AsQueryable(), specification);
        }
    }
}
