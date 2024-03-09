using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using System.Security.Claims;
using System.Text;
using ViewModels;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Net.Mail;
using System.Net;

namespace Reposatiory
{
    public class UserManager(RamadanOlympicsContext _mydB, UserManager<User> _userManager
            , IConfiguration _configuration) : MainManager<User>(_mydB)
    {
        private readonly UserManager<User> userManager = _userManager;
        private readonly IConfiguration configuration = _configuration;
        string baseUrl = "Content/Images/";
        public async Task<IdentityResult> CreatePasswordAsync(User user, string password)
        {
            try
            {
                IdentityResult identityResult = await userManager.CreateAsync(user, password);
                return identityResult;
            }
            catch (ArgumentNullException)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Description = "Password is requierd",
                });
            }
            catch (DbUpdateException ex)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Description = ex.InnerException.Message,
                });
            }
        }
        public async Task<APIResult<UserDataViewModel>> SignUpAsync(UserSignUpViewModel viewModel)
        {
            APIResult<UserDataViewModel> aPIResult = new();
            string role;
            string roleInData;
            DateTime now = DateTime.Now;
            DateOnly newDateOfBirth = new DateOnly(viewModel.DateOfBirth.Year, viewModel.DateOfBirth.Month, viewModel.DateOfBirth.Day);
            int totalMonths = (now.Year - viewModel.DateOfBirth.Year) * 12 + now.Month - viewModel.DateOfBirth.Month;
            if (totalMonths >= 192)
            {
                if (viewModel.NationalId == null || viewModel.NationalIdImage == null)
                {
                    aPIResult.StatusCode = 401;
                    aPIResult.IsSucceed = false;
                    aPIResult.Message = "National id is requierd";
                    return aPIResult;
                }
                if (totalMonths >= 480)
                {
                    role = "A40";
                    roleInData = "Above 40";
                }else if(totalMonths >= 240 && totalMonths < 480)
                {
                    role = "U40";
                    roleInData = "Under 40";
                }
                else
                {
                    role = "U20";
                    roleInData = "Under 20";
                }
                if (viewModel.Gender == Gender.male)
                {
                    role = 'M' + role ;
                }
                else
                {
                    role = 'F' + role ;
                }
                User user = new()
                {
                    Name = viewModel.FullName,
                    NationalId = viewModel.NationalId,
                    PhoneNumber = viewModel.PhoneNumber,
                    DateOfBirth = newDateOfBirth,
                    Email = viewModel.Email,
                    Gender = viewModel.Gender,
                    UserName = viewModel.Email
                };
                IFormFile file = viewModel.NationalIdImage;
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                user.NationalIdImageUrl = baseUrl + uniqueFileName;
                IFormFile file2 = viewModel.PersonalImage;
                string uniqueFileName2 = Guid.NewGuid().ToString() + "_" + file2.FileName;
                user.PersonalImageUrl = baseUrl + uniqueFileName2;
                IdentityResult identityResult = await CreatePasswordAsync(user, viewModel.Password);
                if (identityResult.Succeeded)
                {
                    FileStream fileStream = new FileStream(
                    Path.Combine(
                        Directory.GetCurrentDirectory(), "Content", "Images", uniqueFileName),
                    FileMode.Create);
                    await file.CopyToAsync(fileStream);
                    fileStream.Position = 0;
                    fileStream.Close();
                    FileStream fileStream2 = new FileStream(
                    Path.Combine(
                        Directory.GetCurrentDirectory(), "Content", "Images", uniqueFileName2),
                    FileMode.Create);
                    await file2.CopyToAsync(fileStream2);
                    fileStream2.Position = 0;
                    fileStream2.Close();
                    await userManager.AddToRoleAsync(user, role);
                    List<Claim> claims = new();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    claims.Add(new Claim(ClaimTypes.Role, role));
                    JwtSecurityToken securityToken = new(
                            claims: claims,
                            signingCredentials: new SigningCredentials(
                                key: new SymmetricSecurityKey(
                                    Encoding.ASCII.GetBytes(this.configuration["JWT:Key"])
                                    ),
                                algorithm: SecurityAlgorithms.HmacSha384
                                ),
                            expires: DateTime.Now.AddMonths(12)
                            );
                    UserDataViewModel signUpData = new()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                        DateOfBirth = newDateOfBirth,
                        Gender = viewModel.Gender,
                        Role = roleInData
                    };
                    aPIResult.Data = signUpData;
                    aPIResult.Message = "User authorized";
                    aPIResult.IsSucceed = true;
                    aPIResult.StatusCode = 200;
                    return aPIResult;
                }
                else
                {
                    string message = identityResult.Errors.Select(i => i.Description).FirstOrDefault();
                    aPIResult.IsSucceed = false;
                    aPIResult.Message = message.Contains("'IX_AspNetUsers_Email'") || message.Contains("Username ") ?
                        "Email already taken" : message.Contains("'IX_AspNetUsers_PhoneNumber'") ? "Phone number already taken" :
                        message.Contains("IX_AspNetUsers_NationalId") ? "National id already used befor" :
                        message == "Password requierd" ? "Password requierd" : message;
                    aPIResult.StatusCode = 400;
                    return aPIResult;
                }
            }
            else
            {
                if (totalMonths < 144)
                {
                    role = "U12";
                    roleInData = "Under 12";
                }
                else if (totalMonths >= 144 && totalMonths < 168)
                {
                    role = "U14";
                    roleInData = "Under 14";
                }
                else
                {
                    role = "U16";
                    roleInData = "Under 16";
                }
                if (viewModel.Gender == Gender.male)
                {
                    role = 'M' + role;
                }
                else
                {
                    role = 'F' + role;
                }
                User user2 = new()
                {
                    Name = viewModel.FullName,
                    PhoneNumber = viewModel.PhoneNumber,
                    DateOfBirth = newDateOfBirth,
                    Email = viewModel.Email,
                    Gender = viewModel.Gender,
                    UserName = viewModel.Email
                };
                IFormFile file3 = viewModel.PersonalImage;
                string uniqueFileName3 = Guid.NewGuid().ToString() + "_" + file3.FileName;
                user2.PersonalImageUrl = baseUrl + uniqueFileName3;
                IdentityResult identityResult2 = await CreatePasswordAsync(user2, viewModel.Password);
                if (identityResult2.Succeeded)
                {
                    FileStream fileStream3 = new FileStream(
                       Path.Combine(
                           Directory.GetCurrentDirectory(), "Content", "Images", uniqueFileName3),
                       FileMode.Create);
                    await file3.CopyToAsync(fileStream3);
                    fileStream3.Position = 0;
                    fileStream3.Close();
                    await userManager.AddToRoleAsync(user2, role);
                    List<Claim> claims = new();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user2.Id));
                    claims.Add(new Claim(ClaimTypes.Role, role));
                    JwtSecurityToken securityToken = new(
                            claims: claims,
                            signingCredentials: new SigningCredentials(
                                key: new SymmetricSecurityKey(
                                    Encoding.ASCII.GetBytes(this.configuration["JWT:Key"])
                                    ),
                                algorithm: SecurityAlgorithms.HmacSha384
                                ),
                            expires: DateTime.Now.AddMonths(12)
                            );
                    UserDataViewModel signUpData = new()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                        DateOfBirth = newDateOfBirth,
                        Gender = viewModel.Gender,
                        Role = roleInData
                    };
                    aPIResult.Data = signUpData;
                    aPIResult.Message = "User authorized";
                    aPIResult.IsSucceed = true;
                    aPIResult.StatusCode = 200;
                    return aPIResult;
                }
                else
                {
                    string message = identityResult2.Errors.Select(i => i.Description).FirstOrDefault();
                    aPIResult.IsSucceed = false;
                    aPIResult.Message = message.Contains("'IX_AspNetUsers_Email'") || message.Contains("Username ")
                        ? "Email already taken" : message.Contains("'IX_AspNetUsers_PhoneNumber'") ? "Phone number already taken" :
                        message == "Password requierd" ? "Password requierd" : message;
                    aPIResult.StatusCode = 400;
                    return aPIResult;
                }
            }
        }
        public async Task<APIResult<UserDataViewModel>> SignInAsync(UserSignInViewModel viewModel)
        {
            APIResult<UserDataViewModel> APIResult = new();
            User user = await userManager.FindByEmailAsync(viewModel.Email);
            if (user != null)
            {
                APIResult<UserDataViewModel> aPIResult = await CheckPasswordAsync(user, viewModel.Password);
                return aPIResult; 
            }
            else
            {
                APIResult.IsSucceed = false;
                APIResult.Message = "User not exist";
                APIResult.StatusCode = 401;
                return APIResult;
            }
        }
        public async Task<APIResult<UserDataViewModel>> CheckPasswordAsync(User user, string password)
        {
            APIResult<UserDataViewModel> APIResult = new();
            List<Claim> claims = new();
            bool checkPassword = await userManager.CheckPasswordAsync(user, password);
            if (checkPassword)
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                IList<string> roles = await userManager.GetRolesAsync(user);
                if (roles.Count > 0)
                {
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }
                JwtSecurityToken securityToken = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: new SigningCredentials(
                        key: new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(this.configuration["JWT:Key"])
                            ),
                        algorithm: SecurityAlgorithms.HmacSha384
                        ),
                    expires: DateTime.Now.AddMonths(12)
                    );
                bool isHaveATeam = user.Teams.Any();
                UserDataViewModel signInDataView = new()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    Role = roles[0][1..] == "U12" ? "Under 12" : roles[0][1..] == "U14" ? "Under 14" : roles[0][1..] == "U16" ? "Under 16" :
                    roles[0][1..] == "U20" ? "Under 20" : roles[0][1..] == "U40" ? "Under 40" : "Above 40" 
                };
                APIResult.Data = signInDataView;
                APIResult.Message = "Welcome back, check side bar for joining our tournments";
                APIResult.StatusCode = 200;
                APIResult.IsSucceed = true;
                return APIResult;
            }
            else
            {
                APIResult.Message = "User not exist";
                APIResult.IsSucceed = false;
                APIResult.StatusCode = 401;
                return APIResult;
            }
        }
        public async Task<APIResult<string>> GetForgetPasswordCodeAsync(string email)
        {
            APIResult<string> APIResult = new();
            User user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                string code = await userManager.GeneratePasswordResetTokenAsync(user);
                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("mohamedtarek70m@gmail.com", "ptuw vvlf rkue elgh");
                    client.EnableSsl = true;

                    MailAddress from = new MailAddress("mohamedtarek70m@gmail.com");
                    MailAddress to = new MailAddress(email);
                    string subject = "Password Change Request";
                    string body = $"Your password change code for PH ramadan olympics is: {code}";
                    MailMessage message = new MailMessage(from, to)
                    {
                        Subject = subject,
                        Body = body,
                    };
                    client.Send(message);
                }
                APIResult.Message = "Code sent to your email address";
                APIResult.IsSucceed = true;
                APIResult.StatusCode = 200;
                return APIResult;
            }
            else
            {
                APIResult.Message = "User not found";
                APIResult.IsSucceed = false;
                APIResult.StatusCode = 401;
                return APIResult;
            }
        }
        public async Task<APIResult<string>> ResetPasswordAsync(UserResetPasswordViewModel viewModel)
        {
            APIResult<string> aPIResult = new();
            User user = await userManager.FindByEmailAsync(viewModel.Email);
            if (user != null)
            {
                IdentityResult identityResult = await userManager.ResetPasswordAsync(user, viewModel.Code, viewModel.NewPassword);
                if (identityResult.Succeeded)
                {
                    aPIResult.Message = "Password changed";
                    aPIResult.StatusCode = 200;
                    aPIResult.IsSucceed = true;
                    return aPIResult;
                }
                else
                {
                    aPIResult.Message = "Code not valid";
                    aPIResult.StatusCode = 401;
                    aPIResult.IsSucceed = false;
                    return aPIResult;
                }
            }
            else
            {
                aPIResult.Message = "User not found";
                aPIResult.StatusCode = 401;
                aPIResult.IsSucceed = false;
                return aPIResult;
            }
        }
    }
}

