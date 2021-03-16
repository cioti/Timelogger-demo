using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Application.Dto;
using Timelogger.Domain.Abstractions;
using Timelogger.Domain.Entities;
using Timelogger.Domain.Specifications;
using Timelogger.Shared.Exceptions;

namespace Timelogger.Application.Queries.Projects
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto>
    {
        private readonly IGenericAsyncRepository<Project> _projectRepository;

        public GetProjectByIdQueryHandler(IGenericAsyncRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetBySpecAsync(new ProjectWithClientSpec(request.Id), cancellationToken);
            if (project == null)
            {
                throw new NotFoundException();
            }

            return new ProjectDto
            {
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                ClientFirstname = project.Client.Name.FirstName,
                ClientLastname = project.Client.Name.LastName,
            };
        }
    }
}
