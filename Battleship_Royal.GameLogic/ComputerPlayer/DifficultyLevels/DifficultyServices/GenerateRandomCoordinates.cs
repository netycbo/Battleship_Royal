using System;
using System.Collections.Generic;
using System.Linq;
using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices.Interfaces;

namespace Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices
{
    public class GenerateRandomCoordinates(Random random) : IGenerateRandomCoordinates
    {
        public int GenerateCoordinates(out int row, out int col)
        {
            row = random.Next(0, 10);
            col = random.Next(0, 10);

            return row * 10 + col;
        }
    }
}
