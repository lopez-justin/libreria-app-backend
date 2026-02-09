using Models.Context;
using Models.Entities;

namespace Api.Data;

public static class AdminSeeder
{
    public static async Task SeedAsync(ApplicationDBContext context, IConfiguration config)
        {
            
            // ¿Ya existe un Admin?
            var adminExists = context.AuthUsers
                .Any(a => a.Role == "admin");
    
            if (adminExists)
                return;
    
            var email = config["AdminSeed:Email"];
            var password = config["AdminSeed:Password"];
            var name = config["AdminSeed:Name"];
    
            var user = new User
            {
                Name = name!,
                Email = email!,
                Active = true,
                MemberSince = DateTime.UtcNow
            };
    
            context.Users.Add(user);
            await context.SaveChangesAsync();
    
            var authUser = new AuthUser
            {
                UserId = user.Id,
                Email = email!,
                Password = password!,
                Role = "admin",
                Active = true
            };
    
            context.AuthUsers.Add(authUser);
            await context.SaveChangesAsync();
        }
}