﻿using Battleship_Royal.Api.Responses.Games;
using MediatR;

namespace Battleship_Royal.Api.Requests.Game
{
    public class SaveToGamesDbRequest : IRequest<SaveToGamesDbResponse>
    {
    }
}