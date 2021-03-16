using MediatR;
using Newtonsoft.Json;
using System;

namespace Timelogger.Application.Commands.WorkEntries
{
    public class CreateWorkEntryCommand : IRequest<int>
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Hours { get; set; }
        public DateTime Date { get; set; }
    }
}
