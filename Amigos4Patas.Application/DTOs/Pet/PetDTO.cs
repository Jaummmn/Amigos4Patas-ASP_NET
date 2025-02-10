using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Amigos4Patas.Application.DTOs;

public class PetDTO
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Informe o nome do pet")]
    [MinLength(3)]
    [MaxLength(100)]
    public string Nome { get; set; }
    [Required(ErrorMessage = "Informe a cor do pet")]
    [MinLength(3)]
    [MaxLength(100)]
    public string Cor { get; set; }
    [Required]
    public int CanilId { get; set; }
    
   
}