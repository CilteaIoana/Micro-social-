using Micro_social_platform.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Micro_social_platform.Models
{

    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Verificam daca in baza de date exista cel putin un rol
                // insemnand ca a fost rulat codul
                // De aceea facem return pentru a nu insera rolurile inca o data
                // Acesta metoda trebuie sa se execute o singura data
                if (context.Roles.Any())
                {
                    return; // baza de date contine deja roluri
                }
                // CREAREA ROLURILOR IN BD
                // daca nu contine roluri, acestea se vor crea
                context.Roles.AddRange(
                new IdentityRole { Id = "0e8d45e3b0d147109cf3da63b3cfcef1", Name = "Admin", NormalizedName = "Admin".ToUpper() },
                new IdentityRole { Id = "0e8d45e3b0d147109cf3da63b3cfcef2", Name = "User", NormalizedName = "User".ToUpper() }
                );
                // o noua instanta pe care o vom utiliza pentru crearea parolelor utilizatorilor
                 // parolele sunt de tip hash
                 var hasher = new PasswordHasher<ApplicationUser>();
                // CREAREA USERILOR IN BD
                // Se creeaza cate un user pentru fiecare rol
                context.Users.AddRange(
                new ApplicationUser
                {
                    Id = "df9c5788dbf0424eb765d1e133b5b471",
                    // primary key
                    UserName = "admin@test.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "ADMIN@TEST.COM",
                    Email = "admin@test.com",
                    NormalizedUserName = "ADMIN@TEST.COM",
                    PasswordHash = hasher.HashPassword(null, "Admin1!")
                },
               new ApplicationUser
               {
                   Id = "df9c5788dbf0424eb765d1e133b5b472",
                   // primary key
                   UserName = "user@test.com",
                   EmailConfirmed = true,
                   NormalizedEmail = "USER@TEST.COM",
                   Email = "user@test.com",
                   NormalizedUserName = "USER@TEST.COM",
                   PasswordHash = hasher.HashPassword(null, "User1!")
               }
                );
                // ASOCIEREA USER-ROLE
                context.UserRoles.AddRange(
                new IdentityUserRole<string>
                {
                    RoleId = "0e8d45e3b0d147109cf3da63b3cfcef1",
                    UserId = "df9c5788dbf0424eb765d1e133b5b471"
                },
               new IdentityUserRole<string>
               {
                   RoleId = "0e8d45e3b0d147109cf3da63b3cfcef2",
                   UserId = "df9c5788dbf0424eb765d1e133b5b472"
               }
               );
                context.SaveChanges();
            }
        }
    }
}