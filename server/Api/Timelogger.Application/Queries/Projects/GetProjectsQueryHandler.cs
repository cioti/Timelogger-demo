using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Application.Dto;
using Timelogger.Domain.Abstractions;
using Timelogger.Domain.Entities;
using Timelogger.Domain.Specifications;

namespace Timelogger.Application.Queries.Projects
{
    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, IEnumerable<ProjectDto>>
    {
        private readonly IGenericAsyncRepository<Project> _projectRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetProjectsQueryHandler(IGenericAsyncRepository<Project> projectRepository, ICurrentUserService currentUserService)
        {
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var freelancerId = _currentUserService.UserId;

            var projects = await _projectRepository.ListBySpecAsync(new ProjectsForFreelancerSpec(Guid.Parse(freelancerId)), false, cancellationToken);

            return projects.Select(prj => new ProjectDto
            {
                Id = prj.Id,
                Name = prj.Name,
                Description = prj.Description,
                StartDate = prj.StartDate,
                EndDate = prj.EndDate,
                ClientFirstname = prj.Client.Name.FirstName,
                ClientLastname = prj.Client.Name.LastName,
            });
        }
    }
}
