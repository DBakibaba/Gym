using Gym.API.Dtos;
using Gym.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.API.Services;


public class WorkoutService(GymContext dbContext, ILogger<WorkoutService> logger)
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
            logger.LogWarning("Workut {WorkoutId} is not found for updating workout.", id);
            return null;
        }
        existingWorkout.Name = updateWorkout.Name;
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Updated workout {WorkoutId}", id);
        return existingWorkout;
    }
    public async Task<bool> DeleteWorkoutAsync(int id)
    {
        var workout = await dbContext.Workouts.FindAsync(id);
        if (workout is null)
        {
            logger.LogWarning("workout {workoutId} was not found for deleting.", id);
            return false;
        }
        dbContext.Workouts.Remove(workout);
        await dbContext.SaveChangesAsync();
        return true;
    }

}