using System;

namespace Amigos4Patas.Application.DTOs.Pet;

public class PetRetornoDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cor { get; set; }
    public int CanilId { get; set; }
    public string CanilNome { get; set; }
    
}