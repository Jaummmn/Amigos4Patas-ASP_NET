using System.Collections.Generic;
using System.Threading.Tasks;
using Amigos4Patas.Domain.Entities;

namespace Amigos4Patas.Domain.Interfaces;

public interface ICanilRepository
{
    Task<IEnumerable<Canil>> GetAllAsync();
    Task<Canil>  GetByIdAsyncCanil(int id);
    
    Task<Canil> UpdateAsync(Canil canil);
    Task<Canil> CreateAsync(Canil canil);
    Task DeleteAsync(int id);
}