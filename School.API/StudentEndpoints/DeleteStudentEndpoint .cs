using FastEndpoints;
using School.API.Helper;
using School.Core.inerfaces;

public class DeleteStudentEndpoint : EndpointWithoutRequest<ResponseApi>
{
    private readonly IStudentRepository _studentRepository;

    public DeleteStudentEndpoint(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public override void Configure()
    {
        Delete("/api/student/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<string>("id");

        var isDeleted = _studentRepository.delete(id);

        if (isDeleted)
        {
            await SendAsync(  new ResponseApi(200,"Deleted Successfully")   );
        }
        else
        {
            await SendAsync(new ResponseApi(400, "Deleted Failed"));
        }
    }
}
