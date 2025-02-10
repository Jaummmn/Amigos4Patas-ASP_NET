using System.Collections.Generic;
using System.Threading.Tasks;
using Amigos4Patas.Application.DTOs;
using Amigos4Patas.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Amigos4Patas.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    Task<ApplicationUser> GetUserByIdAsync(string id);

    Task<bool> RemoveUserAsync(string id);
    Task<ApplicationUser> UpdateUserAsync(UserDTO user);
}