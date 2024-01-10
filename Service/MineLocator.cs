
using System;
using System.Collections.Generic;
using MineSweeperSolution.Model;

namespace MineSweeperSolution.Service
{
    
    /// <summary>
    /// To allocate mine in the grid square and determine if selected square has mine.
    /// </summary>
    public class MineLocator: IMineLocator
    {
        private Mine mine = null;
       // private IList<SquareLocation> mineLocations = new List<SquareLocation>();
        public MineLocator(Mine mine)
        {
            this.mine = mine;
        }

        /// <summary>
        /// To get updated mine field
        /// </summary>
        public Mine GetMineField { set { mine = value; } get { return mine; } }
      //  public IList<SquareLocation> MineLocations { get { return mineLocations; } }

        /// <summary>
        /// to setup mine randomly base on number of mine
        /// </summary>
        public void AllocateMines(int[,] mineLocations)
        {
            try
            {
                Mapper.MineLocations.Clear();
                for (int i = 0; i < mine.NumOfMine; i++)
                {
                    SquareLocation mineLocation = new SquareLocation();
                    mineLocation.RowIndex = mineLocations[i, 0];
                    mineLocation.ColumnIndex = mineLocations[i, 1]; 
                    mine.Grid[mineLocation.RowIndex, mineLocation.ColumnIndex] = Constants.MineValue;
                    Mapper.MineLocations.Add(mineLocation); 
                }

                MineCalculator calculator = new MineCalculator(this);

                for (int row = 0; row < mine.Grid.GetLength(0); row++)
                {
                    for (int col = 0; col < mine.Grid.GetLength(1); col++)
                    {
                        calculator.FindNextAdjacentMine(row, col);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Uncovered square contains mine?
        /// </summary>
        /// <param name="adjacentRowIndex"></param>
        /// <param name="adjacentColumnIndex"></param>
        /// <returns>true if uncovered square contains mine otherwise false</returns>
        public bool HasMine(int adjacentRowIndex, int adjacentColumnIndex)
        {
            try
            {
                string cellValue = mine.Grid[adjacentRowIndex, adjacentColumnIndex];
                if (cellValue == Constants.MineValue)
                {
                    //selected square contain mine
                    return true;
                }
                else
                {
                    //selected square does not contain mine
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
