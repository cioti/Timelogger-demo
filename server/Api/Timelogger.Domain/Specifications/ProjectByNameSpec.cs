using Ardalis.Specification;
using Timelogger.Domain.Entities;

namespace Timelogger.Domain.Specifications
{
    public class ProjectByNameSpec : Specification<Project>
    {
        public ProjectByNameSpec(string name)
        {
            Query.Where(prj => prj.Name == name);
        }
    }
}
