using School.Core.Entities;
using School.Core.inerfaces;
using School.Core.shared;
using School.Infrastructure.SharedStorage;

namespace School.Infrastructure.Repositories
{
    public class ClassRepository : IClassRepository
    {
        //private readonly ConcurrentDictionary<string, Mark> _marks = new();




        public bool add(Class c)
        {
            if (ClassStorage._classes.Any(cl => cl.Id == c.Id))
                return false;

            ClassStorage._classes.Add(c);
            return true;
        }
        public bool Delete(string id)
        {
            var existing = ClassStorage._classes.FirstOrDefault(c => c.Id.ToString() == id);
            if (existing == null)
                return false;

            ClassStorage._classes.Remove(existing);
            return true;
        }
        public IEnumerable<Class> GetAll()
        {
            return ClassStorage._classes;
        }

        public PaginatedResult<Class> GetAll(PaginationParams pagination)
        {
            var query = ClassStorage._classes.AsEnumerable();

           
            if (!string.IsNullOrWhiteSpace(pagination.Name))
            {
                query = query.Where(c => c.Name.Contains(pagination.Name, StringComparison.OrdinalIgnoreCase));
            }

            var totalCount = query.Count();

            var items = query
                .OrderBy(c => c.Name)
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();

            return new PaginatedResult<Class>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }
        public double GetAverageMarks(string classId)
        {
            if (!int.TryParse(classId, out int classIdInt))
                return 0;

            var classMarks = MarkStorage.Marks.Values.Where(m => m.ClassId == classIdInt) .ToList();

            if (!classMarks.Any())
                return 0; 

            return (double)classMarks.Average(m => m.ExamMark + m.AssignmentMark);
        }
    }
}
