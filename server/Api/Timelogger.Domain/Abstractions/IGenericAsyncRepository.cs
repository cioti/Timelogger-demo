using Ardalis.Specification;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Timelogger.Domain.Abstractions
{
    public interface IGenericAsyncRepository<TEntity>
    {
        Task<TEntity> FindAsync<TKey>(TKey id, CancellationToken cancellationToken = default);
        Task<TEntity> GetBySpecAsync<Spec>(Spec specification, CancellationToken cancellationToken = default)
            where Spec : ISpecification<TEntity>;
        Task<TResult> GetBySpecAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
         Task<IReadOnlyList<TEntity>> ListBySpecAsync<Spec>(Spec specification,bool trackChanges = true, CancellationToken cancellationToken = default)
            where Spec : ISpecification<TEntity>;
    }
}
