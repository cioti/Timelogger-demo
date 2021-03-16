using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timelogger.Shared.Models;

namespace Timelogger.Shared.Exceptions
{
    public class ValidationException : Exception
    {
        private const string DefaultMessage = "Validation errors occured.";
        public IEnumerable<ValidationError> Errors { get; }
        public ValidationException(IEnumerable<ValidationError> errors) : base(DefaultMessage)
        {
            Errors = errors;
        }

        public ValidationException(ValidationError error) : base(DefaultMessage)
        {
            Errors = new List<ValidationError> { error };
        }

        public ValidationException(string propertyName, string reason) : base(DefaultMessage)
        {
            Errors = new List<ValidationError> { new ValidationError(propertyName, reason) };
        }

        public ValidationException(string reason) : base(DefaultMessage)
        {
            Errors = new List<ValidationError> { new ValidationError(reason) };
        }

        public override string ToString()
        {
            string message = string.Join("; ", Errors.Select(e => e.ToString()));
            return message + base.ToString();
        }
    }
}
