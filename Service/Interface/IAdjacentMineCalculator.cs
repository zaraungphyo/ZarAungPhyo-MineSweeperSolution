

namespace MineSweeperSolution.Service
{
    public interface IAdjacentMineCalculator
    {
        /// <summary>
        /// If an uncovered square has no adjacent mines, the program should automatically uncover all adjacent squares until it reaches squares that do have adjacent mines.
        /// </summary>
        /// <param name="UpdateAdjacentSquare"></param>
        void FindAdjacentMine(DelegateGridGenerator UpdateAdjacentSquare);
    }
}
