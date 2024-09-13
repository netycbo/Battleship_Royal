using AutoMapper;
using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Battleship_Royal.Api.Requests;
using Battleship_Royal.Api.Responses;
using MediatR;

namespace Battleship_Royal.Api.Handlers
{
    public class NewGameHandler(IMapper mapper,ISaveGamesServices saveGamesServices) : IRequestHandler<NewGameRequest, NewGameResponse>
    {
        public Task<NewGameResponse> Handle(NewGameRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
