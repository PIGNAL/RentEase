using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentEase.Identity.Models;

namespace RentEase.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "27b82dc0-0603-460e-99ec-c7b1b5c52780",
                    Email = "joni_ballatore@hotmail.com",
                    NormalizedEmail = "joni_ballatore@hotmail.com",
                    FullName = "Jonathan Ballatore",
                    Address = "San José Obrero, Famailla, Tucumán, Argentina",
                    UserName = "pignal",
                    NormalizedUserName = "pignal",
                    PasswordHash = hasher.HashPassword(null, "pignal2134"),
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "a7cf2088-481a-4d28-aae5-fd7e4ec71c48",
                    Email = "juanperez@localhost.com",
                    NormalizedEmail = "juanperez@localhost.com",
                    FullName = "Juan Perez",
                    Address = "Calle Falsa 123, Buenos Aires, Argentina",
                    UserName = "juanperez",
                    NormalizedUserName = "juanperez",
                    PasswordHash = hasher.HashPassword(null, "pignal2134"),
                    EmailConfirmed = true
                });
        }
    }
}
