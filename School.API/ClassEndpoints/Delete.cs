namespace School.API.ClassEndpoints
{
    using FastEndpoints;
    using global::School.Core.inerfaces;

    namespace School.API.ClassEndpoints
    {
        public class Delete : EndpointWithoutRequest
        {
            private readonly IClassRepository _classRepository;

            public Delete(IClassRepository classRepository)
            {
                _classRepository = classRepository;
            }

            public override void Configure()
            {
                Delete("/api/classes/delete/{id}");
                AllowAnonymous();
            }

            public override async Task HandleAsync(CancellationToken ct)
            {
                var id = Route<string>("id");

                var deleted = _classRepository.Delete(id);

                if (deleted)
                {
                    await SendAsync($"Class with ID '{id}' deleted successfully", 200, ct);
                }
                else
                {
                    AddError($"No class found with ID '{id}'.");
                    await SendErrorsAsync(404, ct);
                }
            }
        }
    }

}
