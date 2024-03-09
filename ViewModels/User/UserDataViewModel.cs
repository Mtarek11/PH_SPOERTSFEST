using Models;

namespace ViewModels
{
    public class UserDataViewModel
    {
        public string Token { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Role {  get; set; }
    }
}
