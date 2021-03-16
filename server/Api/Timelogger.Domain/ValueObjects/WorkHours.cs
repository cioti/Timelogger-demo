using System;
using System.Collections.Generic;

namespace Timelogger.Domain.ValueObjects
{
    public class WorkHours : ValueObject
    {
        public WorkHours(decimal value)
        {
            if(value < 0)
            {
                throw new ArgumentException("Cannot add negative work hours");
            }
            if(value % 0.5M != 0 || CountDigitsAfterDecimalPoint(value) > 1)
            {
                throw new ArgumentException("Value for workhours must be a minimum of 0.5 and cannot contain more than 1 number after decimal point");
            }
            Value = value;
        }

        public decimal Value { get; private set; }

        private int CountDigitsAfterDecimalPoint(decimal value)
        {
            return BitConverter.GetBytes(decimal.GetBits(value)[3])[2];
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
