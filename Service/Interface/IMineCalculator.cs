
namespace MineSweeperSolution.Service
{

    public interface IMineCalculator
    {
        void CalculateBelowAdjacent(int adjacentRowIndex, int adjacentColumnIndex);
        void CalculateBelowLeftAdjacent(int adjacentRowIndex, int adjacentColumnIndex);
        void CalculateBelowRightAdjacent(int adjacentRowIndex, int adjacentColumnIndex);
        void CalculateLeftAdjacent(int adjacentRowIndex, int adjacentColumnIndex);
        void CalculateRightAdjacent(int adjacentRowIndex, int adjacentColumnIndex);
        void CalculateUpperAdjacent(int adjacentRowIndex, int adjacentColumnIndex);
        void CalculateUpperLeftAdjacent(int adjacentRowIndex, int adjacentColumnIndex);
        void CalculateUpperRightAdjacent(int adjacentRowIndex, int adjacentColumnIndex);
    }

}
