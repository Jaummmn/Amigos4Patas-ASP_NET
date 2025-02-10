using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amigos4Patas.Domain.Entities;
using Amigos4Patas.Domain.Interfaces;
using Amigos4Patas.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Amigos4Patas.Infrastructure.Repositories;

public class PetRepositories : IPetRepository
{
    private readonly ApplicationDbContext _context;

    public PetRepositories(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Pet>> GetAllAsync()
    {
        var pets = await _context.Pets
            .Include(p => p.Canil)
            .ThenInclude(c => c.User)
            .ToListAsync();
        return pets;
    }
    
    public async Task<Pet> GetByIdAsync(int id)
    {
       return await _context.Pets.FindAsync(id);
    }
    
    public async Task<Pet> UpdateAsync(Pet pet)
    {
        // Atualiza a entidade
        _context.Pets.Update(pet);
        await _context.SaveChangesAsync();
    
        return pet;
    }
    
    public async Task CreateAsync(Pet pet)
    {
       await _context.Pets.AddAsync(pet);
       await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ID inválido para deletar o pet.");

        var pet = await _context.Pets.FindAsync(id);
        if (pet == null)
            throw new KeyNotFoundException("Pet não encontrado.");

        try
        {
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
           
            throw new Exception("Ocorreu um erro ao deletar o pet.", ex);
        }
    }

}