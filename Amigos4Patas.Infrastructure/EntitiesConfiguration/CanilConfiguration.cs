using System;
using Amigos4Patas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amigos4Patas.Infrastructure.EntitiesConfiguration;

public class CanilConfiguration : IEntityTypeConfiguration<Canil>
{
    public void Configure(EntityTypeBuilder<Canil> builder)
    {
        builder.HasKey(c => c.Id);
        builder
            .HasOne(uc => uc.User)
            .WithOne()
            .HasForeignKey<Canil>(uc => uc.UserId);
        
        builder
            .HasMany(c => c.Pets)
            .WithOne(p => p.Canil)
            .HasForeignKey(p => p.CanilId);

        builder.ToTable("UserCanil");
    }
}