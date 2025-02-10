using System.ComponentModel.DataAnnotations;

namespace Amigos4Patas.Application.DTOs;

public class CanilRegisterDTO
{
    [Required]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    public string Descricao { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public string UserID { get; set; }
}