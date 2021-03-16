using Timelogger.Domain.ValueObjects;

namespace Timelogger.Domain.Abstractions
{
    public interface IAuditableEntity
    {
        public Audit Audit { get; }
    }
}
