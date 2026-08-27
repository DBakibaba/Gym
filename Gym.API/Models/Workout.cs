namespace Gym.API.Models;

public class Workout
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public List<Exercise> Exercise { get; set; } = [];
}