using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public static class DataSeeding
    {
        public static void DataSeed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Name = "MU12", NormalizedName = "MU12" },
            new IdentityRole { Name = "MU14", NormalizedName = "MU14" },
            new IdentityRole { Name = "MU16", NormalizedName = "MU16" },
            new IdentityRole { Name = "MU20", NormalizedName = "MU20" },
            new IdentityRole { Name = "MU40", NormalizedName = "MU40" },
            new IdentityRole { Name = "MA40", NormalizedName = "MA40" },
            new IdentityRole { Name = "FU12", NormalizedName = "FU12" },
            new IdentityRole { Name = "FU14", NormalizedName = "FU14" },
            new IdentityRole { Name = "FU16", NormalizedName = "FU16" },
            new IdentityRole { Name = "FU20", NormalizedName = "FU20" },
            new IdentityRole { Name = "FU40", NormalizedName = "FU40" },
            new IdentityRole { Name = "FA40", NormalizedName = "FA40" });
        }
    }
}
