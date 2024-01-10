using MineSweeperSolution.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MineSweeperSolution.Service
{
    public class MineCalculator : IMineCalculator
    {
        private int NumOfRows = 0;
        private int NumOfColumns = 0;
        private Mine mine = null;
        private IMineLocator mineLocator = null;

        public MineCalculator(IMineLocator mineLocator)
        {
            this.mineLocator = mineLocator;
            mine = mineLocator.GetMineField;
            mine.SquareLocations = new List<SquareLocation>();
            NumOfRows = mine.NumOfRows;
            NumOfColumns = mine.NumOfColumns;
        }

        public void FindNextAdjacentMine(int adjacentRowIndex, int adjacentColumnIndex)
        {
            try
            {
                CalculateBelowAdjacent(adjacentRowIndex, adjacentColumnIndex);
                CalculateBelowLeftAdjacent(adjacentRowIndex, adjacentColumnIndex);
                CalculateBelowRightAdjacent(adjacentRowIndex, adjacentColumnIndex);
                CalculateLeftAdjacent(adjacentRowIndex, adjacentColumnIndex);
                CalculateRightAdjacent(adjacentRowIndex, adjacentColumnIndex);
                CalculateUpperAdjacent(adjacentRowIndex, adjacentColumnIndex);
                CalculateUpperLeftAdjacent(adjacentRowIndex, adjacentColumnIndex);
                CalculateUpperRightAdjacent(adjacentRowIndex, adjacentColumnIndex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// if adjacent cell has mine then increase 1 to its selected cell value, meaning selected cell value has adjacent mine counter
        /// </summary>
        /// <param name="selectedRowIndex"></param>
        /// <param name="adjacentRowIndex"></param>
        /// <param name="selectedColumnIndex"></param>
        /// <param name="adjacentColumnIndex"></param>
        public virtual void ValidateGridIndex(int selectedRowIndex, int adjacentRowIndex, int selectedColumnIndex, int adjacentColumnIndex)
        {
            if (adjacentRowIndex < NumOfRows && adjacentRowIndex > -1 && adjacentColumnIndex < NumOfColumns && adjacentColumnIndex > -1)
            {
                SquareLocation temp = new SquareLocation() { RowIndex = adjacentRowIndex, ColumnIndex = adjacentColumnIndex };
                if (!mine.SquareLocations.Any(x => x.RowIndex == temp.RowIndex && x.ColumnIndex == temp.ColumnIndex))
                {
                    int adjacentMineCounter = -1;
                    //get selected cell value 
                    string selectedCellValue = mine.Grid[selectedRowIndex, selectedColumnIndex];
                    string adjCellValue = mine.Grid[adjacentRowIndex, adjacentColumnIndex];
                    if (adjCellValue == Constants.MineValue)
                    {
                        if (int.TryParse(selectedCellValue, out adjacentMineCounter))
                        {
                            adjacentMineCounter += 1;
                            mine.Grid[selectedRowIndex, selectedColumnIndex] = adjacentMineCounter.ToString();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// By default, let it always return false to indicate no recursively finding adjcent cell
        /// </summary>
        /// <param name="adjacentRowIndex"></param>
        /// <param name="adjacentColumnIndex"></param>
        /// <param name="selectedRowIndex"></param>
        /// <param name="selectedColumnIndex"></param>
        /// <returns></returns>
        public virtual bool ValidateNextAdjacentGridIndex(int adjacentRowIndex, int adjacentColumnIndex, int selectedRowIndex = 0, int selectedColumnIndex = 0)
        {
            try
            {
                //It is ok to let it return false by default. indicate no recursive when call from MineLocator.AllocateMines()
                //This will override in MineAdjacentCalculator drived class
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void CalculateBelowAdjacent(int selectedRowIndex, int selectedColumnIndex)
        {
            try
            {
                if (selectedRowIndex < NumOfRows && selectedRowIndex > -1 && selectedColumnIndex > -1 && selectedColumnIndex < NumOfColumns)
                {
                    int adjacentRowIndex = selectedRowIndex + 1;
                    int adjacentColumnIndex = selectedColumnIndex;
                    ValidateGridIndex(selectedRowIndex, adjacentRowIndex, selectedColumnIndex, adjacentColumnIndex);
                    if (!ValidateNextAdjacentGridIndex(adjacentRowIndex, adjacentColumnIndex,selectedRowIndex,selectedColumnIndex))
                    {
                        return; //break recurrisve
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void CalculateBelowLeftAdjacent(int selectedRowIndex, int selectedColumnIndex)
        {
            try
            {
                if (selectedRowIndex < NumOfRows && selectedRowIndex > -1 && selectedColumnIndex > -1 && selectedColumnIndex < NumOfColumns)
                {
                    int nRowIndex = selectedRowIndex + 1;
                    int nColumnIndex = selectedColumnIndex - 1;

                    ValidateGridIndex(selectedRowIndex, nRowIndex, selectedColumnIndex, nColumnIndex);
                    if (!ValidateNextAdjacentGridIndex(nRowIndex, nColumnIndex, selectedRowIndex, selectedColumnIndex))
                    {
                        return; //break recurrisve
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void CalculateBelowRightAdjacent(int selectedRowIndex, int selectedColumnIndex)
        {
            try
            {
                if (selectedRowIndex < NumOfRows && selectedRowIndex > -1 && selectedColumnIndex > -1 && selectedColumnIndex < NumOfColumns)
                {
                    int nRowIndex = selectedRowIndex + 1;
                    int nColumnIndex = selectedColumnIndex + 1;
                    ValidateGridIndex(selectedRowIndex, nRowIndex, selectedColumnIndex, nColumnIndex);
                    if (!ValidateNextAdjacentGridIndex(nRowIndex, nColumnIndex, selectedRowIndex, selectedColumnIndex))
                    {
                        return; //break recurrisve
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void CalculateLeftAdjacent(int selectedRowIndex, int selectedColumnIndex)
        {
            try
            {
                if (selectedRowIndex < NumOfRows && selectedRowIndex > -1 && selectedColumnIndex > -1 && selectedColumnIndex < NumOfColumns)
                {
                    int nRowIndex = selectedRowIndex;
                    int nColumnIndex = selectedColumnIndex - 1;
                    ValidateGridIndex(selectedRowIndex, nRowIndex, selectedColumnIndex, nColumnIndex);
                    if (!ValidateNextAdjacentGridIndex(nRowIndex, nColumnIndex,selectedRowIndex, selectedColumnIndex))
                    {
                        return; //break recurrisve
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void CalculateRightAdjacent(int selectedRowIndex, int selectedColumnIndex)
        {
            try
            {
                if (selectedRowIndex < NumOfRows && selectedRowIndex > -1 && selectedColumnIndex > -1 && selectedColumnIndex < NumOfColumns)
                {
                    int nRowIndex = selectedRowIndex;
                    int nColumnIndex = selectedColumnIndex + 1;
                    ValidateGridIndex(selectedRowIndex, nRowIndex, selectedColumnIndex, nColumnIndex);
                    if (!ValidateNextAdjacentGridIndex(nRowIndex, nColumnIndex, selectedRowIndex, selectedColumnIndex))
                    {
                        return; //break recurrisve
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void CalculateUpperAdjacent(int selectedRowIndex, int selectedColumnIndex)
        {
            try
            {
                if (selectedRowIndex < NumOfRows && selectedRowIndex > -1 && selectedColumnIndex > -1 && selectedColumnIndex < NumOfColumns)
                {
                    int nRowIndex = selectedRowIndex - 1;
                    int nColumnIndex = selectedColumnIndex;
                    ValidateGridIndex(selectedRowIndex, nRowIndex, selectedColumnIndex, nColumnIndex);
                    if (!ValidateNextAdjacentGridIndex(nRowIndex, nColumnIndex,selectedRowIndex, selectedColumnIndex))
                    {
                        return; //break recurrisve
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void CalculateUpperLeftAdjacent(int selectedRowIndex, int selectedColumnIndex)
        {
            try
            {
                if (selectedRowIndex < NumOfRows && selectedRowIndex > -1 && selectedColumnIndex > -1 && selectedColumnIndex < NumOfColumns)
                {
                    int nRowIndex = selectedRowIndex - 1;
                    int nColumnIndex = selectedColumnIndex - 1;
                    ValidateGridIndex(selectedRowIndex, nRowIndex, selectedColumnIndex, nColumnIndex);
                    if (!ValidateNextAdjacentGridIndex(nRowIndex, nColumnIndex, selectedRowIndex, selectedColumnIndex))
                    {
                        return; //break recurrisve
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void CalculateUpperRightAdjacent(int selectedRowIndex, int selectedColumnIndex)
        {
            try
            {
                if (selectedRowIndex < NumOfRows && selectedRowIndex > -1 && selectedColumnIndex > -1 && selectedColumnIndex < NumOfColumns)
                {
                    int nRowIndex = selectedRowIndex - 1;
                    int nColumnIndex = selectedColumnIndex + 1;
                    ValidateGridIndex(selectedRowIndex, nRowIndex, selectedColumnIndex, nColumnIndex);
                    if (!ValidateNextAdjacentGridIndex(nRowIndex, nColumnIndex, selectedRowIndex, selectedColumnIndex))
                    {
                        return; //break recurrisve
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
