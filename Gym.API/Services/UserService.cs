using Gym.API.Dtos;
using Gym.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Gym.API.Services;

public class UserService(GymContext dbContext, IPasswordHasher<User> passwordHasher)
{
    public async Task<User?> RegisterAsync(RegisterDto registerDto)
    {
        var emailExists = await dbContext.Users.AnyAsync(user => user.Email == registerDto.Email);
        if (emailExists)
        {
            return null;
        }

        var user = new User
        {
            Email = registerDto.Email,
            PasswordHash = ""
        };
        user.PasswordHash = passwordHasher.HashPassword(user, registerDto.Password);

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        return user;
    }
    public async Task<User?> LoginAsync(LoginDto loginDto)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Email == loginDto.Email);
        if (user is null)
        {
            return null;
        }

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }
        return user;
    }


}