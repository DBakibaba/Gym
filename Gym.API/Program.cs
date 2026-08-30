using Gym.API;
using Gym.API.Endpoints;
using Gym.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();


builder.Services.AddDbContext<GymContext>(options => options.UseSqlite("Data Source=gym.db"));
builder.Services.AddScoped<ExerciseService>();
builder.Services.AddScoped<WorkoutService>();

var app = builder.Build();

app.MapExerciseEndpoints();
app.MapWorkoutEndpoints();
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        await Results.Problem(
            statusCode: 500,
            title: "An unexpected error occured."
        ).ExecuteAsync(context);
    });
});



app.Run();
