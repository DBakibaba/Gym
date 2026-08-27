using System.ComponentModel.DataAnnotations;
namespace Gym.API.Dtos;

public record UpdateWorkoutDto(

[Required][StringLength(50)] string Name

);
