
using School.Core.Entities;
using School.Core.shared;

namespace School.Core.inerfaces
{
    public interface IClassRepository
    {
        bool add(Class  c);
        bool Delete(string id);
       // IEnumerable<Class> GetAll();
        PaginatedResult<Class> GetAll(PaginationParams pagination);
        double GetAverageMarks(string classId);
    }
}
