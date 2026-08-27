namespace Gym.API.Models;

public class Exercise
{
    public Workout Workout { get; set; } = null!;
    public int WorkoutId { get; set; }
    public int Id { get; set; }

    public required string Name { get; set; }
    public int Reps { get; set; }


}
