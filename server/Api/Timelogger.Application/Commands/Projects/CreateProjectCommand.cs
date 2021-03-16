using MediatR;
using System;

namespace Timelogger.Application.Commands.Projects
{
    public class CreateProjectCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClientFirstname { get; set; }
        public string ClientLastname { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
