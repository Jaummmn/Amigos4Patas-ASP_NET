using System.Collections.Generic;
using Amigos4Patas.Domain.Validation;
using Microsoft.AspNetCore.Identity;

namespace Amigos4Patas.Domain.Entities;

public class Canil : Entity
{
    public Canil(string descricao, string userId)
    {
        ValidateDomain(descricao, userId);
        Descricao = descricao;
        UserId = userId;
        Pets = new List<Pet>();
    }

    public Canil(ICollection<Pet> pets)
    {
        Pets = pets;
    }
    public Canil(){}

    private void ValidateDomain(string descricao, string userId)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(descricao),
            "Nome inválido. O nome é obrigatório.");

        DomainExceptionValidation.When(descricao.Length < 10,
            "O nome deve ter no mínimo 10 caracteres.");
        
        DomainExceptionValidation.When(string.IsNullOrEmpty(userId), 
            "O user id é obrigatório.");
    }

    public void Update(string userId, string descricao)
    {
        ValidateDomain(descricao, userId);
        UserId = userId;
        Descricao = descricao;
    }

    public string Descricao { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public ICollection<Pet> Pets { get; set; }
}