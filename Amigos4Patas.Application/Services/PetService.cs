using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amigos4Patas.Application.DTOs;
using Amigos4Patas.Application.DTOs.Pet;
using Amigos4Patas.Application.Interfaces;
using Amigos4Patas.Application.Mappings;
using Amigos4Patas.Domain.Entities;
using Amigos4Patas.Domain.Interfaces;
using AutoMapper;

namespace Amigos4Patas.Application.Services;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IMapper _mapper;

    public PetService(IPetRepository petRepository, IMapper mapper)
    {
        _petRepository = petRepository;
        _mapper = mapper;
       
    }

    public async Task<IEnumerable<PetRetornoDTO>> GetPets()
    {
        var pets = await _petRepository.GetAllAsync();  // Obtém os Pets com os dados relacionados
        return _mapper.Map<IEnumerable<PetRetornoDTO>>(pets);  // Mapeia para o DTO
    }

    public Task<PetRetornoDTO> GetById(int? id)
    {
        throw new System.NotImplementedException();
    }

    public async Task<PetDTO> Add(PetDTO petDto)
    {
        var pet = new Pet(petDto.Nome, petDto.Cor, DateTime.UtcNow)
        {
            CanilId = petDto.CanilId,
        };

        await _petRepository.CreateAsync(pet);

        // Retornar um DTO representando o pet criado
        return new PetDTO
        {
            Id = pet.Id,
            Nome = pet.Name,
            Cor = pet.Color,
            CanilId = pet.CanilId
        };
    }

    public async Task<PetDTO> Update(PetDTO petDto)
{
    var existingPet = await _petRepository.GetByIdAsync(petDto.Id);

    if (existingPet == null)
    {
        throw new Exception("Pet não encontrado.");
    }

    // Atualiza manualmente as propriedades necessárias
    existingPet.Name = petDto.Nome;
    existingPet.Color = petDto.Cor;
    
    // Atualiza no repositório
    var updatedPet = await _petRepository.UpdateAsync(existingPet);

    return _mapper.Map<PetDTO>(updatedPet);
}

    
    public Task Remove(int id)
    {
       return _petRepository.DeleteAsync(id);
    }
}