namespace Gym.API.Models;

public class Workout
{
    public int Id { get; set; }
    public required string Name { get; set; }
    //this is Exercise navigation property 
    public List<Exercise> Exercise { get; set; } = [];
}