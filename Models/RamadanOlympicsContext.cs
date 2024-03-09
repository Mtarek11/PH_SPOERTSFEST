using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class RamadanOlympicsContext(DbContextOptions<RamadanOlympicsContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfigurations());
            modelBuilder.ApplyConfiguration(new PlayerConfigurations());
            modelBuilder.ApplyConfiguration(new TeamConfigurations());
            modelBuilder.DataSeed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
