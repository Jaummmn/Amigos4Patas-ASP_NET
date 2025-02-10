using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amigos4Patas.Application.DTOs;
using Amigos4Patas.Application.Interfaces;
using Amigos4Patas.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Amigos4Patas.Application.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        
        return _mapper.Map<IEnumerable<UserDTO>>(users);
        
    }

    public  async Task<ApplicationUser> GetUserByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<bool> RemoveUserAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return false;
        }
        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            return false;
        }
        return true;
    }

    public async Task<ApplicationUser> UpdateUserAsync(UserDTO user)
    {
        var userdb = await _userManager.FindByIdAsync(user.UserID.ToString());
    
        if (userdb == null)
        {
            throw new ApplicationException("Usuário inexistente");
        }

        // Atualiza apenas os campos desejados
        userdb.UserName = user.UserName;
        userdb.Email = user.Email;

        var result = await _userManager.UpdateAsync(userdb);

        if (!result.Succeeded)
        {
            throw new Exception("erro ao atualizar usuario");
        }

        return userdb;
    }

}