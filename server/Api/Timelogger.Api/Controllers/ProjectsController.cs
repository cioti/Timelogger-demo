using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Application.Commands.Projects;
using Timelogger.Application.Dto;
using Timelogger.Application.Queries.Projects;

namespace Timelogger.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProjectsController : BaseApiController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProjectCommand command, CancellationToken cancellationToken)
        {
            Guid projectId = await Mediator.Send(command, cancellationToken);
            return Created($"{Request.Path}/{projectId}", projectId);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ProjectDto>> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetProjectByIdQuery { Id = id }, cancellationToken));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> ListAsync(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetProjectsQuery(), cancellationToken));
        }
    }
}