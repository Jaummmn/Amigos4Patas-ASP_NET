using System.Collections.Generic;
using Amigos4Patas.Application.DTOs.Pet;

namespace Amigos4Patas.Application.DTOs.Canil;

public class CanilRetornoDTO
{
    public int CanilID { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Descricao { get; set; }
    public List<PetRetornoDTO> Pets { get; set; } = new();
}