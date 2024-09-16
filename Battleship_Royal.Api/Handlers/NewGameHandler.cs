using AutoMapper;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Api.Requests;
using Battleship_Royal.Api.Responses;
using Battleship_Royal.Data.Entities;
using Battleship_Royal.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Battleship_Royal.Api.Handlers
{
    public class NewGameHandler(IMapper mapper, ISaveGamesServices saveGamesServices, IUserIdService idService, UserManager<Game> userManager)
        : IRequestHandler<NewGameRequest, NewGameResponse>
    {
        public async Task<NewGameResponse> Handle(NewGameRequest request, CancellationToken cancellationToken)
        {
            var player1Id = idService.GetUserId();
            if (player1Id == "-1")
            {
                throw new ArgumentException("player1 is not autenticated");
            }
            var player1 = await userManager.FindByIdAsync(player1Id);
            if (player1.IsInGame)
            {
                throw new Exception($"player1: {request.Player1} is allredy in game");
            }
            var player2Id = idService.GetUserId();
            if (player2Id == "-1")
            {
                throw new ArgumentException("player1 is not autenticated");
            }
            var player2 = await userManager.FindByIdAsync(player1Id);
            if (player2.IsInGame)
            {
                throw new Exception($"player1: {request.Player2} is allredy in game");
            }
        }
    }
}
