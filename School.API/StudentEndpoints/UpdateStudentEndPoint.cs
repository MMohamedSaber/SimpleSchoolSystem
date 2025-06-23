using FastEndpoints;
using School.Core.Entities;
using School.Core.inerfaces;
using School.Core.shared;

public class UpdateStudentEndpoint : Endpoint<CreateStudentRequest,string>
{
    private readonly IStudentRepository _studentRepository;

    public UpdateStudentEndpoint(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public override void Configure()
    {
        Put("/api/student/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateStudentRequest req, CancellationToken ct)
    {
        var id = Route<string>("id");

        var isUpdated = _studentRepository.update(id, req);

        if (isUpdated)
        {
            await SendOkAsync("updated successfully",ct); 
        }
        else
        {
            await SendNotFoundAsync(ct); 
        }
    }
}
