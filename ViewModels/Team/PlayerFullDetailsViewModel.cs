using Models;

namespace ViewModels
{
    public class PlayerFullDetailsViewModel
    {
        public string PlayerId { get; set; }
        public string NationalId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string NationalIdImageUrl { get; set; }
        public string PersonalImageUrl { get; set; }
    }
}
