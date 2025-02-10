using System.Collections.Generic;
using System.Threading.Tasks;
using Amigos4Patas.Domain.Entities;

namespace Amigos4Patas.Domain.Interfaces;

public interface IPetRepository
{
    Task<IEnumerable<Pet>> GetAllAsync();
    Task<Pet>  GetByIdAsync(int id);
    Task<Pet> UpdateAsync(Pet pet);
    Task CreateAsync(Pet pet);
    Task DeleteAsync(int id);
}