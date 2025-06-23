using FastEndpoints;
using School.API.DTOs;
using School.API.Helper;
using School.Core.inerfaces;

namespace School.API.MarkEndpoints
{
    public class Record:Endpoint<RequestMark, ResponseApi>
    {
        private readonly IMarksRepository _marksRepository;

        public Record(IMarksRepository marksRepository)
        {
            _marksRepository = marksRepository;
        }

        public override void Configure()
        {
            Post("/api/marks/record");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RequestMark marks, CancellationToken ct)
        {

            var result = _marksRepository.Add(marks);
            if (result ==true)
            {
                await SendAsync(new ResponseApi(200,"Recorded Succesfully"));
            }
            else
            {
                await SendErrorsAsync(400);
            }

        }
    }
}
