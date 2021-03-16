using FluentValidation;

namespace Timelogger.Application.Commands.Projects
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x => x.ClientFirstname).NotNull().NotEmpty();
            RuleFor(x => x.ClientLastname).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.StartDate).NotNull();
        }
    }
}
