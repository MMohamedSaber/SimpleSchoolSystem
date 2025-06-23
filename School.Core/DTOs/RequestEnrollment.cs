
namespace School.Core.DTOs
{
    public class RequestEnrollment
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
