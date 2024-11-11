using System.ComponentModel;
using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices.Interfaces;
using Battleship_Royal.GameLogic.GameBoard;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices;

namespace Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices
{
    public class BfsAlgorithm(IGameBoardServices gameBoard) : IBfsAlgorithm
    {
        public struct Target
        {
            public int Row { get; set; }
            public int Col { get; set; }

            public Target(int row, int col)
            {
                Row = row;
                Col = col;
            }
        }
        public Target BFS(int start)
        {
            Queue<Target> queue = new Queue<Target>();
            HashSet<Target> visited = new HashSet<Target>();

            queue.Enqueue(new Target(0, 0));

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (gameBoard.Attack(current.Row, current.Col))
                {
                    return current;
                }

                AddNeighbor(current.Row - 1, current.Col, queue, visited);
                AddNeighbor(current.Row + 1, current.Col, queue, visited);
                AddNeighbor(current.Row, current.Col - 1, queue, visited);
                AddNeighbor(current.Row, current.Col + 1, queue, visited);
            }

            return new Target(-1, -1);
        }

        private void AddNeighbor(int row, int col, Queue<Target> queue, HashSet<Target> visited)
        {
            if (row >= 0 && row < 10 && col >= 0 && col < 10)
            {
                var neighbor = new Target(row, col);
                if (!visited.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                }
            }
        }


    }
}

