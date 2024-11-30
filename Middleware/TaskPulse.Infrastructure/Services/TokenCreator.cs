using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskPulse.Domain.Options;
using TaskPulse.Infrastructure.Services;

namespace TaskPulse.Infrastructure;

public class TokenCreator: ITokenCreator
{
    private EncryptPasswordOptions encryptPasswordOptions;

    private string Token => encryptPasswordOptions.Token;
    
    public TokenCreator(IOptions<EncryptPasswordOptions> encryptPasswordOptions)
    {
        this.encryptPasswordOptions = encryptPasswordOptions.Value;
    }
    public Task<string> CreateToken(string username)
    {
        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.Name, username)
            //new Claim(ClaimTypes.Role, "Admin"),
            //new Claim(ClaimTypes.Role, "User"),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Token));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        string jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return Task.FromResult(jwt.ToString());
    }
}