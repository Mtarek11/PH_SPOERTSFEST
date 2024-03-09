using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string NationalId {  get; set; }
        public string NationalIdImageUrl { get; set; }
        public string PersonalImageUrl {  get; set; }
        public Gender Gender { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
    public enum Gender
    {
        male = 1,
        female = 2,
    }
}
