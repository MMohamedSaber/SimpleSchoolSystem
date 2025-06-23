using System.Data.Common;
using FastEndpoints;
using School.API.Helper;
using School.Core.Entities;
using School.Core.inerfaces;
using School.Core.shared;

public class AddStudent : Endpoint<CreateStudentRequest, Student>
{
    
        private readonly IStudentRepository _studentRepository;

    public AddStudent(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public override void Configure()
        {
            Post("/api/student/add");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateStudentRequest student, CancellationToken ct)
        {
            
            if (student is null)
        {
            await SendErrorsAsync(400,ct);
            return;
        };
        var result = _studentRepository.Add(student);
            if (result is not null)
            {
                await SendAsync(result, StatusCodes.Status201Created, ct);
            }
            else
            {
                await SendErrorsAsync(400, ct);
            }

        }
    }

