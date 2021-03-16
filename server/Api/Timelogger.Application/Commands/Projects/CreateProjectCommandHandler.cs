using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Domain.Abstractions;
using Timelogger.Domain.Entities;
using Timelogger.Domain.Specifications;
using Timelogger.Shared.Exceptions;

namespace Timelogger.Application.Commands.Projects
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid>
    {
        private readonly IGenericAsyncRepository<Project> _projectRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateProjectCommandHandler(IGenericAsyncRepository<Project> projectRepository, ICurrentUserService currentUserService)
        {
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
        }


        public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetBySpecAsync(new ProjectByNameSpec(request.Name), cancellationToken);
            if (project != null)
            {
                throw new BadRequestException("There is already a project with this name.");
            }

            //This should actually be the id of the freelancer entity, but I had no time to make the system as I wanted
            //and for now we just use the userid
            var freelancerId = _currentUserService.UserId;

            project = new Project(request.Name,
                request.Description,
                Guid.Parse(freelancerId),
                request.ClientFirstname,
                request.ClientLastname,
                request.StartDate,
                request.EndDate);

            await _projectRepository.AddAsync(project, cancellationToken);

            return project.Id;
        }
    }
}
