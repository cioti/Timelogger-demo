using Ardalis.Specification;
using System;
using Timelogger.Domain.Entities;

namespace Timelogger.Domain.Specifications
{
    public class ProjectsForFreelancerSpec : Specification<Project>
    {
        public ProjectsForFreelancerSpec(Guid freelancerId)
        {
            Query.AsNoTracking()
                .Include(prj => prj.Client)
                .Where(prj => prj.FreelancerId == freelancerId);
        }
    }
}
