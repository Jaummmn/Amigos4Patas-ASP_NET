using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amigos4Patas.Domain.Entities;
using Amigos4Patas.Domain.Interfaces;
using Amigos4Patas.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Amigos4Patas.Infrastructure.Repositories;

public class CanilRepositories : ICanilRepository
{
    private readonly ApplicationDbContext _context;
  
    public CanilRepositories(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Canil>> GetAllAsync()
    {
        var resultado = await _context.UserCanils
            .Include(c => c.User)
            .ToListAsync();
         
        return resultado;
    }
    
    public async Task<Canil> GetByIdAsyncCanil(int id)
    {
        var canil = await _context.UserCanils
            .Include(uc => uc.User)
            .Select(uc => new Canil
            {
                Id = uc.Id,
                Descricao = uc.Descricao,
                UserId = uc.UserId,
                User = uc.User,
                Pets = uc.Pets.Where(p => p.CanilId == id).ToList()
            })
            .FirstOrDefaultAsync(uc => uc.Id == id);

        return canil;
    }
    public async Task<Canil> UpdateAsync(Canil canil)
    {
        // Carregar o Canil e incluir a entidade User relacionada
        var canilToUpdateEntity = await _context.UserCanils
            .Include(c => c.User) // Certifique-se de carregar o User
            .FirstOrDefaultAsync(c => c.Id == canil.Id);

        if (canilToUpdateEntity == null)
        {
            return null; // Canil não encontrado
        }

        // Atualizar as propriedades do Canil
        _context.Entry(canilToUpdateEntity).CurrentValues.SetValues(canil);
        // Atualizar as propriedades do User relacionado
        if (canil.User != null && canilToUpdateEntity.User != null)
        {
            canilToUpdateEntity.User.UserName = canil.User.UserName;
            canilToUpdateEntity.User.Email = canil.User.Email;
            // Atualize outras propriedades relevantes, se necessário
        }
        await _context.SaveChangesAsync(); // Salvar alterações em ambas as tabelas
        
        return canilToUpdateEntity;
    }
    public async Task<Canil> CreateAsync(Canil canil)
    {
        _context.Add(canil);
        await _context.SaveChangesAsync();
        return canil;
        
    }

    public async Task DeleteAsync(int id)
    {
        var canil = await _context.UserCanils.Include(c => c.User) 
            .FirstOrDefaultAsync(c => c.Id == id);
        if (canil == null)
        {
            throw new Exception("Canil não encontrado.");
        }
        _context.UserCanils.Remove(canil);
        _context.Users.Remove(canil.User);
        await _context.SaveChangesAsync();
    }
    
}

