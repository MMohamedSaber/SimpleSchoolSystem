using FastEndpoints;
using FastEndpoints.Swagger;
using School.API.Middleware;
using School.Core.inerfaces;
using School.Infrastructure.Repositories;

var bld = WebApplication.CreateBuilder();

// 1.Add services
bld.Services.AddFastEndpoints();
bld.Services.SwaggerDocument(); 
bld.Services.AddSingleton<IStudentRepository, StudentRepository>();
bld.Services.AddSingleton<IClassRepository, ClassRepository>();
bld.Services.AddSingleton<IMarksRepository, MarksRepository>();
bld.Services.AddSingleton<IEnrollmentRepository, EnrollmentRepository>();

var app = bld.Build();

// 2.Configure middleware
app.UseMiddleware<globalhandlingExeption>(); 
app.UseFastEndpoints();
app.UseSwaggerGen();    
app.UseSwaggerUI();     

app.Run();
