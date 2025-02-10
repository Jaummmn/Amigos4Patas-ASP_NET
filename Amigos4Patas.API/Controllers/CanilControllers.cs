using Amigos4Patas.Application.DTOs;
using Amigos4Patas.Application.Interfaces;
using Amigos4Patas.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Amigos4Patas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CanilControllers : ControllerBase
{
    private readonly ICanilService _canilService;

    public CanilControllers(ICanilService canilService)
    {
        _canilService = canilService;
    }
    
    [HttpGet("GetAllCanil")]
    public async Task<IActionResult> GetCanil() 
    {
        var results = await _canilService.GetCanils();
        if (results is null || !results.Any()) 
        {
            return NotFound();
        }
        return Ok(results);
    }

    [HttpGet("GetCanil/{id}")]
    public async Task<IActionResult> GetCanil(int id)
    {
        if (id < 1 || id == null)
        {
            return BadRequest();
        }

        var canilDto = await _canilService.GetById(id);
        return Ok(canilDto);
    }

    [HttpPut("UpdateCanil")]
    public async Task<IActionResult> UpdateCanil(int id, [FromBody] CanilDTO canil)
    {
        if (!ModelState.IsValid || id != canil.CanilID)
        {
            return BadRequest("Dados inválidos ou ID não corresponde.");
        }

        try
        {
            var result = await _canilService.UpdateCanil(canil);

            if (result == null)
            {
                return NotFound("Canil não encontrado para atualização.");
            }

            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("RemoveCanil/{canilId}")]
    public async Task<IActionResult> RemoveCanil(int canilId)
    {
        try
        {
            await _canilService.DeleteCanil(canilId);
            return NoContent(); // Retorna status 204, que indica que a ação foi bem-sucedida
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // Se houver erro, retorna status 400 com a mensagem de erro
        }
    }


}