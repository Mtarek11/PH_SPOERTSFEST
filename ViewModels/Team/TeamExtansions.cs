using Models;

namespace ViewModels
{
    public static class TeamExtansions
    {
        public static TeamViewModel ToTeamViewModel (this Team team)
        {
            return new TeamViewModel
            {
                TeamName = team.Name,
                Players = team.Players.Select(i => i.ToPlayerViewModelInTeam()).ToList(),
            };
        }
        public static PlayerViewModelInTeam ToPlayerViewModelInTeam(this Player player)
        {
            return new PlayerViewModelInTeam
            {
                PlayerId = player.Id,
                Name = player.Name,
                Email = player.Email,
            };
        }
        public static PlayerFullDetailsViewModel ToPlayerFullDetailsViewModel(this Player player)
        {
            return new PlayerFullDetailsViewModel
            {
                PlayerId = player.Id,
                DateOfBirth = player.DateOfBirth.ToString("MMMM d, yyyy"),
                Email = player.Email,
                Gender = player.Gender,
                Name = player.Name,
                NationalId = player.NationalId,
                NationalIdImageUrl = player.NationalIdImageUrl,
                PersonalImageUrl = player.PersonalImageUrl,
                PhoneNumber = player.PhoneNumber,
            };
        }
        public static TeamViewModelInList ToVTeamViewModelInList(this Team team)
        {
            return new TeamViewModelInList()
            {
                TeamId = team.Id,
                Name = team.Name != null ? team.Name : team.Players.Select(i => i.Name).FirstOrDefault()
            };
        }
        public static TeamsViewModelForAdmin ToViewModelForAdmin(this Team team)
        {
            return new TeamsViewModelForAdmin()
            {
                TeamName = team.Name != null ? team.Name : "Individual",
                Players = team.Players.Select(i => i.ToViewModelForAdmin()).ToList(),
            };
        }
        public static PlayerViewModelForAdmin ToViewModelForAdmin(this Player player)
        {
            return new PlayerViewModelForAdmin
            {
                DateOfBirth = player.DateOfBirth.ToString("MMMM d, yyyy"),
                Email = player.Email,
                Gender = player.Gender,
                Name = player.Name,
                NationalId = player.NationalId,
                PhoneNumber = player.PhoneNumber,
            };
        }
    }
}
