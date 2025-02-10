using System.Collections.Generic;
using System.Threading.Tasks;
using Amigos4Patas.Application.DTOs;
using Amigos4Patas.Application.DTOs.Pet;
using Amigos4Patas.Domain.Entities;

namespace Amigos4Patas.Application.Interfaces;

public interface IPetService
{
    Task<IEnumerable<PetRetornoDTO>> GetPets();
    Task<PetRetornoDTO> GetById(int? id);
    Task<PetDTO> Add(PetDTO petDto);
    Task<PetDTO> Update(PetDTO pet);
    Task Remove(int id);
}