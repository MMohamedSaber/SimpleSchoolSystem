using FastEndpoints;
using School.Core.Entities;
using School.Core.inerfaces;
using School.Core.shared;

namespace School.API.ClassEndpoints
{
    public class GetAll : Endpoint<PaginationParams, PaginatedResult<Class>>
    {
        private readonly IClassRepository _classRepository;

        public GetAll(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public override void Configure()
        {
            Get("/api/classes/all");
            AllowAnonymous();
        }

        public override async Task HandleAsync(PaginationParams req, CancellationToken ct)
        {
            var result = _classRepository.GetAll(req);
            await SendAsync(result, cancellation: ct);
        }
    }
}
