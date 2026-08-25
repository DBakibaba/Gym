using System.ComponentModel.DataAnnotations;

namespace Gym.API.Dtos;

public record CreateExerciseDto(


[Required][StringLength(50)] string Name,
[Range(1, 100)] int Reps

);