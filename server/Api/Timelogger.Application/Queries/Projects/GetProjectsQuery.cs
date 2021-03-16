using MediatR;
using System.Collections.Generic;
using Timelogger.Application.Dto;

namespace Timelogger.Application.Queries.Projects
{
    public class GetProjectsQuery : IRequest<IEnumerable<ProjectDto>>
    {
    }
}
