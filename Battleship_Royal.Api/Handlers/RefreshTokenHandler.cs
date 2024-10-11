using AutoMapper;
using Battleship_Royal.Api.Dtos;
using Battleship_Royal.Api.Handlers.Services;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Api.Requests.Players;
using Battleship_Royal.Api.Responses.Players;
using Battleship_Royal.Data.DbContext;
using Battleship_Royal.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Battleship_Royal.Api.Handlers
{
    public class RefreshTokenHandler(IMapper mapper, BattleshipsDbContext context, UserManager<ApplicationUser> userManager, IGenerateJwtToken generateJwtToken, IGenerateRefreshToken generateRefreshToken)
        : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
    {
        public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);
            var userToken = await context.Tokens.SingleOrDefaultAsync(x=>x.RefreshToken == request.RefreshToken);

            if (userToken == null || userToken.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                Console.WriteLine("Invalid refresh token");
            }
            var roles = await userManager.GetRolesAsync(user);
            var newJwtToken = await generateJwtToken.GenerateToken(user, roles);
            var newRefreshToken = generateRefreshToken.Generate();
            userToken.RefreshToken = newRefreshToken;
            userToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(5);
            await userManager.UpdateAsync(user);

            return new RefreshTokenResponse
            {
                Data = mapper.Map<RefreshTokenDto>(user),
            };
        }
    }
}
