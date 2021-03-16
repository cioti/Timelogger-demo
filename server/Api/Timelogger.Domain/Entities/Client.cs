using Ardalis.GuardClauses;
using System;
using Timelogger.Domain.Abstractions;
using Timelogger.Domain.ValueObjects;

namespace Timelogger.Domain.Entities
{
    public class Client : BaseEntity<Guid>
    {
        private Client() { }
        public Client(string firstName, string lastName)
        {
            Name = new Name(firstName, lastName);
            Address = null;
        }
        public Client(Name name, Address address)
        {
            Name = Guard.Against.Null(name, nameof(name));
            Address = address;
        }

        public Name Name { get; private set; }
        public Address Address { get; private set; }
        public Guid FreelancerId { get; private set; }
        public Guid ProjectId { get; private set; }
    }
}
