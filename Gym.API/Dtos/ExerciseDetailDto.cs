namespace Gym.API.Dtos;

public record ExerciseDetailDto(

int Id,
string Name,
int Reps,
int WorkoutId,
string WorkoutName

);