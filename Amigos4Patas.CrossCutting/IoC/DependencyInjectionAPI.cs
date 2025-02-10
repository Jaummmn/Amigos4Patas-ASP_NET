using System;
using System.Text;
using System.Threading.Tasks;
using Amigos4Patas.Application.Interfaces;
using Amigos4Patas.Application.Mappings;
using Amigos4Patas.Application.Services;
using Amigos4Patas.Domain.Entities;
using Amigos4Patas.Domain.Interfaces;
using Amigos4Patas.Infrastructure.Context;
using Amigos4Patas.Infrastructure.Repositories;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;


namespace Amigos4Patas.CrossCutting.IoC;

public static class DependencyInjectionAPI
{
    public static IServiceCollection AddIoC(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

     
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false; // Desabilita a exigência de dígitos na senha
                options.Password.RequiredLength = 4; // Define o comprimento mínimo da senha como 4 caracteres
                options.Password.RequireNonAlphanumeric = false; // Desabilita a exigência de caracteres não alfanuméricos
                options.Password.RequireUppercase = false; // Desabilita a exigência de letras maiúsculas
                options.Password.RequireLowercase = false; // Desabilita a exigência de letras minúsculas
                options.Password.RequiredUniqueChars = 1; // Define o número mínimo de caracteres únicos
            }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        services.AddScoped<UserManager<ApplicationUser>>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPetService, PetService>();
        services.AddScoped<IPetRepository, PetRepositories>();
        services.AddScoped<ICanilService, CanilService>();
        services.AddScoped<ICanilRepository, CanilRepositories>();
        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
        services.AddScoped<DataSeeder>();
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }

}