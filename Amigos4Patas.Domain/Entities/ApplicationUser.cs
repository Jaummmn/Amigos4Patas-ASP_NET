#nullable enable
using System;
using Microsoft.AspNetCore.Identity;

namespace Amigos4Patas.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
