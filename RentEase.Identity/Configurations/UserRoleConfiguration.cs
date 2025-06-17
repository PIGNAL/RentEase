using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentEase.Identity.Configurations
{
    public class UserRoleConfiguration: IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "86401a46-2766-404b-b19e-40ca8544182d",
                    UserId = "27b82dc0-0603-460e-99ec-c7b1b5c52780"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "fa5df3be-1c94-430c-ae24-fc2e0b11e027",
                    UserId = "a7cf2088-481a-4d28-aae5-fd7e4ec71c48"
                });
        }
    }
}
