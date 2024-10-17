using AutoMapper;
using Battleship_Royal.Api.Dtos;
using Battleship_Royal.Api.Handlers.Services;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Api.Requests.Players;
using Battleship_Royal.Api.Responses.Players;
using Battleship_Royal.Data.DbContext;
using Battleship_Royal.Data.Entities;
using Battleship_Royal.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Battleship_Royal.Api.Handlers
{
    public class LoginHandler(IMapper mapper,BattleshipsDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration, IGenerateRefreshToken generateRefreshToken, IGenerateJwtToken generateJwtToken)
        : IRequestHandler<LoginRequest, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var userExist = await userManager.FindByEmailAsync(request.Email);
            if (userExist == null)
            {
                return new LoginResponse { Error = "User not found" };
            }

            var correctPassword = await userManager.CheckPasswordAsync(userExist, request.Password);
            if (!correctPassword)
            {
                return new LoginResponse { Error = "Wrong password" };
            }
            var roles = await userManager.GetRolesAsync(userExist);

            var token = await generateJwtToken.GenerateToken(userExist, roles);
            var refreshToken = generateRefreshToken.Generate();
            var userId = userExist.Id;
            
            var saveTokens = new Token
            {
                UserId = userId,
                RefreshToken = refreshToken
            };

            await context.Tokens.AddAsync(saveTokens);
            await context.SaveChangesAsync(cancellationToken);

            var redyToLogInn = mapper.Map<LoginDto>(userExist);
            redyToLogInn.Token = token;
            redyToLogInn.RefreshToken = refreshToken;
            return new LoginResponse
            {
                Data = redyToLogInn,
                

            };
        }      
    }
}
