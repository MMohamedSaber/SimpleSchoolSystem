
using School.Core.DTOs;
using School.Core.Entities;

namespace School.Core.inerfaces
{
    public interface IEnrollmentRepository
    {
        bool Add(RequestEnrollment reqEnroments);

    }
}
