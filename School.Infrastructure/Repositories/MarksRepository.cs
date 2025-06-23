
using System.Collections.Concurrent;
using School.API.DTOs;
using School.Core.Entities;
using School.Core.inerfaces;

namespace School.Infrastructure.Repositories
{
    public class MarksRepository : IMarksRepository
    {
        private  int _nextId = 1;

                 

        public bool Add(RequestMark rqmarks)
        {
            var generatedId = _nextId++;
            var Mark = new Mark
            {
                Id = generatedId,
                StudentId = rqmarks.StudentId,
                ClassId = rqmarks.ClassId,
                ExamMark = rqmarks.ExamMark,
                AssignmentMark = rqmarks.AssignmentMark
            };

            
            var isAdd = MarkStorage.Marks.TryAdd(generatedId.ToString(), Mark);// Implementation for adding marks
            return true; // Placeholder return value
        }
    }
    
    
}
