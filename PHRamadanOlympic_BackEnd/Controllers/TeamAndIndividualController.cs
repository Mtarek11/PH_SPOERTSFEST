using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Models;
using Reposatiory;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using ViewModels;
using static Reposatiory.TeamManager;

namespace WebApis
{
    /// <summary>
    /// Team and Individual apis
    /// </summary>
    /// <param name="_teamManager"></param>
    public class TeamAndIndividualController(TeamManager _teamManager) : ControllerBase
    {
        private readonly TeamManager teamManager = _teamManager;
        /// <summary>
        /// Export all players to excell sheet
        /// </summary>
        /// <returns></returns>
        [HttpPost("ExportAllPlayersToExcellSheet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResult<string>))]
        public async Task<IActionResult> ExportPlayersToExcellSheetAsync()
        {
            APIResult<string> result = await teamManager.ExportAllPlayersToExcellAsync();
            return new JsonResult(result)
            {
                StatusCode = result.StatusCode
            };
        }
        ///// <summary>
        ///// Print players cards
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost("PrintPlayerCards")]
        //public async Task<IActionResult> Print()
        //{
        //    await teamManager.AddPlayerInfoToPdf();
        //    return Ok("hel");
        //}
        /// <summary>
        /// Create team ==> Sport type enum 1 for football, 2 for basketball, 3 for tennis, 4 for padel ==> neglect National id image url, personal image url and player id
        /// </summary>
        /// <param name="Sport"></param>
        /// <param name="TeamName"></param>
        /// <param name="Players"></param>
        /// <param name="sportType"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("api/CreateTeam")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResult<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(APIResult<string>))]
        public async Task<IActionResult> CreateTeamAsync([FromQuery, Required] Sport Sport, [FromQuery, Required] SportType sportType, [FromQuery] string TeamName,
            [FromForm] List<PlayerViewModel> Players)
        {
            if (ModelState.IsValid)
            {
                Claim userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                string userId = userIdClaim.Value;
                APIResult<string> result = await teamManager.CreateTeamAsync(TeamName, Sport, sportType, userId, Players);
                return new JsonResult(result)
                { 
                    StatusCode = result.StatusCode
                };
            } 
            else
            {
                return BadRequest(new APIResult<string>()
                {
                    Message = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)),
                    IsSucceed = false,
                    StatusCode = 400
                });
            }
        }
        /// <summary>
        /// Get user sports
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("api/GetUserSports")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult<List<UserSportsViewModel>>))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(APIResult<List<UserSportsViewModel>>))]
        public async Task<IActionResult> GetSportsAsync()
        {
            Claim userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId = userIdClaim.Value;
            APIResult<List<UserSportsViewModel>> result = await teamManager.GetUserSportsAsync(userId);
            return new JsonResult(result)
            {
                StatusCode = result.StatusCode
            };
        }
        /// <summary>
        /// Get teams
        /// </summary>
        /// <param name="sport"></param>
        /// <param name="sportType"></param>
        /// <returns></returns> 
        [Authorize]
        [HttpGet("api/GetTeamsBySportAndSportTypeId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult<List<TeamViewModelInList>>))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(APIResult<List<TeamViewModelInList>>))]
        public async Task<IActionResult> GetTeamsAsync([FromQuery, Required] Sport sport, [FromQuery, Required] SportType sportType)
        {
            Claim userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId = userIdClaim.Value;
            APIResult<List<TeamViewModelInList>> result = await teamManager.GetUserTeamsBySportAndSportTypeId(sport, sportType, userId);
            return new JsonResult(result)
            {
                StatusCode = result.StatusCode
            };
        }
        /// <summary>
        /// Get team 
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("api/GetTeamById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult<TeamViewModel>))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(APIResult<TeamViewModel>))]
        public async Task<IActionResult> GetTeamAsync([FromQuery, Required] string teamId)
        {
            APIResult<TeamViewModel> result = await teamManager.GetTeamsAsync(teamId);
            return new JsonResult(result)
            {
                StatusCode = result.StatusCode
            };
        }
        /// <summary>
        /// Get players in the team 
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("api/GetPlayers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult<List<PlayerViewModelInTeam>>))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(APIResult<List<PlayerViewModelInTeam>>))]
        public async Task<IActionResult> GetPlayersAsync([FromQuery, Required] string teamId)
        {
            APIResult<List<PlayerViewModelInTeam>> result = await teamManager.GetPlayersAsync(teamId);
            return new JsonResult(result)
            {
                StatusCode = result.StatusCode
            };
        }
        /// <summary>
        /// Delete team 
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("api/DeleteTeam")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult<string>))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(APIResult<string>))]
        public async Task<IActionResult> DeleteTeamAsync([FromQuery, Required] string teamId)
        {
            Claim userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId = userIdClaim.Value;
            APIResult<string> result = await teamManager.DeleteTeamAsync(teamId, userId);
            return new JsonResult(result)
            {
                StatusCode = result.StatusCode
            };
        }
        /// <summary>
        /// Get player details by id
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns> 
        [Authorize]
        [HttpGet("api/GetPlayerById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult<PlayerFullDetailsViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResult<PlayerFullDetailsViewModel>))]
        public async Task<IActionResult> GetPlayerByIdsync([FromQuery, Required] string playerId)
        {
            APIResult<PlayerFullDetailsViewModel> result = await teamManager.GetPlayerByIdAsync(playerId);
            return new JsonResult(result)
            {
                StatusCode = result.StatusCode
            };
        }
        /// <summary>
        /// Update player
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns> 
        [Authorize]
        [HttpPut("api/UpdatePlayer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResult<string>))]
        public async Task<IActionResult> UpdatePlayerAsync([FromForm] UpdatePlayerViewModel viewModel)
        {
            APIResult<string> result = await teamManager.EditPlayerAsync(viewModel);
            return new JsonResult(result)
            {
                StatusCode = result.StatusCode
            };
        }
    }
}
