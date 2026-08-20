using Gym.API;
using Gym.API.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<GymContext>(options => options.UseSqlite("Data Source=gym.db"));

var app = builder.Build();
app.MapExerciseEndpoints();

app.Run();
