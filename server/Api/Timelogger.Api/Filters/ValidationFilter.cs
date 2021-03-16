using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using Timelogger.Shared.Exceptions;
using Timelogger.Shared.Models;

namespace Timelogger.Api.Filters
{
     public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => new ValidationError(e.ErrorMessage))
                                 .ToList();

                throw new ValidationException(errors);
            }
        }
    }
}
