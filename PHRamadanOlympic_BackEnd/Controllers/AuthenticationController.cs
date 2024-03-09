using Microsoft.AspNetCore.Mvc;
using Reposatiory;
using System.ComponentModel.DataAnnotations;
using ViewModels;

namespace WebApis
{
    /// <summary>
    /// Authentication APIs
    /// </summary>
    /// <param name="_userManager"></param>
    public class AuthenticationController(UserManager _userManager) : ControllerBase
    {
        private readonly UserManager userManager = _userManager;
        /// <summary>
        /// Sign up ==> national id and national id image are not requierd, unless user above 18 years old, gender ==> 1-male / 2-female
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost("api/SignUp")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(APIResult<UserDataViewModel>))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult<UserDataViewModel>))]
        public async Task<IActionResult> SignUpForUserAsync([FromForm] UserSignUpViewModel viewModel)
        { 
            if (ModelState.IsValid) 
            {
                APIResult<UserDataViewModel> result = await userManager.SignUpAsync(viewModel);
                return new JsonResult(result) 
                { 
                    StatusCode = result.StatusCode 
                };
            } 
            else
            {
                return Unauthorized(new APIResult<UserDataViewModel>()
                {
                    Message = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)),
                    IsSucceed = false,
                    StatusCode = 400
                });
            }
        }
        /// <summary>
        /// Sign in 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost("api/SignIn")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult<UserDataViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(APIResult<UserDataViewModel>))]
        public async Task<IActionResult> SignInForUserAsync([FromForm] UserSignInViewModel viewModel)
        {
            if (ModelState.IsValid) 
            {
                APIResult<UserDataViewModel> result = await userManager.SignInAsync(viewModel);
                return new JsonResult(result)
                { 
                    StatusCode = result.StatusCode
                };
            }
            else
            {
                return Unauthorized(new APIResult<UserDataViewModel>()
                {
                    Message = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)),
                    IsSucceed = false,
                    StatusCode = 401
                });
            }
        }
        /// <summary>
        /// Send forget password code to user email, if the email founded in the DB
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        [HttpPost("api/ForgetPassword")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(APIResult<string>))]
        public async Task<IActionResult> GetForgetPasswordCodeAsync([Required, EmailAddress, FromQuery] string Email)
        {
            if (ModelState.IsValid)
            {
                APIResult<string> result = await userManager.GetForgetPasswordCodeAsync(Email);
                return new JsonResult(result)
                {
                    StatusCode = result.StatusCode
                };
            }
            else
            {
                return Unauthorized(new APIResult<string>()
                {
                    Message = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)),
                    IsSucceed = false,
                    StatusCode = 401
                });
            }
        }
        /// <summary>
        /// Reset user password 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost("api/ResetPassword")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(APIResult<string>))]
        public async Task<IActionResult> ResetPasswordCodeAsync(UserResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                APIResult<string> result = await userManager.ResetPasswordAsync(viewModel);
                return new JsonResult(result)
                {
                    StatusCode = result.StatusCode
                };
            }
            else
            {
                return Unauthorized(new APIResult<string>()
                {
                    Message = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)),
                    IsSucceed = false,
                    StatusCode = 401
                });
            }
        }
    }
}
