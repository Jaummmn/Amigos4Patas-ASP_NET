using Amigos4Patas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amigos4Patas.Infrastructure.EntitiesConfiguration;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();  
        builder.Property(p=>p.Color).HasMaxLength(100).IsRequired();
        builder.HasOne(e=>e.Canil).WithMany(e=>e.Pets).HasForeignKey(e=>e.CanilId).IsRequired();
            builder.HasOne(p => p.Canil) // Um Pet tem um Canil
            .WithMany(c => c.Pets) // Um Canil tem muitos Pets
            .HasForeignKey(p => p.CanilId) // A chave estrangeira é CanilId
            .OnDelete(DeleteBehavior.Cascade); // Especifica o comportamento ao excluir
    }
}