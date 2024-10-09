using AutoMapper;
using Battleship_Royal.Api.Dtos;
using Battleship_Royal.Api.Handlers.Services;
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
    public class LoginHandler(IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration, IGenerateRefreshToken generateRefreshToken, IGenerateJwtToken generateJwtToken)
        : IRequestHandler<LoginRequest, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var userExist = await userManager.FindByEmailAsync(request.Email);
            if (userExist == null)
            {
                throw new Exception("User not found");
            }

            var correctPassword = await userManager.CheckPasswordAsync(userExist, request.Password);
            if (!correctPassword)
            {
                throw new Exception("Wrong password");
            }
            var roles = await userManager.GetRolesAsync(userExist);

            var token = await generateJwtToken.GenerateToken(userExist, roles);
            var refreshToken = generateRefreshToken.Generate();
            userExist.RefreshToken = refreshToken;
            await userManager.UpdateAsync(userExist);

            var redyToLogInn = mapper.Map<LoginDto>(userExist);

            return new LoginResponse
            {
                Data = redyToLogInn,
                Token = token,
                RefreshToken = refreshToken
            };
        }      
    }
}
