using DataAccess.Models;

using WebAPI;
using WebAPI.Services.Implementations;
using WebAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped<ILinkedRepository<Company>, CompanyRepository>();
builder.Services.AddScoped<ILinkedRepository<Employee>, EmployeeRepository>();
builder.Services.AddScoped<IGenericRepository<Project>, GenericRepository<Project>>();

builder.Services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling
                                                               = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddCors(options => options.AddPolicy("AllowOrigin", builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowOrigin");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
