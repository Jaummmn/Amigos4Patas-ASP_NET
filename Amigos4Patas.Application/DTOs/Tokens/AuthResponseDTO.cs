using System;
using System.Collections.Generic;

namespace Amigos4Patas.Application.DTOs.Tokens;

public class AuthResponseDTO
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    
    public string Id { get; set; }
    public IList<string> Roles { get; set; }
}