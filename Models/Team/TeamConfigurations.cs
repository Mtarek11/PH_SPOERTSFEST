using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class TeamConfigurations : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Teams");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.Name).IsRequired(false);
            builder.HasIndex(i => new { i.Name, i.Sport, i.SportType }).IsUnique(true);
            builder.Property(i => i.Sport).IsRequired(true);
            builder.HasOne(i => i.Captian).WithMany(i => i.Teams).HasForeignKey(i => i.CaptainId).OnDelete(DeleteBehavior.SetNull).IsRequired(false);
        }
    }
}
