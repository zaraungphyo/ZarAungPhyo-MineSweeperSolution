
namespace MineSweeperSolution.Service
{
    public interface IRevealMine
    {
        /// <summary>
        /// Display Number of mines contain in selected square.
        /// </summary>
        /// <returns>true if selected square contain mine; otherwise is false</returns>
        bool RevealMineField();
        /// <summary>
        /// uncover all adjacent squares until it reaches squares that do have adjacent mines
        /// </summary>
        /// <param name="firstAttempt"></param>
        /// <param name="UpdateAdjacentSquare"></param>
        void RevealAdjacentMine(DelegateGridGenerator UpdateAdjacentSquare);
    }
}
