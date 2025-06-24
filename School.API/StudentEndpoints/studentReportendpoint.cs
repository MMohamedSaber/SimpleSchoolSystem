using System.Net;
using FastEndpoints;
using School.API.Helper;
using School.Core.DTOs;
using School.Core.inerfaces;

namespace School.API.StudentEndpoints
{
    public class studentReportendpoint : EndpointWithoutRequest<StudentReport>
    {
        private readonly IStudentRepository studentRepository;

        public studentReportendpoint(IStudentRepository studentrepo)
        {
            studentRepository = studentrepo;
        }

        public override void Configure()
        {
            Get("/api/students/{StudentId}/report-student");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var studentId = Route<string>("StudentId");

            var studentReport = studentRepository.StudentReport(studentId);

            if (studentReport != null)
            {
                await SendNotFoundAsync(); 
                return;
            }
           
                await SendAsync(studentReport);
        }
    }
}