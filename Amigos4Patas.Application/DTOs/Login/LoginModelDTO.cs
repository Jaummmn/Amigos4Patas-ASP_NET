using System.ComponentModel.DataAnnotations;

namespace Amigos4Patas.Application.DTOs;

public class LoginModelDTO
{
   [Required (ErrorMessage = "Email is required")]
   [EmailAddress]
   public string Email { get; set; }
   [Required(ErrorMessage = "Password is required")]
   public string Password { get; set; }
}