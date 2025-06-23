using FastEndpoints;
using School.Core.Entities;
using School.Core.inerfaces;

namespace School.API.ClassEndpoints
{
    public class Create : Endpoint<Class>
    {
        private readonly IClassRepository _classRepository;

        public Create(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }


        public override void Configure()
        {
            Post("/api/classes/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Class req, CancellationToken ct)
        {
           

            var added = _classRepository.add(req);

            if (added)
            {
                await SendAsync("Class added successfully", 201, ct);
            }
            else
            {
                AddError("Class with the same ID already exists.");
                await SendErrorsAsync(400, ct);
            }
        }
    }
}
