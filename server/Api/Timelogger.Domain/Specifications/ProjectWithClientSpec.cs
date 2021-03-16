using Ardalis.Specification;
using System;
using Timelogger.Domain.Entities;

namespace Timelogger.Domain.Specifications
{
    public class ProjectWithClientSpec : Specification<Project>
    {
        public ProjectWithClientSpec(Guid id)
        {
            Query.Include(prj => prj.Client)
                .Where(prj => prj.Id == id);
        }
    }
}
