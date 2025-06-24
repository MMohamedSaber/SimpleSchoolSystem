using System.Collections.Concurrent;
using School.Core.DTOs;
using School.Core.Entities;
using School.Core.inerfaces;
using School.Core.shared;
using School.Infrastructure.SharedStorage;

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

        public StudentReport StudentReport(string Id)
        {
            if (!int.TryParse(Id, out int id))
                return null;

            var enrollments = EnrolledStorage._enrollment.Values.Where(s => s.StudentId == id).ToList();

            var enrolledClasses = enrollments
                .Select(e => ClassStorage._classes[e.ClassId])
                .ToList();

            var allmarks = MarkStorage.Marks.Values.Where(m => m.StudentId == id).ToList();

            decimal average = 0;
            average = allmarks.Average(m => m.AssignmentMark + m.ExamMark);

            var classReports = enrollments.Select(e =>
            {
                var cls = ClassStorage._classes[e.ClassId];
                var mark = allmarks.FirstOrDefault(m => m.ClassId == cls.Id);

                return new ResponseClass
                {
                    Name = cls.Name,
                    Teacher = cls.Teacher,
                    Description = cls.Description,
                    marks =  mark.AssignmentMark + mark.ExamMark 
                };
            }).ToList();

            return new StudentReport
            {
                Classes = classReports,

                averagemarks = average,
                FullName = $"{_students[Id.ToString()].FirstName} {_students[Id.ToString()].LastName}"
            };

        }
    }
}
