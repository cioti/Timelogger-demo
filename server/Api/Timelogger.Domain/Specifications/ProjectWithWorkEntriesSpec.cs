using Ardalis.Specification;
using System;
using Timelogger.Domain.Entities;

namespace Timelogger.Domain.Specifications
{
    public class ProjectWithWorkEntriesSpec : Specification<Project>
    {
        public ProjectWithWorkEntriesSpec(Guid id)
        {
            Query.Include(prj => prj.WorkEntries)
                .Where(prj => prj.Id == id);
        }
    }
}
