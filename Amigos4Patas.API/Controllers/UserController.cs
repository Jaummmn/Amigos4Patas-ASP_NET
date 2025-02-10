using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Amigos4Patas.Application.DTOs;
using Amigos4Patas.Application.Interfaces;
using Amigos4Patas.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace Amigos4Patas.Controllers;
[Authorize(Roles = "User")]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    // Modifique o tipo do parâmetro para IUserService, e não UserService
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [AllowAnonymous]
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }

            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    [HttpDelete("users/{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return BadRequest("User ID must be provided.");
        }
        try
        {
            var result = await _userService.RemoveUserAsync(id);
        
            if (!result)
            {
                return NotFound($"Usuario nao encontrado: {id}");
            }
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    [HttpPut("users/{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDTO user)
    {
        if (!Guid.TryParse(id, out var userId) || user.UserID == Guid.Empty || user.UserID != userId)
        {
            return BadRequest("ID do usuário inválido ou não corresponde ao fornecido na URL.");
        }

        try
        {
            var result = await _userService.UpdateUserAsync(user);
            return Ok(result);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno no servidor.", error = ex.Message });
        }
    }
    
}
