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
using Timelogger.Shared.Exceptions;

namespace Timelogger.Application.Queries.WorkEntries
{
    public class GetWorkEntriesForProjectQueryHandler : IRequestHandler<GetWorkEntriesForProjectQuery, IEnumerable<WorkEntryDto>>
    {
        private readonly IGenericAsyncRepository<Project> _projectRepository;

        public GetWorkEntriesForProjectQueryHandler(IGenericAsyncRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<IEnumerable<WorkEntryDto>> Handle(GetWorkEntriesForProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetBySpecAsync(
                new ProjectWithWorkEntriesSpec(request.ProjectId), cancellationToken);
            if (project == null)
            {
                throw new NotFoundException(nameof(Project));
            }

            return project.WorkEntries.Select(we => new WorkEntryDto
            {
                Id = we.Id,
                Title = we.Title,
                Details = we.Details,
                Hours = we.Hours.Value,
                Date = we.Date
            });
        }
    }
}
