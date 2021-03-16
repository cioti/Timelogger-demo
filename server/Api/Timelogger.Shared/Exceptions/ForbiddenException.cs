using System;
using System.Collections.Generic;
using System.Text;

namespace Timelogger.Shared.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) : base(message) { }
    }
}
