using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Numerics;
using ViewModels;

namespace Reposatiory
{
    public class TeamManager(RamadanOlympicsContext _mydB, PlayerManager _playerManager, UnitOfWork _unitOfWork) : MainManager<Team>(_mydB)
    {
        private readonly PlayerManager playerManager = _playerManager;
        private readonly UnitOfWork unitOfWork = _unitOfWork;
        string baseUrl = "Content/Images/";
        public async Task<APIResult<string>> CreateTeamAsync(string teamName, Sport sport, SportType sportType, string userId, List<PlayerViewModel> players)
        {
            APIResult<string> aPIResult = new();
            Team team = new()
            {
                CaptainId = userId,
                Name = teamName,
                Sport = sport,
                SportType = sportType,
                Players = new List<Player>()
            };
            if (sport == Sport.FTM)
            {
                if (players.Count < 7 || players.Count > 9)
                {
                    aPIResult.Message = "Team must contain 7 to 9 players.";
                    aPIResult.IsSucceed = false;
                    aPIResult.StatusCode = 400;
                    return aPIResult;
                }
                if (teamName == null)
                {
                    aPIResult.Message = "Team name is requierd.";
                    aPIResult.IsSucceed = false;
                    aPIResult.StatusCode = 400;
                    return aPIResult;
                }
                foreach (PlayerViewModel player in players)
                {
                    if (player.Gender == Gender.female)
                    {
                        aPIResult.Message = "All team mumbers must be males";
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                    }
                }
            }
            else if (sport == Sport.BTM)
            {
                if (players.Count < 4 || players.Count > 5)
                {
                    aPIResult.Message = "Team must contain 4 or 5 players.";
                    aPIResult.IsSucceed = false;
                    aPIResult.StatusCode = 400;
                    return aPIResult;
                }
                if (teamName == null)
                {
                    aPIResult.Message = "Team name is requierd.";
                    aPIResult.IsSucceed = false;
                    aPIResult.StatusCode = 400;
                    return aPIResult;
                }
                foreach (PlayerViewModel player in players)
                {
                    if (player.Gender == Gender.female)
                    {
                        aPIResult.Message = "All team mumbers must be males";
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                    }
                }
            }
            else if (sport == Sport.FTF)
            {
                if (players.Count < 7 || players.Count > 9)
                {
                    aPIResult.Message = "Team must contain 7 to 9 players.";
                    aPIResult.IsSucceed = false;
                    aPIResult.StatusCode = 400;
                    return aPIResult;
                }
                if (teamName == null)
                {
                    aPIResult.Message = "Team name is requierd.";
                    aPIResult.IsSucceed = false;
                    aPIResult.StatusCode = 400;
                    return aPIResult;
                }
                foreach (PlayerViewModel player in players)
                {
                    if (player.Gender == Gender.male)
                    {
                        aPIResult.Message = "All team mumbers must be females";
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                    }
                }
            }
            else if (sport == Sport.BTF)
            {
                if (players.Count < 4 || players.Count > 5)
                {
                    aPIResult.Message = "Team must contain 4 or 5 players.";
                    aPIResult.IsSucceed = false;
                    aPIResult.StatusCode = 400;
                    return aPIResult;
                }
                if (teamName == null)
                {
                    aPIResult.Message = "Team name is requierd.";
                    aPIResult.IsSucceed = false;
                    aPIResult.StatusCode = 400;
                    return aPIResult;
                }
                foreach (PlayerViewModel player in players)
                {
                    if (player.Gender == Gender.male)
                    {
                        aPIResult.Message = "All team mumbers must be females";
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                    }
                }
            }
            else if (sport == Sport.BIM || sport == Sport.BIF || sport == Sport.TS)
            {
                if (players.Count != 1)
                {
                    aPIResult.Message = "Add player.";
                    aPIResult.IsSucceed = false;
                    aPIResult.StatusCode = 400;
                    return aPIResult;
                }
            }
            else if (sport == Sport.TD || sport == Sport.P)
            {
                if (teamName == null)
                {
                    aPIResult.Message = "Team name is requierd.";
                    aPIResult.IsSucceed = false;
                    aPIResult.StatusCode = 400;
                    return aPIResult;
                }
                if (players.Count != 2)
                {
                    aPIResult.Message = "Team must contain 2 players.";
                    aPIResult.IsSucceed = false;
                    aPIResult.StatusCode = 400;
                    return aPIResult;
                }
            }
            if (sportType == SportType.U12)
            {
                foreach (PlayerViewModel player in players)
                {
                    DateTime now = DateTime.Now;
                    int totalMonths = (now.Year - player.DateOfBirth.Year) * 12 + now.Month - player.DateOfBirth.Month;
                    if (totalMonths >= 144)
                    {
                        aPIResult.Message = "All team members must be under 12 years old";
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                    }
                }
            }
            else if (sportType == SportType.U14)
            {
                foreach (PlayerViewModel player in players)
                {
                    DateTime now = DateTime.Now;
                    int totalMonths = (now.Year - player.DateOfBirth.Year) * 12 + now.Month - player.DateOfBirth.Month;
                    if (totalMonths >= 168 || totalMonths < 144)
                    {
                        aPIResult.Message = "All team members must be under 14 years old and 12 years old or older.";
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                    }
                }
            }
            else if (sportType == SportType.U18)
            {
                foreach (PlayerViewModel player in players)
                {
                    DateTime now = DateTime.Now;
                    int totalMonths = (now.Year - player.DateOfBirth.Year) * 12 + now.Month - player.DateOfBirth.Month;
                    if (totalMonths >= 216 || totalMonths < 168)
                    {
                        aPIResult.Message = "All team members must be under 18 years old and 14 years old or older.";
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                    }
                    if (player.NationalId == null || player.NationalIdImage == null)
                    {
                        aPIResult.Message = "National ID is required for all players on your team";
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                    }
                }
            }
            else if (sportType == SportType.P45)
            {
                foreach (PlayerViewModel player in players)
                {
                    DateTime now = DateTime.Now;
                    int totalMonths = (now.Year - player.DateOfBirth.Year) * 12 + now.Month - player.DateOfBirth.Month;
                    if (totalMonths < 540)
                    {
                        aPIResult.Message = "All team members must be above 45 years old.";
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                    }
                    if (player.NationalId == null || player.NationalIdImage == null)
                    {
                        aPIResult.Message = "National ID is required for all players on your team";
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                    }
                }
            }
            else if (sportType == SportType.OAT || sportType == SportType.DC || sportType == SportType.TPC)
            {
                foreach (PlayerViewModel player in players)
                {
                    DateTime now = DateTime.Now;
                    int totalMonths = (now.Year - player.DateOfBirth.Year) * 12 + now.Month - player.DateOfBirth.Month;
                    if (totalMonths > 192)
                    {
                        if (player.NationalId == null || player.NationalIdImage == null)
                        {
                            aPIResult.Message = "National ID is required for all plus 16 years old players";
                            aPIResult.IsSucceed = false;
                            aPIResult.StatusCode = 400;
                            return aPIResult;
                        }
                    }
                }
            }
            foreach (PlayerViewModel player in players)
            {
                Player teamPlayer = new()
                {
                    Name = player.Name,
                    DateOfBirth = new DateOnly(player.DateOfBirth.Year, player.DateOfBirth.Month, player.DateOfBirth.Day),
                    Email = player.Email,
                    Gender = player.Gender,
                    PhoneNumber = player.PhoneNumber,
                    NationalId = player.NationalId,
                    Sport = sport,
                    SportType = sportType
                };
                IFormFile file = player.PersonalImage;
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                teamPlayer.PersonalImageUrl = baseUrl + uniqueFileName;
                player.PersonalImageUrl = uniqueFileName;
                if (player.NationalIdImage != null)
                {
                    IFormFile file2 = player.NationalIdImage;
                    string uniqueFileName2 = Guid.NewGuid().ToString() + "_" + file2.FileName;
                    teamPlayer.NationalIdImageUrl = baseUrl + uniqueFileName2;
                    player.NationalIdImageUrl = uniqueFileName2;
                }
                team.Players.Add(teamPlayer);
            }
            try
            {
                await AddAsync(team);
                await unitOfWork.CommitAsync();
                foreach (PlayerViewModel player in players)
                {
                    FileStream fileStream = new FileStream(
                    Path.Combine(
                    Directory.GetCurrentDirectory(), "Content", "Images", player.PersonalImageUrl),
                    FileMode.Create);
                    await player.PersonalImage.CopyToAsync(fileStream);
                    fileStream.Position = 0;
                    fileStream.Close();
                    if (player.NationalIdImage != null)
                    {
                        FileStream fileStream2 = new FileStream(
                        Path.Combine(
                        Directory.GetCurrentDirectory(), "Content", "Images", player.NationalIdImageUrl),
                        FileMode.Create);
                        await player.NationalIdImage.CopyToAsync(fileStream2);
                        fileStream2.Position = 0;
                        fileStream2.Close();
                    }

                }
                aPIResult.Message = "Joined Successfully!";
                aPIResult.IsSucceed = true;
                aPIResult.StatusCode = 200;
                return aPIResult;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("conflicted"))
                {
                    aPIResult.StatusCode = 401;
                    aPIResult.Message = "User not exist";
                    aPIResult.IsSucceed = false;
                    return aPIResult;
                }
                else
                {
                    string errorMessage = ex.InnerException.Message;

                    if (errorMessage.Contains("'IX_Players_Email_Sport_SportType'"))
                    {
                        int startIndex = errorMessage.IndexOf('(') + 1;
                        int endIndex = errorMessage.IndexOf(',', startIndex);
                        string duplicateEmail = errorMessage.Substring(startIndex, endIndex - startIndex).Trim();

                        aPIResult.Message = $"Email '{duplicateEmail}' already exists. Please choose a different email address.";
                    }
                    else if (errorMessage.Contains("'IX_Players_NationalId'"))
                    {
                        int startIndex = errorMessage.IndexOf('(') + 1;
                        int endIndex = errorMessage.IndexOf(',', startIndex);
                        string duplicateNationalId = errorMessage.Substring(startIndex, endIndex - startIndex).Trim();

                        aPIResult.Message = $"National ID '{duplicateNationalId}' already exists. Please choose a different one.";
                    }
                    else if (errorMessage.Contains("'IX_Teams_Name_Sport_SportType'"))
                    {
                        aPIResult.Message = "Team name already exists. Please choose another one.";
                    }
                    else
                    {
                        aPIResult.Message = errorMessage;
                    }

                    aPIResult.IsSucceed = false;
                    aPIResult.StatusCode = 400;
                    return aPIResult;
                }
            }
        }
        public async Task<APIResult<List<UserSportsViewModel>>> GetUserSportsAsync(string userId)
        {
            APIResult<List<UserSportsViewModel>> aPIResult = new();
            List<UserSportsViewModel> userSports = await GetAll().Where(i => i.CaptainId == userId).GroupBy(i => new { i.Sport, i.SportType })
                .Select(i => new UserSportsViewModel()
                {
                    SportId = i.Key.Sport,
                    SportTypeId = i.Key.SportType,
                    Sport = i.Key.Sport == Sport.FTM ? "Football Team, Males" : i.Key.Sport == Sport.P ? "Padel" :
                    i.Key.Sport == Sport.BIF ? "Basketball Individual, Females" : i.Key.Sport == Sport.BIM ? "Basketball Individual, Males" :
                     i.Key.Sport == Sport.BTF ? "Basketball Team, Females" : i.Key.Sport == Sport.TS ? "Tennis Single" :
                i.Key.Sport == Sport.TD ? "Tennis Double" : i.Key.Sport == Sport.BTM ? "Basketball Team, Males" : "Football Team, Females",
                    SportType = i.Key.SportType == SportType.U18 ? "U 18" : i.Key.SportType == SportType.TPC ? "3 point competition" :
                i.Key.SportType == SportType.P45 ? "+45" : i.Key.SportType == SportType.U12 ? "U 12" :
                i.Key.SportType == SportType.U14 ? "U 14" : i.Key.SportType == SportType.DC ? "Dunking competition" : "Open Age",
                    TeamsCount = i.Count()
                }).ToListAsync();
            if (userSports.Count > 0)
            {
                foreach (UserSportsViewModel userSportsViewModel in userSports)
                {
                    userSportsViewModel.SportName = userSportsViewModel.Sport + " " + userSportsViewModel.SportType;

                }
                aPIResult.Data = userSports;
                aPIResult.Message = "Your tournments";
                aPIResult.IsSucceed = true;
                aPIResult.StatusCode = 200;
                return aPIResult;
            }
            else
            {
                aPIResult.Data = userSports;
                aPIResult.Message = "No tournments found";
                aPIResult.IsSucceed = false;
                aPIResult.StatusCode = 201;
                return aPIResult;
            }
        }
        public async Task<APIResult<List<TeamViewModelInList>>> GetUserTeamsBySportAndSportTypeId(Sport sport, SportType sportType, string userId)
        {
            APIResult<List<TeamViewModelInList>> aPIResult = new();
            List<TeamViewModelInList> teams = await GetAll().Where(i => i.CaptainId == userId && i.Sport == sport && i.SportType == sportType)
                .Select(i => i.ToVTeamViewModelInList()).ToListAsync();
            if (teams.Count > 0)
            {
                aPIResult.Data = teams;
                aPIResult.Message = "Your teams";
                aPIResult.IsSucceed = true;
                aPIResult.StatusCode = 200;
                return aPIResult;
            }
            else
            {
                aPIResult.Message = "No teams founded";
                aPIResult.IsSucceed = false;
                aPIResult.StatusCode = 201;
                return aPIResult;
            }
        }
        public async Task<APIResult<TeamViewModel>> GetTeamsAsync(string teamId)
        {
            APIResult<TeamViewModel> aPIResult = new();
            TeamViewModel team = await GetAll().Where(i => i.Id == teamId).Select(i => i.ToTeamViewModel()).FirstOrDefaultAsync();
            if (team != null)
            {
                aPIResult.Data = team;
                aPIResult.StatusCode = 200;
                aPIResult.IsSucceed = true;
                aPIResult.Message = "Your team information";
                return aPIResult;
            }
            else
            {
                aPIResult.StatusCode = 201;
                aPIResult.IsSucceed = false;
                aPIResult.Message = "You don't have a reservation. Please make a reservation first.";
                return aPIResult;
            }
        }
        public async Task<APIResult<List<PlayerViewModelInTeam>>> GetPlayersAsync(string teamId)
        {
            APIResult<List<PlayerViewModelInTeam>> aPIResult = new();
            List<PlayerViewModelInTeam> players = await playerManager.GetAll().Where(i => i.TeamId == teamId).Select(i => i.ToPlayerViewModelInTeam()).ToListAsync();
            if (players.Count > 0)
            {
                aPIResult.Message = "Your team players";
                aPIResult.Data = players;
                aPIResult.IsSucceed = true;
                aPIResult.StatusCode = 200;
                return aPIResult;
            }
            else
            {
                aPIResult.Message = "No players founded";
                aPIResult.IsSucceed = false;
                aPIResult.StatusCode = 201;
                return aPIResult;
            }
        }
        public async Task<APIResult<string>> DeleteTeamAsync(string teamId, string userId)
        {
            APIResult<string> aPIResult = new();
            Team team = await GetAll().Where(i => i.CaptainId == userId && i.Id == teamId).FirstOrDefaultAsync();
            if (team != null)
            {
                List<string> nationalIdsImageUrl = team.Players.Where(i => i.NationalIdImageUrl != null).Select(i => i.NationalIdImageUrl).ToList();
                List<string> personalsImageUrl = team.Players.Where(i => i.PersonalImageUrl != null).Select(i => i.PersonalImageUrl).ToList();
                if (nationalIdsImageUrl.Count > 0)
                {
                    foreach (string nationalId in nationalIdsImageUrl)
                    {
                        if (nationalId != null)
                        {
                            string filePath = Path.Combine(Directory.GetCurrentDirectory(), nationalId);
                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }
                        }
                    }
                }
                if (personalsImageUrl.Count > 0)
                {
                    foreach (string personal in personalsImageUrl)
                    {
                        if (personal != null)
                        {
                            string filePath = Path.Combine(Directory.GetCurrentDirectory(), personal);
                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }
                        }
                    }
                }
                Remove(team);
                await unitOfWork.CommitAsync();
                aPIResult.StatusCode = 200;
                aPIResult.Message = "You have canceled your reservation.";
                aPIResult.IsSucceed = true;
                return aPIResult;
            }
            else
            {
                aPIResult.StatusCode = 400;
                aPIResult.Message = "You don't have a reservation. Please make a reservation first.";
                aPIResult.IsSucceed = false;
                return aPIResult;
            }
        }
        public async Task<APIResult<PlayerFullDetailsViewModel>> GetPlayerByIdAsync(string playerId)
        {
            APIResult<PlayerFullDetailsViewModel> aPIResult = new();
            PlayerFullDetailsViewModel player = await playerManager.GetAll().Where(i => i.Id == playerId).Select(i => i.ToPlayerFullDetailsViewModel()).FirstOrDefaultAsync();
            if (player != null)
            {
                aPIResult.StatusCode = 200;
                aPIResult.Message = "Player full details";
                aPIResult.IsSucceed = true;
                aPIResult.Data = player;
                return aPIResult;
            }
            else
            {
                aPIResult.StatusCode = 400;
                aPIResult.Message = "Player not found";
                aPIResult.IsSucceed = false;
                return aPIResult;
            }
        }
        public async Task AddPlayerInfoToPdf()
        {
            string inputPdfPath = "Content/Images/Player Card.pdf";
            List<Team> teams = await GetAll().ToListAsync();
            foreach (Team team in teams)
            {
                string outputPdfPath = $"Content/Images/{team.CaptainId}.pdf";
                using (FileStream inputStream = new FileStream(inputPdfPath, FileMode.Open, FileAccess.Read))
                using (FileStream outputStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
                {
                    PdfReader reader = new PdfReader(inputStream);
                    PdfStamper stamper = new PdfStamper(reader, outputStream);
                    PdfContentByte canvas = stamper.GetOverContent(1);
                    iTextSharp.text.Image playerImage = iTextSharp.text.Image.GetInstance(team.Captian.PersonalImageUrl);
                    playerImage.SetAbsolutePosition(10, 278);
                    playerImage.ScaleAbsolute(186, 213);
                    canvas.AddImage(playerImage);
                    BaseFont font = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    Font textFont = new Font(font, 16, Font.NORMAL);
                    ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, new Phrase($"{team.Captian.Name}", textFont), 280, 443, 0);
                    ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, new Phrase($"{team.Captian.Gender}", textFont), 280, 402, 0);
                    ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, new Phrase($"{team.Sport}", textFont), 283, 363, 0);
                    ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, new Phrase($"{team.Captian.DateOfBirth}", textFont), 310, 327, 0);
                    ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, new Phrase($"{team.Name}", textFont), 320, 287, 0);
                    stamper.Close();
                    reader.Close();
                }
                foreach (Player player in team.Players)
                {
                    outputPdfPath = $"Content/Images/{player.Id.ToString()}.pdf";
                    using (FileStream inputStream1 = new FileStream(inputPdfPath, FileMode.Open, FileAccess.Read))
                    using (FileStream outputStream1 = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
                    {
                        PdfReader reader1 = new PdfReader(inputStream1);
                        PdfStamper stamper1 = new PdfStamper(reader1, outputStream1);
                        PdfContentByte canvas1 = stamper1.GetOverContent(1);
                        iTextSharp.text.Image playerImage1 = iTextSharp.text.Image.GetInstance(player.PersonalImageUrl);
                        playerImage1.SetAbsolutePosition(10, 278);
                        playerImage1.ScaleAbsolute(186, 213);
                        canvas1.AddImage(playerImage1);
                        BaseFont font1 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        Font textFont1 = new Font(font1, 16, Font.NORMAL);
                        ColumnText.ShowTextAligned(canvas1, Element.ALIGN_LEFT, new Phrase($"{player.Name}", textFont1), 278, 441, 0);
                        ColumnText.ShowTextAligned(canvas1, Element.ALIGN_LEFT, new Phrase(team.Sport.ToString()[0].ToString() == "F" ?
                            "Football" : team.Sport.ToString()[0].ToString() == "B" ? "BasketBall" : team.Sport.ToString()[0].ToString()
                            == "T" ? "Tennis" : "Basketball", textFont1), 278, 401, 0);
                        ColumnText.ShowTextAligned(canvas1, Element.ALIGN_LEFT, new Phrase($"{player.Gender}", textFont1), 286, 363, 0);
                        ColumnText.ShowTextAligned(canvas1, Element.ALIGN_LEFT, new Phrase(team.Sport.ToString()[2..].ToString() == "U12" ?
                            "Under 12" : team.Sport.ToString()[2..].ToString() == "U14" ? "Under 14" : team.Sport.ToString()[2..].ToString() == "U16" ?
                            "Under 16" : team.Sport.ToString()[2..].ToString() == "U20" ?
                            "Under 20" : team.Sport.ToString()[2..].ToString() == "U40" ?
                            "Under 40" : "Above 40", textFont1), 312, 324, 0);
                        ColumnText.ShowTextAligned(canvas1, Element.ALIGN_LEFT, new Phrase($"{team.Name}", textFont1), 320, 286, 0);
                        stamper1.Close();
                        reader1.Close();
                    }
                }
            }
        }
        public async Task<APIResult<string>> EditPlayerAsync(UpdatePlayerViewModel playerViewModel)
        {
            APIResult<string> aPIResult = new();
            Player player = await playerManager.FindByStringIdAsync(playerViewModel.PlayerId);
            if (player != null)
            {
                string oldNationalIdImage = "";
                string oldPersonalImage = "";
                player.PhoneNumber = playerViewModel.PhoneNumber;
                player.Name = playerViewModel.Name;
                player.Email = playerViewModel.Email;
                if (player.Sport == Sport.FTM)
                {
                    if (playerViewModel.Gender == Gender.female)
                    {
                        aPIResult.Message = "Team mumber must be male";
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                    }
                }
                else if (player.Sport == Sport.BTM)
                {
                    if (playerViewModel.Gender == Gender.female)
                    {
                        aPIResult.Message = "Team mumber must be male";
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                    }
                }
                else if (player.Sport == Sport.FTF)
                {
                    if (playerViewModel.Gender == Gender.male)
                    {
                        aPIResult.Message = "Team mumber must be female";
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                    }
                }
                else if (player.Sport == Sport.BTF)
                {
                    if (playerViewModel.Gender == Gender.male)
                    {
                        aPIResult.Message = "Team mumbers must be female";
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                    }
                }
                if (player.SportType == SportType.U12)
                {
                    DateTime now = DateTime.Now;
                    int totalMonths = (now.Year - playerViewModel.DateOfBirth.Year) * 12 + now.Month - playerViewModel.DateOfBirth.Month;
                    if (totalMonths >= 144)
                    {
                        aPIResult.Message = "Team member must be under 12 years old";
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                    }
                }
                else if (player.SportType == SportType.U14)
                {
                    DateTime now = DateTime.Now;
                    int totalMonths = (now.Year - playerViewModel.DateOfBirth.Year) * 12 + now.Month - playerViewModel.DateOfBirth.Month;
                    if (totalMonths >= 168 || totalMonths < 144)
                    {
                        aPIResult.Message = "Team member must be under 14 years old and 12 years old or older.";
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                    }
                }
                else if (player.SportType == SportType.U18)
                {
                    DateTime now = DateTime.Now;
                    int totalMonths = (now.Year - playerViewModel.DateOfBirth.Year) * 12 + now.Month - playerViewModel.DateOfBirth.Month;
                    if (totalMonths >= 216 || totalMonths < 168)
                    {
                        aPIResult.Message = "Team member must be under 18 years old and 14 years old or older.";
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                    }
                    if (playerViewModel.NationalId != null)
                    {
                        player.NationalId = playerViewModel.NationalId;
                    }
                    if (playerViewModel.NationalIdImage != null)
                    {
                        IFormFile file2 = playerViewModel.NationalIdImage;
                        string uniqueFileName2 = Guid.NewGuid().ToString() + "_" + file2.FileName;
                        oldNationalIdImage = player.NationalIdImageUrl;
                        player.NationalIdImageUrl = baseUrl + uniqueFileName2;
                        playerViewModel.NationalIdImageUrl = uniqueFileName2;
                    }
                }
                else if (player.SportType == SportType.P45)
                {
                    DateTime now = DateTime.Now;
                    int totalMonths = (now.Year - playerViewModel.DateOfBirth.Year) * 12 + now.Month - playerViewModel.DateOfBirth.Month;
                    if (totalMonths < 540)
                    {
                        aPIResult.Message = "Team member must be above 45 years old.";
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                    }
                    if (playerViewModel.NationalId != null)
                    {
                        player.NationalId = playerViewModel.NationalId;
                    }
                    if (playerViewModel.NationalIdImage != null)
                    {
                        IFormFile file2 = playerViewModel.NationalIdImage;
                        string uniqueFileName2 = Guid.NewGuid().ToString() + "_" + file2.FileName;
                        oldNationalIdImage = player.NationalIdImageUrl;
                        player.NationalIdImageUrl = baseUrl + uniqueFileName2;
                        playerViewModel.NationalIdImageUrl = uniqueFileName2;
                    }
                }
                else if (player.SportType == SportType.OAT || player.SportType == SportType.DC || player.SportType == SportType.TPC)
                {
                    DateTime now = DateTime.Now;
                    int totalMonths = (now.Year - player.DateOfBirth.Year) * 12 + now.Month - player.DateOfBirth.Month;
                    if (totalMonths > 192)
                    {
                        if (playerViewModel.NationalId == null)
                        {
                            aPIResult.Message = "National ID is required for plus 16 years old players";
                            aPIResult.IsSucceed = false;
                            aPIResult.StatusCode = 400;
                            return aPIResult;
                        }
                        else
                        {
                            player.NationalId = playerViewModel.NationalId;
                            if (playerViewModel.NationalIdImage != null)
                            {
                                IFormFile file2 = playerViewModel.NationalIdImage;
                                string uniqueFileName2 = Guid.NewGuid().ToString() + "_" + file2.FileName;
                                oldNationalIdImage = player.NationalIdImageUrl;
                                player.NationalIdImageUrl = baseUrl + uniqueFileName2;
                                playerViewModel.NationalIdImageUrl = uniqueFileName2;
                            }
                        }
                    }
                }
                player.DateOfBirth = new DateOnly(playerViewModel.DateOfBirth.Year, playerViewModel.DateOfBirth.Month, playerViewModel.DateOfBirth.Day);
                player.Gender = playerViewModel.Gender;
                if (playerViewModel.PersonalImage != null)
                {
                    IFormFile file = playerViewModel.PersonalImage;
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    oldPersonalImage = player.PersonalImageUrl;
                    player.PersonalImageUrl = baseUrl + uniqueFileName;
                    playerViewModel.PersonalImageUrl = uniqueFileName;
                }
                try
                {
                    playerManager.Update(player);
                    await unitOfWork.CommitAsync();
                    if (playerViewModel.PersonalImage != null)
                    {
                        FileStream fileStream = new FileStream(
                        Path.Combine(
                        Directory.GetCurrentDirectory(), "Content", "Images", playerViewModel.PersonalImageUrl),
                        FileMode.Create);
                        await playerViewModel.PersonalImage.CopyToAsync(fileStream);
                        fileStream.Position = 0;
                        fileStream.Close();
                    }
                    if (playerViewModel.NationalIdImage != null)
                    {
                        FileStream fileStream2 = new FileStream(
                        Path.Combine(
                        Directory.GetCurrentDirectory(), "Content", "Images", playerViewModel.NationalIdImageUrl),
                        FileMode.Create);
                        await playerViewModel.NationalIdImage.CopyToAsync(fileStream2);
                        fileStream2.Position = 0;
                        fileStream2.Close();
                    }
                    if (oldPersonalImage != "")
                    {
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), oldPersonalImage);
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                    if (oldNationalIdImage != "")
                    {
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), oldNationalIdImage);
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                    aPIResult.Message = "Player updated Successfully!";
                    aPIResult.IsSucceed = true;
                    aPIResult.StatusCode = 200;
                    return aPIResult;
                }
                catch (DbUpdateException ex)
                {
                        string errorMessage = ex.InnerException.Message;
                        if (errorMessage.Contains("'IX_Players_Email_Sport_SportType'"))
                        {
                            int startIndex = errorMessage.IndexOf('(') + 1;
                            int endIndex = errorMessage.IndexOf(',', startIndex);
                            string duplicateEmail = errorMessage.Substring(startIndex, endIndex - startIndex).Trim();

                            aPIResult.Message = $"Email '{duplicateEmail}' already exists. Please choose a different email address.";
                        }
                        else if (errorMessage.Contains("'IX_Players_NationalId'"))
                        {
                            int startIndex = errorMessage.IndexOf('(') + 1;
                            int endIndex = errorMessage.IndexOf(',', startIndex);
                            string duplicateNationalId = errorMessage.Substring(startIndex, endIndex - startIndex).Trim();

                            aPIResult.Message = $"National ID '{duplicateNationalId}' already exists. Please choose a different one.";
                        }
                        else
                        {
                            aPIResult.Message = errorMessage;
                        }
                        aPIResult.IsSucceed = false;
                        aPIResult.StatusCode = 400;
                        return aPIResult;
                }
            }
            else
            {
                aPIResult.Message = "Player not found";
                aPIResult.StatusCode = 400;
                aPIResult.IsSucceed = false;
                return aPIResult;
            }

        }
    }
}



