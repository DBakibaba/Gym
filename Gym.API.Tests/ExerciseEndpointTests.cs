using System.Net;
using System.Net.Http.Json;
using Gym.API;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

public class ExerciseEndpointTests
{
    [Fact]
    public async Task PostExercise_WorkoutDoesNotExist_ReturnsNotFound()
    {
        await using var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
{
    var descriptor = services.SingleOrDefault(
        d => d.ServiceType ==
            typeof(IDbContextOptionsConfiguration<GymContext>));

    if (descriptor is not null)
    {
        services.Remove(descriptor);
    }

    services.AddDbContext<GymContext>(options =>
    {
        options.UseInMemoryDatabase("IntegrationTestDatabase");
    });
});
            });

        var client = factory.CreateClient();

        var request = new
        {
            name = "Bench Press",
            reps = 10,
            workoutId = 999
        };

        var response =
            await client.PostAsJsonAsync("/exercises", request);

        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }
}