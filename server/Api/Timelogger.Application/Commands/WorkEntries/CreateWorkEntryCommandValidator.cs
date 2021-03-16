using FluentValidation;

namespace Timelogger.Application.Commands.WorkEntries
{
    public class CreateWorkEntryCommandValidator : AbstractValidator<CreateWorkEntryCommand>
    {
        public CreateWorkEntryCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}
