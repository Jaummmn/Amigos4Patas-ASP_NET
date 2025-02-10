using System.Collections.Generic;
using System.Threading.Tasks;
using Amigos4Patas.Application.DTOs;
using Amigos4Patas.Application.DTOs.Canil;


namespace Amigos4Patas.Application.Interfaces;

public interface ICanilService
{
    Task<IEnumerable<CanilDTO>> GetCanils();
    Task<CanilRetornoDTO> GetById(int id);
    Task CreateCanil(CanilRegisterDTO canil);
    Task<CanilDTO> UpdateCanil(CanilDTO canil);
    Task DeleteCanil(int id);
 
}