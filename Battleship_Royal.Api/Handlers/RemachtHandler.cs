using AutoMapper;
using Battleship_Royal.Api.Dtos.Game;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Api.Requests.Game;
using Battleship_Royal.Api.Responses.Games;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;


namespace Battleship_Royal.Api.Handlers
{
    public class RemachtHandler(IGameCacheService gameCacheService, IDeserializeService deserializeService) 
        : IRequestHandler<RematchRequest, RematchRersponse>
    {
        public async Task<RematchRersponse> Handle(RematchRequest request, CancellationToken cancellationToken)
        {
            var key = request.GameId;
            var temporaryGameFromRedis = await gameCacheService.GetGameAsync<string>(key);

            if (temporaryGameFromRedis.IsNullOrEmpty())
            {
                throw new Exception("TemporaryGame is null");
            }

            var prepareGame = deserializeService.Deserialize<PrepareGameDto>(temporaryGameFromRedis);
            if (prepareGame == null)
            {
                throw new Exception("PrepareGame is null");
            }

            return new RematchRersponse
            {
                Data = prepareGame
            };
        }

    }
}

