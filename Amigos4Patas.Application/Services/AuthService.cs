using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Amigos4Patas.Application.DTOs;
using Amigos4Patas.Application.DTOs.Tokens;
using Amigos4Patas.Application.Interfaces;
using Amigos4Patas.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Amigos4Patas.Application.Services;

public class AuthService : IAuthService
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ICanilService _canilService;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _config;

    public AuthService(IConfiguration config, ICanilService canilService, ITokenService tokenService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _config = config;
        _canilService = canilService;
        _tokenService = tokenService;
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<AuthResponseDTO> RegisterAsync(object model)
    {
    ApplicationUser user;

    switch (model)
    {
        case UserRegisterDTO userDTO:
        {
            var userExists = await _userManager.FindByEmailAsync(userDTO.Email);
            if (userExists != null)
            {
                throw new Exception("Usuário já cadastrado");
            }

            user = new ApplicationUser { Email = userDTO.Email, UserName = userDTO.UserName, SecurityStamp = Guid.NewGuid().ToString() };
            var result = await _userManager.CreateAsync(user, userDTO.Password);

            if (!result.Succeeded)
            {
                // Captura e relata os erros específicos
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Erro ao registrar usuário: {errors}");
            }

            await AssignRoleToUserAsync(user, "User");

            return new AuthResponseDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                
            };
        }

        case CanilRegisterDTO canilDTO:
        {
            var userExists = await _userManager.FindByEmailAsync(canilDTO.Email);
            if (userExists != null)
            {
                throw new Exception("Usuário já cadastrado");
            }

            user = new ApplicationUser { Email = canilDTO.Email, UserName = canilDTO.UserName, SecurityStamp = Guid.NewGuid().ToString() };
            var result = await _userManager.CreateAsync(user, canilDTO.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Erro ao registrar canil");
            }

            var canil = new CanilRegisterDTO()
            {
                Descricao = canilDTO.Descricao,
                UserID = user.Id,
            };

            await _canilService.CreateCanil(canil);

            await AssignRoleToUserAsync(user, "Canil");

            return new AuthResponseDTO
            {
                UserName = user.UserName,
                Email = user.Email,
            };
        }

        default:
            throw new ArgumentException("Tipo de registro não suportado.");
    }
}


/// {
   // "email": "joao2@gmail.com",
   // "password": "123456789"
//


    public async Task LogoutAsync()
    {
        throw new System.NotImplementedException();
    }

    public async Task<AuthResponseDTO> LoginAsync(LoginModelDTO model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        
        if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            // Corrigir para obter o token como string
            JwtSecurityToken jwtToken = _tokenService.GenerateToken(authClaims, _config);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_config["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);
            await _userManager.UpdateAsync(user);

            // Retorna o objeto AuthResponseDTO
            return new AuthResponseDTO
            {
                Token = tokenString, // Retorna o token como string
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                UserName = user.UserName,
                Email = user.Email,
                Roles = userRoles,
                Id = user.Id
            };
        }

        // Lança exceção em caso de falha no login
        throw new UnauthorizedAccessException("Invalid email or password.");
    }

    private async Task AssignRoleToUserAsync(ApplicationUser user, string role)
    {
        if (!await _roleManager.RoleExistsAsync(role))
        {
            await _roleManager.CreateAsync(new IdentityRole(role));
        }
        await _userManager.AddToRoleAsync(user, role);
    }
}