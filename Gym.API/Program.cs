using Gym.API;
using Gym.API.Endpoints;
using Gym.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddValidation();
builder.Services.AddDbContext<GymContext>(options => options.UseSqlite("Data Source=gym.db"));

builder.Services.AddScoped<ExerciseService>();


var app = builder.Build();
app.MapExerciseEndpoints();

app.Run();
