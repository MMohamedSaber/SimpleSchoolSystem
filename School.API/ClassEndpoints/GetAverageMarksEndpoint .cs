using FastEndpoints;
using School.API.Helper;
using School.Core.inerfaces;

public class GetAverageMarksEndpoint : EndpointWithoutRequest
{
    private readonly IClassRepository _classRepository;

    public GetAverageMarksEndpoint(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public override void Configure()
    {
        Get("/api/class/{classId}/average-marks");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var classId = Route<string>("classId");

        var average = _classRepository.GetAverageMarks(classId);

        if (average == 0)
        {
            await SendAsync(new ResponseApi( 400,"Class not found or has no marks." ));
            return;
        }

        await SendAsync(new
        {
            classId,
            averageTotalMarks = average
        }, 200, ct);
    }
}
