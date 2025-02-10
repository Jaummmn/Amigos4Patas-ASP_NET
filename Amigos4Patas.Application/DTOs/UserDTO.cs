

using System;

namespace Amigos4Patas.Application.DTOs;

public class UserDTO
{
    public string UserName { get; set; }
    public string Email { get; set; }
    
    public Guid UserID { get; set; }
}