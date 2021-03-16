using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Domain.Abstractions;
using Timelogger.Domain.Entities;
using Timelogger.Domain.Specifications;
using Timelogger.Shared.Exceptions;

namespace Timelogger.Application.Commands.WorkEntries
{
    public class CreateWorkEntryCommandHandler : IRequestHandler<CreateWorkEntryCommand, int>
    {
        private readonly IGenericAsyncRepository<Project> _projectRepository;

        public CreateWorkEntryCommandHandler(IGenericAsyncRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<int> Handle(CreateWorkEntryCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetBySpecAsync(
                new ProjectWithWorkEntriesSpec(request.ProjectId), cancellationToken);
            if (project == null)
            {
                throw new NotFoundException(nameof(Project));
            }
            //I did this parsing here because I had no time to implement a proper numeric input box on the client
            if (!decimal.TryParse(request.Hours, out decimal parsedHours))
            {
                throw new BadRequestException("Hours must be a valid decimal");
            }
            var workEntry = new WorkEntry(request.Title,
                request.Description,
                parsedHours,
                request.Date);
            project.AddWorkEntry(workEntry);
            await _projectRepository.UpdateAsync(project, cancellationToken);
            return workEntry.Id;
        }
    }
}
