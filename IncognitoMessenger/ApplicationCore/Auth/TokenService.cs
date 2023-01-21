using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Business.Entities;
using IncognitoMessenger.ApplicationCore.Auth.Models;
using IncognitoMessenger.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IncognitoMessenger.Services.Token;

public class TokenService
{
    private readonly JwtSettings jwtSettings;

    public TokenService(IOptions<JwtSettings> jwtSettings)
    {
        this.jwtSettings = jwtSettings.Value;

    }

    public AccessToken GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            null,
            claims,
            expires: DateTime.Now.AddMinutes(jwtSettings.AccessTokenExpiryTimeInMinutes),
            signingCredentials: credentials
        );

        var key = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        return new AccessToken { Key = key, ExpiryTime = jwtSettings.AccessTokenExpiryTimeInMinutes };
    }

    public RefreshToken GenerateRefreshToken(User user, int? tokenId = null)
    {
        var refreshToken = new RefreshToken()
        {
            Token = $"{user.Id}-{Guid.NewGuid()}",
            UserId = user.Id
        };

        if (tokenId != null)
            refreshToken.Id = tokenId.Value;

        return refreshToken;
    }
}