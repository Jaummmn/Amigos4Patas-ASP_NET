using System.ComponentModel.DataAnnotations;

namespace Amigos4Patas.Application.DTOs;

public class UserRegisterDTO
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}