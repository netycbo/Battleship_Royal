using AutoMapper;
using Battleship_Royal.Api.Dtos;
using Battleship_Royal.Api.Requests;
using Battleship_Royal.Api.Responses;
using Battleship_Royal.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Battleship_Royal.Api.Handlers
{
    public class LoginHandler(IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration ) : IRequestHandler<LoginRequest, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
          var userExist = await  userManager.FindByNameAsync(request.Email);
          if (userExist == null)
          {
              throw new Exception("User not found");
          }
          
          var correctPassword = userManager.CheckPasswordAsync(userExist, request.Password);
          if (!correctPassword.Result)
          {
              throw new Exception("Wrong password");
          }
          var token = GenerateJwtToken(userExist);
          return new LoginResponse
          {
              Data = mapper.Map<LoginDto>(userExist)
          };
          
        }
        public async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var roles = await userManager.GetRolesAsync(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Actor, user.JoinedDate.ToString())
            };
            
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
        }
    }
}
