using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using Timelogger.Domain.Abstractions;
using Timelogger.Domain.ValueObjects;
using Timelogger.Shared.Exceptions;

namespace Timelogger.Domain.Entities
{
    public class Project : BaseEntity<Guid>, IAggregateRoot, IAuditableEntity
    {
        private Project() { }
        public Project(string name, string description, Guid freelancerId, string clientFirstname,
            string clientLastname, DateTime startDate, DateTime? endDate = null)
        {
            if (endDate.HasValue && endDate <= startDate)
            {
                throw new ArgumentException("End date must be greater than start date");
            }
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            FreelancerId = freelancerId;
            Client = new Client(clientFirstname, clientLastname);
            _workEntries = new List<WorkEntry>();
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Audit Audit { get; private set; } = new Audit();
        public Guid FreelancerId { get; private set; }
        public Client Client { get; private set; }
        public Guid ClientId { get; private set; }

        private readonly List<WorkEntry> _workEntries;
        public IReadOnlyCollection<WorkEntry> WorkEntries => _workEntries.ToList(); // defensive copy

        public void AddWorkEntry(WorkEntry workEntry)
        {
            Guard.Against.Null(workEntry, nameof(workEntry));
            var sameDayEntries = _workEntries.Where(we => we.Date == workEntry.Date);
            var totalHours = sameDayEntries.Sum(sde => sde.Hours.Value);
            if (totalHours > 16)
            {
                throw new BadRequestException("Unable to add more than 16 hours of work in the same day. Go to bed!!");
            }
            _workEntries.Add(workEntry);
        }
    }
}
