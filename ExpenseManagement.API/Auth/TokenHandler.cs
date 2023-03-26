using DataAccess.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Auth;

public class TokenHandler : ITokenHandler
{
    private readonly IConfiguration configuration;

    public TokenHandler(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public Task<string> GenerateToken(Users users)
    {
        var roles = users.Roles.Split(',').ToList();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, users.Name)
        };

        roles.ForEach((role) =>
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        });


        var configKey = configuration["Jwt:Key"];
        if (configKey is null) 
        { 
            throw new ArgumentNullException(nameof(configKey)); 
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials
            );

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
