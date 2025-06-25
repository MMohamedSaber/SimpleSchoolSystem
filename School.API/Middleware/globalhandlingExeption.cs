
using Microsoft.AspNetCore.Mvc;

namespace School.API.Middleware
{
    public class globalhandlingExeption
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<globalhandlingExeption> _logger;

        public globalhandlingExeption(RequestDelegate next, ILogger<globalhandlingExeption> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "an unhandled exception occurred: {message}", ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;

                var response = new ProblemDetails
                {
                    Status = context.Response.StatusCode,
                    Title = "Internal Server Error",
                    Detail = ex.Message, 
                    Instance = context.Request.Path
                };

                await context.Response.WriteAsJsonAsync(response);

            }
        }

       
    }
}