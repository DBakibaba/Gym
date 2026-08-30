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


    public async Task<Workout?> GetAllWorkoutsWithExerciseAsync(int id)
    {
        return await dbContext.Workouts.Include(workout => workout.Exercise).FirstOrDefaultAsync(workout => workout.Id == id);
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

    public async Task<Workout?> GetWorkoutByIdAsync(int id)
    {
        var workout = await dbContext.Workouts.FindAsync(id);
        return workout;

    }
    public async Task<Workout?> UpdateWorkoutAsync(int id, UpdateWorkoutDto updateWorkout)
    {
        var existingWorkout = await dbContext.Workouts.FindAsync(id);
        if (existingWorkout is null)
        {
            return null;
        }
        existingWorkout.Name = updateWorkout.Name;
        await dbContext.SaveChangesAsync();

        return existingWorkout;
    }
    public async Task<bool> DeleteWorkoutAsync(int id)
    {
        var workout = await dbContext.Workouts.FindAsync(id);
        if (workout is null)
        {
            return false;
        }
        dbContext.Workouts.Remove(workout);
        await dbContext.SaveChangesAsync();
        return true;
    }

}