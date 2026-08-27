using Gym.API.Dtos;
using Gym.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.API.Services;


public class WorkoutService(GymContext dbContext)
{

    public async Task<List<Workout>> GetAllWorkoutsAsync()
    {
        return await dbContext.Workouts.ToListAsync();
    }
    public async Task<Workout> CreateWorkoutAsync(CreateWorkoutDto newWorkout)
    {
        Workout workout = new()
        {
            Name = newWorkout.Name.ToUpper(),


        };
        dbContext.Workouts.Add(workout);
        await dbContext.SaveChangesAsync();

        return workout;
    }

}