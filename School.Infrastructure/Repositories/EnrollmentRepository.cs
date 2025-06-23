
using System.Collections.Concurrent;
using System.Collections.Generic;
using School.Core.DTOs;
using School.Core.Entities;
using School.Core.inerfaces;

namespace School.Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ConcurrentDictionary<string, Enrollment> _enrollment = new();
        private int _nextId = 1;
        public bool Add(RequestEnrollment reqEnroments)
        {
            var id = _nextId++;
            var enrollment = new Enrollment
            {
                Id = id,
                StudentId = reqEnroments.StudentId,
                ClassId = reqEnroments.ClassId,
                EnrollmentDate = reqEnroments.EnrollmentDate
            };

            if (_enrollment.ContainsKey(id.ToString()))
                return false;

            if (_enrollment.TryAdd(enrollment.Id.ToString(), enrollment))
                return true ;

            return false;

        }
    }
}
