using Ardalis.GuardClauses;
using System.Collections.Generic;

namespace Timelogger.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = Guard.Against.Null(firstName, nameof(firstName));
            LastName = Guard.Against.Null(lastName, nameof(lastName));
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
