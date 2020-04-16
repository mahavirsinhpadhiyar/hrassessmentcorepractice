using HRAssessmentAPI.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Repositories.UserRepository;
using Shared.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HRAssessmentAPI.Services
{
    //article like:
    //https://jasonwatmore.com/post/2018/08/14/aspnet-core-21-jwt-authentication-tutorial-with-example-api#users-controller-cs
    /// <summary>
    /// user service contains helper methods
    /// like generate user login json web token
    /// </summary>
    public interface IUserService
    {
        Task<UserVM> Authenticate(LoginVM loginVM);
    }
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;
        public UserService(IOptions<AppSettings> appSettings,IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }
        public async Task<UserVM> Authenticate(LoginVM loginVM)
        {
            var user = await _userRepository.Login(loginVM);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;
            user.ConfirmPassword = null;

            return user;
        }
    }
}
