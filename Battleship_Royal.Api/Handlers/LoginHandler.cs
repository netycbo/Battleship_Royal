using AutoMapper;
using Battleship_Royal.Api.Dtos;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Api.Requests.Players;
using Battleship_Royal.Api.Responses.Players;
using Battleship_Royal.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Battleship_Royal.Api.Handlers
{
    public class LoginHandler(IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration, IGameCacheService gameCache ) 
        : IRequestHandler<LoginRequest, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
          var userExist = await  userManager.FindByEmailAsync(request.Email);
          if (userExist == null)
          {
              throw new Exception("User not found");
          }
          var userId = userExist.Id;

          var correctPassword = await userManager.CheckPasswordAsync(userExist, request.Password);
          if (!correctPassword)
          {
              throw new Exception("Wrong password");
          }
            var roles = await userManager.GetRolesAsync(userExist);

            var token = await GenerateJwtToken(userExist, roles);
            
            var redyToLogInn = mapper.Map<LoginDto>(userExist);

            var sessionStarted = request.StartSession;

            await gameCache.SaveSessionAsync(userId, sessionStarted);

          return new LoginResponse
          {
              Data = redyToLogInn,
              Token = token
          };
          
        }
        public async Task<string> GenerateJwtToken(ApplicationUser user, IList<string> role)
        {
            var roles = await userManager.GetRolesAsync(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Actor, user.JoinedDate.ToString())
            };
            
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(2),
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
        }
    }
}
