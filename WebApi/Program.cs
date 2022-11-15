using Data.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PersonalBlogDbContext>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();