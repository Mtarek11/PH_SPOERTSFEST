using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class PlayerConfigurations : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Players");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.NationalId).IsRequired(false);
            builder.Property(i => i.Sport).IsRequired(true);
            builder.Property(i => i.SportType).IsRequired(true);
            builder.Property(i => i.Name).IsRequired(true);
            builder.Property(i => i.PhoneNumber).IsRequired(true);
            builder.Property(i => i.DateOfBirth).IsRequired(true);
            builder.Property(i => i.Email).IsRequired(true);
            builder.Property(i => i.NationalIdImageUrl).IsRequired(false);
            builder.Property(i => i.Gender).IsRequired(true);
            builder.Property(i => i.PersonalImageUrl).IsRequired(true);
            builder.HasIndex(i => new { i.NationalId, i.Sport, i.SportType }).IsUnique(true);
            builder.HasIndex(i => new { i.Email, i.Sport, i.SportType }).IsUnique(true);
            builder.HasOne(i => i.Team).WithMany(i => i.Players).HasForeignKey(i => i.TeamId).OnDelete(DeleteBehavior.Cascade).IsRequired(true);
        }
    }
}
