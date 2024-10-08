﻿namespace Battleship_Royal.Api.Dtos.Game
{
    public class GameStatsDto
    {
        public string PlayerName { get; set; }
        public int NumbersOfGames { get; set; }
        public int NumbersOfWins { get; set; }
        public int NumbersOfLosses { get; set; }
        public TimeSpan HoursOfPlay { get; set; }
        public int QuickestWinInMinutes { get; set; }
        public string? MostGamesWithPlayer { get; set; }
        public int NumbersOfGamesWithThatPlayer { get; set; }
       
    }
}