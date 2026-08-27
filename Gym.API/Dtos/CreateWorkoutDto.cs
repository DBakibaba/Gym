using System.ComponentModel.DataAnnotations;

namespace Gym.API.Dtos;

public record CreateWorkoutDto(

   [Required][StringLength(50)] string Name

);