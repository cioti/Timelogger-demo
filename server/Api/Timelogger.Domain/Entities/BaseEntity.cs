
namespace Timelogger.Domain.Entities
{
    /// <summary>
    /// Marker class for database entity
    /// </summary>
    /// <typeparam name=""></typeparam>
    public class BaseEntity<TKey> : BaseEntity
    {
        public TKey Id { get; protected set; }
    }

    /// <summary>
    /// Marker class for database entity
    /// </summary>
    public class BaseEntity { }
}
