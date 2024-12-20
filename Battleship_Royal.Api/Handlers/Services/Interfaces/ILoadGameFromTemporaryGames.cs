﻿using Battleship_Royal.Api.Dtos;
using Battleship_Royal.Api.Dtos.Game;

namespace Battleship_Royal.Api.Handlers.Services.Interfaces
{
    public interface ILoadGameFromTemporaryGames
    {
        Task LoadFromTemporaryGames(PrepareGameVsComputerDto newGameDto);
    }
}