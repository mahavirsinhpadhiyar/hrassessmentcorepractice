using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HRAssessmentAPI.Helpers
{
    public class Authentication
    {
        private readonly AppSettings _appSettings;

        public Authentication(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public string Authenticate(string Id)
        {
            //If accessing usermanager to find the user then following code will be use
            /*var result = ((ClaimsIdentity)httpContextAccessor.HttpContext.User.Identity).FindFirst(e => e.Type == ClaimTypes.NameIdentifier);*/

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

            // remove password before returning
            //user.Password = null;
            //user.ConfirmPassword = null;
        }
    }
}
