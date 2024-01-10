using MineSweeperSolution.Model;
using System.Collections.Generic;

namespace MineSweeperSolution.Service
{
    /// <summary>
    /// To allocate mine in the grid square and determine if selected square has mine.
    /// </summary>
    public interface IMineLocator
    {
        /// <summary>
        /// To get updated mine field
        /// </summary>
        Mine GetMineField { set; get; }
       // IList<SquareLocation> MineLocations  {   get;   }

        /// <summary>
        /// Allocate mine randomly into the grid square
        /// </summary>
        void AllocateMines(int[,] mineLocations);
        //void AllocateMines();

        ///// <summary>
        ///// Uncovered square contains mine?
        ///// </summary>
        ///// <param name="row"></param>
        ///// <param name="col"></param>
        ///// <param name="numOfMineContain"></param>
        ///// <returns>true if uncovered square contains mine otherwise false</returns>
        //bool HasMine(int row, int col, out int numOfMineContain);

        /// <summary>
        /// Uncovered square contains mine?
        /// </summary>
        /// <param name="location"></param>
        /// <returns>true if uncovered square contains mine otherwise false</returns>
        bool HasMine(int adjacentRowIndex, int adjacentColumnIndex);
    }
}
