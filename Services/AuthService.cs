using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NOROFF_ASPNET.Data;
using NOROFF_ASPNET.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


public class AuthService
{
    private readonly DataContext _dataContext;
    private readonly JwtSettings _jwtSettings;

    public AuthService(DataContext dataContext, JwtSettings jwtSettings)
    {
        _dataContext = dataContext;
        _jwtSettings = jwtSettings;
    }

    public async Task<bool> ValidateUserAsync(string username, string password)
    {
        var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.Username == username);
        if (user == null)
            return false;

        var passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

        return result == PasswordVerificationResult.Success;
    }

    public async Task<bool> RegisterUserAsync(string username, string password)
    {
        //Check if Username already exists
        if (await _dataContext.Users.AnyAsync(u => u.Username == username))
            return false;

        //Create User
        var passwordHasher = new PasswordHasher<User>();
        var user = new User
        {
            Username = username,
            PasswordHash = passwordHasher.HashPassword(null, password)
        };

        _dataContext.Users.Add(user);
        await _dataContext.SaveChangesAsync();
        return true;
    }

    //Used to retrieve the Username if it exists, when logging in
    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _dataContext.Users.SingleOrDefaultAsync(u => u.Username == username);
    }

    //missing part add generate token
    public string GenerateToken(User user)
    {
        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }



}

