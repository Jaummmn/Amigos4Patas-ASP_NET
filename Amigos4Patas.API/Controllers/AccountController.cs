using Amigos4Patas.Application.DTOs;
using Amigos4Patas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Amigos4Patas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("registerUser")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDTO model)
    {
        try
        {
            var result = await _authService.RegisterAsync(model);
            return Ok(result); // Retorna os detalhes do usuário ou sucesso
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message }); // Retorna mensagem de erro detalhada
        }
    }
    [HttpPost("registerCanil")]
    public async Task<IActionResult> Register([FromBody] CanilRegisterDTO model)
    {
        try
        {
            var result = await _authService.RegisterAsync(model);
            return Ok(result); // Retorna os detalhes do canil ou sucesso
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message }); // Retorna mensagem de erro detalhada
        }
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModelDTO model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState); // Retorna 400 se o modelo for inválido
        var result = await _authService.LoginAsync(model);

        if (result is null)
        {
            return Unauthorized(); // Retorna 401 se o login falhar
        }

        // Retorna o resultado com o token e as informações do usuário
        return Ok(new
        {
            Token = result.Token,
            RefreshToken = result.RefreshToken,
            RefreshTokenExpiryTime = result.RefreshTokenExpiryTime,
            UserName = result.UserName,
            Email = result.Email,
            Roles = result.Roles,
            Id = result.Id 
        });
    }
}