using FastEndpoints;
using School.Core.Entities;
using School.Core.inerfaces;
using School.Core.shared;

public class GetAllStudentsEndpoint : Endpoint<PaginationParams, List<Student>>
{
    private readonly IStudentRepository _studentRepository;

    public GetAllStudentsEndpoint(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public override void Configure()
    {
        Get("/api/student/getall");
        AllowAnonymous();
    }

    public override async Task HandleAsync(PaginationParams req, CancellationToken ct)
    {
        var result = _studentRepository.GetAll(req).ToList();

        await SendAsync(result, cancellation: ct);
    }
}
