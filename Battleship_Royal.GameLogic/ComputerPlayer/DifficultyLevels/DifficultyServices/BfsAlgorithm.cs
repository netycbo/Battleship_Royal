using Battleship_Royal.GameLogic.ComputerPlayer.DifficultyLevels.DifficultyServices.Interfaces;
using Battleship_Royal.GameLogic.GameBoard.GameBoardServices;
using Battleship_Royal.GameLogic.GameContext.Interfaces;
using Battleship_Royal.GameLogic;

public class BfsAlgorithm : IBfsAlgorithm
{
    private readonly IGameContext _gameContext;
    private readonly IGameBoardServices _gameBoard;

    public BfsAlgorithm(IGameContext gameContext, IGameBoardServices gameBoard)
    {
        _gameContext = gameContext ?? throw new ArgumentNullException(nameof(gameContext));
        _gameBoard = gameBoard ?? throw new ArgumentNullException(nameof(gameBoard));
    }

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

    public Target BFS(int startRow, int startCol)
    {
        var board = _gameContext.HumanPlayerBoard;

        Queue<Target> queue = new Queue<Target>();
        HashSet<Target> visited = new HashSet<Target>();

        queue.Enqueue(new Target(startRow, startCol));

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            // Próba ataku na obecny cel
            if (_gameBoard.Attack(current.Row, current.Col, board))
            {
                return current;
            }

            // Dodanie sąsiadów do kolejki
            AddNeighbor(current.Row - 1, current.Col, queue, visited, board);
            AddNeighbor(current.Row + 1, current.Col, queue, visited, board);
            AddNeighbor(current.Row, current.Col - 1, queue, visited, board);
            AddNeighbor(current.Row, current.Col + 1, queue, visited, board);
        }

        // Zwróć nieistniejący cel, jeśli nic nie zostało znalezione
        return new Target(-1, -1);
    }

    private void AddNeighbor(int row, int col, Queue<Target> queue, HashSet<Target> visited, Cell[,] board)
    {
        // Sprawdzenie, czy współrzędne są w granicach planszy
        if (row >= 0 && row < board.GetLength(0) && col >= 0 && col < board.GetLength(1))
        {
            var neighbor = new Target(row, col);

            // Dodanie do kolejki tylko, jeśli nie odwiedzono wcześniej
            if (!visited.Contains(neighbor))
            {
                queue.Enqueue(neighbor);
                visited.Add(neighbor);
            }
        }
    }
}
