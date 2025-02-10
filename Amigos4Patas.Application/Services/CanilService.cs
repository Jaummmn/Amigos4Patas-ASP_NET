using System.Collections.Generic;
using System.Threading.Tasks;
using Amigos4Patas.Application.DTOs;
using Amigos4Patas.Application.DTOs.Canil;
using Amigos4Patas.Application.Interfaces;
using Amigos4Patas.Domain.Entities;
using Amigos4Patas.Domain.Interfaces;
using AutoMapper;

namespace Amigos4Patas.Application.Services;

public class CanilService : ICanilService
{
    private ICanilRepository _canilRepository;
    private readonly IMapper _mapper;

    public CanilService(ICanilRepository canilRepository, IMapper mapper)
    {
        _canilRepository = canilRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CanilDTO>> GetCanils()
    {
        var canil = await _canilRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CanilDTO>>(canil);
    }


    public async Task<CanilRetornoDTO> GetById(int id)
    {
        var canil = await _canilRepository.GetByIdAsyncCanil(id);
        return  _mapper.Map<CanilRetornoDTO>(canil);
    }

    public async Task CreateCanil(CanilRegisterDTO canil)
    {
        var canilentry =  _mapper.Map<Canil>(canil);
        await _canilRepository.CreateAsync(canilentry);
    }

    public async Task<CanilDTO> UpdateCanil(CanilDTO canil)
    {
       
        var canilExist = await _canilRepository.GetByIdAsyncCanil(canil.CanilID);
        
        if (canilExist == null)
        {
            throw new KeyNotFoundException("Canil não encontrado para atualização.");
        }
        
        var canilentry =  _mapper.Map<Canil>(canil);
        canilentry.UserId = canilExist.UserId; 
        
        var updatedCanil = await _canilRepository.UpdateAsync(canilentry);
        return _mapper.Map<CanilDTO>(updatedCanil);
    }

    public async Task DeleteCanil(int id)
    {
        await _canilRepository.DeleteAsync(id);
    }
}