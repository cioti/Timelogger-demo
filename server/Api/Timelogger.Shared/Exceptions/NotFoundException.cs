using System;

namespace Timelogger.Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string notFoundResource = "") :
            base(string.IsNullOrWhiteSpace(notFoundResource) ? $"{notFoundResource} resource not found." : "Resource not found.")
        {
        }
    }
}
