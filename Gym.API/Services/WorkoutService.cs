using Gym.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.API.Services;


public class WorkoutService(GymContext dbContext)
{

    public async Task<List<Workout>> GetAllWorkoutsAsync()
    {
        return await dbContext.Workouts.ToListAsync();
    }


}