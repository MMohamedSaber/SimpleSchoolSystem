
using School.Core.DTOs;
using School.Core.Entities;
using School.Core.shared;

namespace School.Core.inerfaces
{
    public interface IStudentRepository
    {
        Student Add( CreateStudentRequest entity);
        IEnumerable<Student> GetAll(PaginationParams pagination);
        bool delete(string countryCode);
        bool update(string id, CreateStudentRequest entity);

        StudentReport StudentReport(string Id);

    }
}
