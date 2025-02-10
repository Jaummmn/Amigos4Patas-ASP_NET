using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

public class DataSeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;
 

    public DataSeeder(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
       
    }

    public async Task SeedAsync()
    {
        await SeedRolesAsync();
      
    }

    private async Task SeedRolesAsync()
    {
        string[] roles = new string[] { "Canil", "User" };

        foreach (var role in roles)
        {
            try
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"Error seeding role {role}: {ex.Message}");
            }
        }
    }


  
   
}