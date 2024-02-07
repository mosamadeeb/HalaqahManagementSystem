using HalaqahAPI.Context;
using HalaqahAPI.Helpers;
using HalaqahAPI.Models;
using HalaqahAPI.Repository;
using HalaqahAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HalaqahContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HalaqahContext")));

builder.Services.AddScoped<IRepository<Student>, GenericRepository<Student>>();
builder.Services.AddScoped<IRepository<StudentAttendance>, GenericRepository<StudentAttendance>>();
builder.Services.AddScoped<StudentService>();

builder.Services.AddScoped<EntityHelper>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
