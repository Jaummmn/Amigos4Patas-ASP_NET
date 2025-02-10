using Amigos4Patas.Application.DTOs;
using Amigos4Patas.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Amigos4Patas.Controllers;

[Authorize(Roles = "Canil")]
[Route("api/[controller]")]
[ApiController]
public class PetController : ControllerBase
{
  private readonly IPetService _petService;

  public PetController(IPetService petService)
  {
    _petService = petService;
  }
  
  [HttpPost]
  public async Task<IActionResult> Post(PetDTO pet)
  {
    if (ModelState.IsValid)
    { 
      var newPet = await _petService.Add(pet);
      return Ok(newPet);
    }
    return BadRequest();
  }

  [HttpPut("{id:int:min(1)}")]
  public async Task<IActionResult> Put(int id, [FromBody] PetDTO petDto)
  {
    if (!ModelState.IsValid || id != petDto.Id)
      return BadRequest(new { Message = "O ID na URL não corresponde ao ID no corpo da requisição." });
    try
    {
      var updatedPet = await _petService.Update(petDto);
      return Ok(updatedPet); // Retorna o objeto atualizado
    }
    catch (KeyNotFoundException ex)
    {
      return NotFound(new { Message = ex.Message }); // Pet não encontrado
    }
  }

  [HttpDelete("{id:int:min(1)}")]
  public async Task<IActionResult> Delete(int id)
  {
    if (id == null) return BadRequest();
    await _petService.Remove(id);
    return Ok();
  }
  
  [AllowAnonymous]
  [HttpGet ("getPets")]
  public async Task<IActionResult> Get()
  {
    var pets = await _petService.GetPets();
    return Ok(pets);
  }
}