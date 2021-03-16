using Ardalis.GuardClauses;
using System;
using Timelogger.Domain.Abstractions;
using Timelogger.Domain.ValueObjects;

namespace Timelogger.Domain.Entities
{
    public class WorkEntry : BaseEntity<int>, IAuditableEntity
    {
        private WorkEntry() { }

        public WorkEntry(string title, string details, decimal workhours, DateTime date)
        {
            Title = Guard.Against.Null(title, nameof(title));
            Details = details;
            Hours = new WorkHours(workhours);
            Date = date;
        }

        public string Title { get; private set; }
        public string Details { get; private set; }
        public Guid ProjectId { get; private set; }
        public WorkHours Hours { get; private set; }
        public DateTime Date { get; private set; }
        public Audit Audit { get; private set; } = new Audit();
    }
}
