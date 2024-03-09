namespace Models
{
    public class Player
    {
        public string Id { get; set; }
        public string NationalId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Email {  get; set; }
        public Gender Gender { get; set; }
        public string NationalIdImageUrl { get; set; }
        public string PersonalImageUrl { get; set; }
        public string TeamId { get; set; }
        public Sport Sport { get; set; }
        public SportType SportType { get; set; }
        public virtual Team Team { get; set; }
    }
}
