using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(i => i.Name).IsRequired(true);
            builder.Property(i => i.PhoneNumber).IsRequired(true);
            builder.HasIndex(i => i.PhoneNumber).IsUnique(true);
            builder.Property(i => i.DateOfBirth).IsRequired(true);
            builder.Property(i => i.Email).IsRequired(true);
            builder.HasIndex(i => i.Email).IsUnique(true);
            builder.Property(i => i.NationalId).IsRequired(false);
            builder.HasIndex(i => i.NationalId).IsUnique(true);
            builder.Property(i => i.NationalIdImageUrl).IsRequired(false);
            builder.Property(i => i.Gender).IsRequired(true);
            builder.Property(i => i.PersonalImageUrl).IsRequired(true);
        }
    }
}
