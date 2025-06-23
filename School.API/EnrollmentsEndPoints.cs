using FastEndpoints;
using School.API.DTOs;
using School.API.Helper;
using School.Core.DTOs;
using School.Core.inerfaces;

namespace School.API
{
    public class EnrollmentsEndPoints : Endpoint<RequestEnrollment,ResponseApi>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentsEndPoints(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public override void Configure()
        {
            Post("/api/enrollments/enroll");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RequestEnrollment enrollments, CancellationToken ct)
        {

            var result = _enrollmentRepository.Add(enrollments);
            if (result == true)
            {
                await SendAsync(new ResponseApi(200, "Enrolled Succesfully"));
            }
            else
            {
                await SendErrorsAsync(400);
            }

        }
    }
}

