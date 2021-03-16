using MediatR;
using System;
using Timelogger.Application.Dto;

namespace Timelogger.Application.Queries.Projects
{
    public class GetProjectByIdQuery : IRequest<ProjectDto>
    {
        public Guid Id { get; set; }
    }
}
