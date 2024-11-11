using Battleship_Royal.GameLogic.GameBoard.GameBoardServices.Helpers.Interfaces;

namespace Battleship_Royal.Data.Services.GameServices.Helpers
{
    public class HasDifferentShape : IHasDifferentShape
    {

        public bool DifferentShape(List<(int Row, int Col)> coordinates)
        {
            if (coordinates.Count != 4)
                return false;

            var sortedCoordinates = coordinates.OrderBy(c => c.Row).ThenBy(c => c.Col).ToList();

            return

                (sortedCoordinates[0].Row == sortedCoordinates[1].Row && sortedCoordinates[1].Row == sortedCoordinates[2].Row &&
                 sortedCoordinates[2].Col + 1 == sortedCoordinates[3].Col && sortedCoordinates[2].Row + 1 == sortedCoordinates[3].Row) ||

                (sortedCoordinates[0].Col == sortedCoordinates[1].Col && sortedCoordinates[1].Col == sortedCoordinates[2].Col &&
                 sortedCoordinates[2].Row + 1 == sortedCoordinates[3].Row && sortedCoordinates[2].Col + 1 == sortedCoordinates[3].Col) ||

                (sortedCoordinates[0].Row == sortedCoordinates[1].Row && sortedCoordinates[1].Row == sortedCoordinates[2].Row &&
                 sortedCoordinates[0].Col + 1 == sortedCoordinates[3].Col && sortedCoordinates[0].Row - 1 == sortedCoordinates[3].Row) ||

                (sortedCoordinates[0].Col == sortedCoordinates[1].Col && sortedCoordinates[1].Col == sortedCoordinates[2].Col &&
                 sortedCoordinates[0].Row + 1 == sortedCoordinates[3].Row && sortedCoordinates[0].Col - 1 == sortedCoordinates[3].Col) ||

                (sortedCoordinates[0].Row == sortedCoordinates[1].Row && sortedCoordinates[1].Row == sortedCoordinates[2].Row &&
                 sortedCoordinates[1].Row - 1 == sortedCoordinates[3].Row && sortedCoordinates[1].Col == sortedCoordinates[3].Col) ||

                (sortedCoordinates[0].Row == sortedCoordinates[1].Row && sortedCoordinates[1].Row == sortedCoordinates[2].Row &&
                 sortedCoordinates[1].Row + 1 == sortedCoordinates[3].Row && sortedCoordinates[1].Col == sortedCoordinates[3].Col) ||

                (sortedCoordinates[0].Col == sortedCoordinates[1].Col && sortedCoordinates[1].Col == sortedCoordinates[2].Col &&
                 sortedCoordinates[1].Col - 1 == sortedCoordinates[3].Col && sortedCoordinates[1].Row == sortedCoordinates[3].Row) ||

                (sortedCoordinates[0].Col == sortedCoordinates[1].Col && sortedCoordinates[1].Col == sortedCoordinates[2].Col &&
                 sortedCoordinates[1].Col + 1 == sortedCoordinates[3].Col && sortedCoordinates[1].Row == sortedCoordinates[3].Row);
        }
        public bool IsSquareShape(List<(int Row, int Col)> coordinates)
        {
            if (coordinates.Count != 4)
                return false;

            var sortedCoordinates = coordinates.OrderBy(c => c.Row).ThenBy(c => c.Col).ToList();

            return (sortedCoordinates[0].Row == sortedCoordinates[1].Row &&
                    sortedCoordinates[2].Row == sortedCoordinates[3].Row &&
                    sortedCoordinates[0].Col == sortedCoordinates[2].Col &&
                    sortedCoordinates[1].Col == sortedCoordinates[3].Col &&
                    sortedCoordinates[1].Col == sortedCoordinates[0].Col + 1 &&
                    sortedCoordinates[2].Row == sortedCoordinates[0].Row + 1);

        }
    }
}
