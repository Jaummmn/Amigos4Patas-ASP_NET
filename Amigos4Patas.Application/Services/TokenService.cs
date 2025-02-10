using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Amigos4Patas.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Amigos4Patas.Application.Services;

public class TokenService : ITokenService
{
    public JwtSecurityToken GenerateToken(IEnumerable<Claim> claims, IConfiguration config)
    {
        // Obtém a chave secreta diretamente do índice
        var key = config["JWT:SecretKey"];
        if (string.IsNullOrEmpty(key))
            throw new InvalidOperationException("SecretKey not found in configuration.");

        var privateKey = Encoding.ASCII.GetBytes(key);

        // Cria as credenciais de assinatura com a chave secreta
        var signinCredentials = new SigningCredentials(
            new SymmetricSecurityKey(privateKey),
            SecurityAlgorithms.HmacSha256
        );

        // Configura o tempo de expiração do token
        var tokenValidityInMinutes = int.Parse(config["JWT:TokenValidityInMinutes"]);
        var expiration = DateTime.UtcNow.AddMinutes(tokenValidityInMinutes);

        // Define o token JWT
        var token = new JwtSecurityToken(
            issuer: config["JWT:ValidIssuer"],
            audience: config["JWT:ValidAudience"],
            claims: claims,
            expires: expiration,
            signingCredentials: signinCredentials
        );
        
        return token;
    }


    public string GenerateRefreshToken()
    {
        var secureRandomBytes = new byte[128];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        
        randomNumberGenerator.GetBytes(secureRandomBytes);
        var refreshToken = Convert.ToBase64String(secureRandomBytes);
        return refreshToken;
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration config)
    {
        // Obtém a chave secreta
        var key = config["JWT:SecretKey"];
        if (string.IsNullOrEmpty(key))
            throw new InvalidOperationException("SecretKey not found in configuration.");

        // Configura parâmetros de validação do token
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false // Ignora a validação de expiração
        };

        // Valida o token e extrai o principal
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

        // Verifica se o token é do tipo correto e se o algoritmo está correto
        if (validatedToken is not JwtSecurityToken jwtSecurityToken || 
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

}