using System.ComponentModel.DataAnnotations;

namespace Gym.API.Dtos;

public record LoginDto(

[Required][EmailAddress][StringLength(50)] string Email,
[Required][StringLength(100, MinimumLength = 6)] string Password

);
