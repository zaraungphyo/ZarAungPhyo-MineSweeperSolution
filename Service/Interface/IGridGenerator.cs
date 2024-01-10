using MineSweeperSolution.Model;
using System.Collections.Generic;

namespace MineSweeperSolution.Service
{
    public interface IGridGenerator
    {
        /// <summary>
        /// Update Selected Square such as A1 as an actual grid location (0,0)
        /// </summary>
        /// <param name="selectedLocation"></param>
        void UpdateSelectedSquare(SquareLocation selectedLocation);

        IMineLocator MineFieldLocator { get; }

        /// <summary>
        /// it is used to display user how many adjacent mine counter contains in their selected square i.e This square contains {0} adjacent mines.
        /// </summary>
        int AdjacentMineCounter { get;  }

        /// <summary>
        /// 
        /// </summary>
        string[,] MaskedGrid { get; }
        /// <summary>
        /// To get actual row index base on row header
        /// </summary>
        /// <param name="cIndex"></param>
        /// <returns></returns>
        int GetActualRowIndex(char cIndex);

        /// <summary>
        /// To get actual column index base on col header
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        int GetActualColumnIndex(int Index);

        /// <summary>
        /// mask and populate square in grid
        /// </summary>
        void GenerateGrid();

        /// <summary>
        /// Update and populate Mine Field upon user uncovered square
        /// </summary>
        /// <param name="locations"></param>
        void GenerateGrid(IList<SquareLocation> locations);

        /// <summary>
        /// Place mine onto the grid
        /// </summary>
        void SetupMine();
    }
}
