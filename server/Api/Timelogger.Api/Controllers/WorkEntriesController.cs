using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Application.Commands.WorkEntries;
using Timelogger.Application.Dto;
using Timelogger.Application.Queries.WorkEntries;

namespace Timelogger.Api.Controllers
{
    public class WorkEntriesController : BaseApiController
    {

        [HttpPost("/api/v1/projects/{projectId:guid}/work-entries")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateAsync([FromRoute] Guid projectId,
            [FromBody] CreateWorkEntryCommand command, CancellationToken cancellationToken)
        {
            command.ProjectId = projectId;
            int workEntryId = await Mediator.Send(command, cancellationToken);
            return Created($"{Request.Path}/{workEntryId}", workEntryId);
        }

        [HttpGet("/api/v1/projects/{projectId:guid}/work-entries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<IEnumerable<WorkEntryDto>>> ListAsync([FromRoute] Guid projectId, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetWorkEntriesForProjectQuery { ProjectId = projectId }, cancellationToken));
        }
    }
}
