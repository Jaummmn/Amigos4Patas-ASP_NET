using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Amigos4Patas.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Amigos4Patas.Application.Interfaces;

public interface ITokenService
{
    JwtSecurityToken GenerateToken(IEnumerable<Claim> claims,
                                    IConfiguration _config);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config);
}