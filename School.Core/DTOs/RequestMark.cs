namespace School.API.DTOs
{
    public class RequestMark
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public decimal ExamMark { get; set; }
        public decimal AssignmentMark { get; set; }

    }
}
