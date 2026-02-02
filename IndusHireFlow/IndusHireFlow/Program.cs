using Business.Interfaces;
using Business.Services;
using Business.Data;
using Business.DataInterface.RepositoryInterfaces;
using Business.Data;
using Business.DataInterface.RepositoryInterfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Register application services
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<ICandidateRepository, Business.Data.CandidateRepository>();
// Register Dapper context (uses IConfiguration)
builder.Services.AddSingleton<DapperContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "IndusHireFlow API",
        Version = "v1"
    });
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "IndusHireFlow v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
