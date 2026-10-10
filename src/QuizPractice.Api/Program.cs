using System.Text.Json.Serialization;
using QuizPractice.Application;
using QuizPractice.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("QuizPracticeDB")
    ?? throw new InvalidOperationException(
        "Connection string 'QuizPracticeDB' is missing. Copy appsettings.Development.example.json "
        + "to appsettings.Development.json and set your SQL Server (see README).");

builder.Services
    .AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(connectionString, builder.Environment.WebRootPath
    ?? Path.Combine(builder.Environment.ContentRootPath, "wwwroot"));

var app = builder.Build();

// Unhandled exceptions become a ProblemDetails 500 without the stack trace.
app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

// Serves /uploads; the React build will be served from here too.
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();

app.Run();
