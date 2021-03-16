using System.Collections.Generic;
using System.Linq;

namespace Timelogger.Domain.Common
{
    public class DomainResult<T> : DomainResult
    {
        private DomainResult(string error) : base(error) { }
        private DomainResult(DomainResultStatus status) : base(status) { }
        private DomainResult(T entity) : base(DomainResultStatus.Ok)
        {
            Value = entity;
        }
        public T Value { get; private set; }

        public static DomainResult<T> Success(T entity) => new DomainResult<T>(entity);
        public static new DomainResult<T> NotFound() => new DomainResult<T>(DomainResultStatus.NotFound);
        public static new DomainResult<T> Forbidden() => new DomainResult<T>(DomainResultStatus.Forbidden);
        public static new DomainResult<T> Error(string error) => new DomainResult<T>(error);
        public override void AddError(string errorMessage)
        {
            Value = default;
            Errors.Add(errorMessage);
            Status = DomainResultStatus.Error;
        }
    }

    public class DomainResult
    {
        protected DomainResult(DomainResultStatus status)
        {
            Status = status;
            Errors = new List<string>();
        }
        protected DomainResult(string error)
        {
            Errors = new List<string>();
            Errors.Add(error);
            Status = DomainResultStatus.Error;
        }
        public bool IsValid => Status == DomainResultStatus.Ok && !Errors.Any();
        public DomainResultStatus Status { get; protected set; }
        public List<string> Errors { get; } 

        public static DomainResult Success() => new DomainResult(DomainResultStatus.Ok);
        public static DomainResult NotFound() => new DomainResult(DomainResultStatus.NotFound);
        public static DomainResult Forbidden() => new DomainResult(DomainResultStatus.Forbidden);
        public static DomainResult Error(string error) => new DomainResult(error);
        public virtual void AddError(string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
            {
                Errors.Add(error);
                Status = DomainResultStatus.Error;
            }
        }
        public void AddErrors(List<string> errors)
        {
            if (errors.Any())
            {
                Errors.AddRange(errors);
                Status = DomainResultStatus.Error;
            }
        }
    }
}
