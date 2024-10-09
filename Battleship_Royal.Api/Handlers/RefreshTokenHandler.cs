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
    public class RefreshTokenHandler(IMapper mapper, UserManager<ApplicationUser> userManager, IGenerateJwtToken generateJwtToken, IGenerateRefreshToken generateRefreshToken) 
        : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
    {
        public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(x=>x.RefreshToken == request.RefreshToken);

            if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                Console.WriteLine("Invalid refresh token");
            }
            var roles = await userManager.GetRolesAsync(user);
            var newJwtToken = await generateJwtToken.GenerateToken(user, roles);
            var newRefreshToken = generateRefreshToken.Generate();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(5);
            await userManager.UpdateAsync(user);

            return new RefreshTokenResponse
            {
                Data = mapper.Map<RefreshTokenDto>(user),
            };
        }
    }
}
