using System;
using Amigos4Patas.Domain.Validation;

namespace Amigos4Patas.Domain.Entities;

public class Pet : Entity
{
    
    public string Name { get; set; }  
    public string Color { get; set; }
    
    public DateTime DataRegistro { get; set; }
    
    public Pet()
    {
        
    } 
    public Pet(string nome, string color, DateTime dataRegistro)
    {
        DataRegistro = dataRegistro;
        ValidateDomain(nome, color);
        Name = nome; // Use a propriedade em vez de um campo
        Color = color; // Use a propriedade em vez de um campo
    }

    private void ValidateDomain(string nome, string color)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. O nome é obrigatório");
        DomainExceptionValidation.When(nome.Length < 3, "O nome deve ter no mínimo 3 caracteres");
        DomainExceptionValidation.When(string.IsNullOrEmpty(color), "A cor é obrigatória");
    }

    public void Update(string nome, string color)
    {
        ValidateDomain(nome, color);
        Name = nome; 
        Color = color; 
    }

    public int CanilId { get; set; }
    public Canil Canil { get; set; }
}