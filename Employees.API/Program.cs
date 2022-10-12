using Employees.API.Data;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// json
builder.Services.Configure<JsonOptions>(options =>
         options.SerializerOptions.DefaultIgnoreCondition
   = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull);


// Inject DbContext
builder.Services.AddDbContext<EmployeeDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("EmployeesDbConnectionString")));

//builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
