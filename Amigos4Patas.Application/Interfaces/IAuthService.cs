using System.Threading.Tasks;
using Amigos4Patas.Application.DTOs;
using Amigos4Patas.Application.DTOs.Tokens;

namespace Amigos4Patas.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDTO> RegisterAsync(object model);
    
    Task LogoutAsync();
    Task<AuthResponseDTO> LoginAsync(LoginModelDTO model);
}