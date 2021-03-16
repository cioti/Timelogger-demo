using MediatR;
using System;
using System.Collections.Generic;
using Timelogger.Application.Dto;

namespace Timelogger.Application.Queries.WorkEntries
{
    public class GetWorkEntriesForProjectQuery :IRequest<IEnumerable<WorkEntryDto>>
    {
        public Guid ProjectId { get; set; }
    }
}
