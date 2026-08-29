namespace Gym.API.Dtos;

public record WorkoutDetailDto(

int Id,
string Name,
List<ExerciseSummaryDto> Exercise
);