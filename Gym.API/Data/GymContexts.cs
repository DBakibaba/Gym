using Gym.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.API;

public class GymContext(DbContextOptions<GymContext> options) : DbContext(options)
{
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<Workout> Workouts => Set<Workout>();
}

