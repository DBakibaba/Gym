using System.ComponentModel.DataAnnotations;

namespace Gym.API.Dtos;

public record RegisterDto(

[Required][StringLength(50)] string Email,
[Required][StringLength(100, MinimumLength = 6)] string Password

);
