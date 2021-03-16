using System.Collections.Generic;

namespace Timelogger.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Address(string country, string city, string street, string zipCode)
        {
            Country = country;
            City = city;
            Street = street;
            ZipCode = zipCode;
        }

        public string Country { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Country;
            yield return City;
            yield return Street;
            yield return ZipCode;
        }
    }
}
