using Gym.API;
using Gym.API.Dtos;
using Gym.API.Services;
using Gym.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

public class ExerciseServiceTests
{
    [Fact]
    public async Task CreateExerciseAsync_WorkoutDoesNotExist_ReturnsNull()
    {
        var options = new DbContextOptionsBuilder<GymContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        using var dbContext = new GymContext(options);

        var logger = NullLogger<ExerciseService>.Instance;

        var service = new ExerciseService(dbContext, logger);

        var newExercise = new CreateExerciseDto(
            "Bench Press",
            10, 999
        );

        var result = await service.CreateExerciseAsync(newExercise);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateExerciseAsync_WorkoutExists_ReturnsExercise()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<GymContext>()
            .UseInMemoryDatabase("WorkoutExistsTestDatabase")
            .Options;

        using var dbContext = new GymContext(options);

        var logger = NullLogger<ExerciseService>.Instance;

        var service = new ExerciseService(dbContext, logger);

        var workout = new Workout
        {
            Name = "PUSH DAY"
        };

        dbContext.Workouts.Add(workout);
        await dbContext.SaveChangesAsync();

        var newExercise = new CreateExerciseDto(
            "Bench Press",
            10,
            workout.Id
        );

        // Act
        var result = await service.CreateExerciseAsync(newExercise);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("BENCH PRESS", result.Name);
        Assert.Equal(workout.Id, result.WorkoutId);
    }
}