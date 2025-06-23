using System.Collections.Concurrent;
using System.Collections.Generic;
using School.Core.Entities;
using School.Core.inerfaces;
using School.Core.shared;

namespace School.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ConcurrentDictionary<string, Student> _students = new();
        private int _nextId = 1;
        public StudentRepository()
        {
            
        }
        public Student Add(CreateStudentRequest entity)
        {
            var generatedId = _nextId++;

            var student = new Student
            {
                Id = Convert.ToInt32(generatedId),
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Age = entity.Age
            };

            if (_students.ContainsKey(generatedId.ToString()))
                return null;

            if (_students.TryAdd(student.Id.ToString(), student))
                return student;
            
            return null;
        }

        

       

        public bool delete(string id)
        {
            return _students.TryRemove(id, out _);
        }

       

        public bool update(string id, CreateStudentRequest entity)
        {
            var student = new Student
            {
               
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Age = entity.Age
            };

            if (_students.ContainsKey(id))
            {
                _students.TryUpdate(id, student, _students[id]);
                return true;
            }

            return false;
        }

        public IEnumerable<Student> GetAll(PaginationParams pagination)
        {
            var query = _students.Values.AsEnumerable();

            
            if (!string.IsNullOrWhiteSpace(pagination.Name))
            {
                query = query.Where(s =>
                    s.FirstName.Contains(pagination.Name, StringComparison.OrdinalIgnoreCase));
            }

            // Pagination
            query = query
                .OrderBy(s => s.FirstName) 
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize);

            return query;
        }
    }
}
