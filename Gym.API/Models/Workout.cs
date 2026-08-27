namespace Gym.API.Models;

public class Workout
{
    public int Id { get; set; }
    public required string name { get; set; }

    public List<Exercise> Exercise { get; set; } = [];
}