using MineSweeperSolution.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MineSweeperSolution.Service
{
    public class AdjacentMineCalculator : MineCalculator, IAdjacentMineCalculator
    {
        private int NumOfRows = 0;
        private int NumOfColumns = 0;
        private Mine mine = null;
        private IMineLocator mineLocator = null;
        string rowcolumnKey = "{0}|{1}";
        private Dictionary<string, bool> NextAdjacentMarker = new Dictionary<string, bool>();
        private Action<int, int> CalculateAdjacentCells;

        public AdjacentMineCalculator(IMineLocator mineLocator) : base(mineLocator)
        {
            this.mineLocator = mineLocator;
            mine = mineLocator.GetMineField;
            mine.SquareLocations = new List<SquareLocation>();
            NumOfRows = mine.NumOfRows;
            NumOfColumns = mine.NumOfColumns;

            CalculateAdjacentCells = new Action<int, int>(CalculateBelowAdjacent);
            CalculateAdjacentCells += CalculateBelowLeftAdjacent;
            CalculateAdjacentCells += CalculateBelowRightAdjacent;
            CalculateAdjacentCells += CalculateLeftAdjacent;
            CalculateAdjacentCells += CalculateRightAdjacent;
            CalculateAdjacentCells += CalculateUpperAdjacent;
            CalculateAdjacentCells += CalculateUpperLeftAdjacent;
            CalculateAdjacentCells += CalculateUpperRightAdjacent;
        }

        /// <summary>
        /// If an uncovered square has no adjacent mines, the program should automatically uncover all adjacent squares until it reaches squares that do have adjacent mines.
        /// </summary>
        /// <param name="UpdateAdjacentSquare"></param>
        public void FindAdjacentMine(DelegateGridGenerator UpdateAdjacentSquare)
        {
            try
            {
                int selectedRowIndex = mine.SelectedSquareLocation.RowIndex;
                int selectedColIndex = mine.SelectedSquareLocation.ColumnIndex;

                //Uncover Adjacent Square
                CalculateAdjacentCells(selectedRowIndex, selectedColIndex);
                CalculateAdjacentCells -= CalculateBelowAdjacent;
                CalculateAdjacentCells -= CalculateBelowLeftAdjacent;
                CalculateAdjacentCells -= CalculateBelowRightAdjacent;
                CalculateAdjacentCells -= CalculateLeftAdjacent;
                CalculateAdjacentCells -= CalculateRightAdjacent;
                CalculateAdjacentCells -= CalculateUpperAdjacent;
                CalculateAdjacentCells -= CalculateUpperLeftAdjacent;
                CalculateAdjacentCells -= CalculateUpperRightAdjacent;

                UpdateAdjacentSquare(mine.SquareLocations);
            }
            catch (Exception ex)
            {
                throw;
            }
        }   

        /// <summary>
        /// Use validate Grid Cell Index.
        /// If selected cell has no mine, mark it to uncover the selected cell which later to use to prompt user the updated minefield and stop finding to reveal its adjacent cell, if selected cell already has adjacent mine counter
        /// If adjacent Cell has no mine and its value alredy has adjacent mine counter, mark it to uncover which later to use to prompt user the updated minefield
        /// </summary>
        /// <param name="selectedRowIndex"></param>
        /// <param name="adjacentRowIndex"></param>
        /// <param name="selectedColumnIndex"></param>
        /// <param name="adjacentColumnIndex"></param>
        public override void ValidateGridIndex(int selectedRowIndex, int adjacentRowIndex, int selectedColumnIndex, int adjacentColumnIndex)
        {
            if (adjacentRowIndex < NumOfRows && adjacentRowIndex > -1 && adjacentColumnIndex < NumOfColumns && adjacentColumnIndex > -1)
            {
                SquareLocation temp = new SquareLocation() { RowIndex = adjacentRowIndex, ColumnIndex = adjacentColumnIndex };
                if (!mine.SquareLocations.Any(x => x.RowIndex == temp.RowIndex && x.ColumnIndex == temp.ColumnIndex))
                {
                    string strAdjacentCellValue = mine.Grid[adjacentRowIndex, adjacentColumnIndex];
                    string strSelectedCellValue = mine.Grid[selectedRowIndex, selectedColumnIndex];

                    if (strSelectedCellValue != null && strSelectedCellValue != Constants.MineValue)
                    {
                        int selectedCellValue = -1;
                        if (int.TryParse(strSelectedCellValue, out selectedCellValue))
                        {
                            //revealed selected cell
                            if (!mine.SquareLocations.Any(x => x.RowIndex == selectedRowIndex && x.ColumnIndex == selectedColumnIndex))
                            {
                                mine.SquareLocations.Add(new SquareLocation() { RowIndex = selectedRowIndex, ColumnIndex = selectedColumnIndex });
                            }
                            if (selectedCellValue > 0)
                            {
                                return; //do not find it adjacent cell to reveal
                            }
                        }
                    }

                    if (strAdjacentCellValue != null && strAdjacentCellValue != Constants.MineValue)
                    {
                        int adjacentCellValue = -1;
             
                        if (int.TryParse(strAdjacentCellValue, out adjacentCellValue))
                        {
                            if (adjacentCellValue > 0)
                            {
                                //revealed its adjacent cell
                                if (!mine.SquareLocations.Any(x => x.RowIndex == adjacentRowIndex && x.ColumnIndex == adjacentColumnIndex))
                                {
                                    mine.SquareLocations.Add(new SquareLocation() { RowIndex = adjacentRowIndex, ColumnIndex = adjacentColumnIndex });
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// if adjacent cell has no mine and its value has no adjacent mine counter, then repeat to find its adjacent mine counter recursively
        /// </summary>
        /// <param name="adjacentRowIndex"></param>
        /// <param name="adjacentColumnIndex"></param>
        /// <param name="selectedRowIndex"></param>
        /// <param name="selectedColumnIndex"></param>
        /// <returns>true in order to find its adjacent mine counter recursively; otherwise stop finding its adjacent mine counter recursively </returns>
        public override bool ValidateNextAdjacentGridIndex(int adjacentRowIndex, int adjacentColumnIndex, int selectedRowIndex = 0, int selectedColumnIndex = 0)
        {
            try
            {
                if (adjacentRowIndex < NumOfRows && adjacentRowIndex > -1 && adjacentColumnIndex < NumOfColumns && adjacentColumnIndex > -1)
                {
                    int GridCellValue = -1; //-1 mean cell has mine
                    if (!int.TryParse(mine.Grid[adjacentRowIndex, adjacentColumnIndex], out GridCellValue))
                    {
                        GridCellValue = -1;
                    }
                    if (GridCellValue == -1 || GridCellValue > 0)
                    {
                        return false;//break recursive
                    }
                    string tempRowcolumnKey = string.Format(rowcolumnKey, adjacentRowIndex, adjacentColumnIndex);
                    if (!NextAdjacentMarker.ContainsKey(tempRowcolumnKey))
                    {
                        NextAdjacentMarker.Add(tempRowcolumnKey, false);
                    }
                    if (!mineLocator.HasMine(selectedRowIndex, selectedColumnIndex)
                            && mine.Grid[selectedRowIndex, selectedColumnIndex] == "0")
                    {
                        if (!mineLocator.HasMine(adjacentRowIndex, adjacentColumnIndex)
                         && mine.Grid[adjacentRowIndex, adjacentColumnIndex] == "0"
                         && !NextAdjacentMarker[tempRowcolumnKey]
                         && !mine.SquareLocations.Any(x => x.RowIndex == adjacentRowIndex && x.ColumnIndex == adjacentColumnIndex))
                        {
                            NextAdjacentMarker[tempRowcolumnKey] = true;
                            //repeat Adjacent square calcuation
                            FindNextAdjacentMine(adjacentRowIndex, adjacentColumnIndex);
                        }
                        else
                        {
                            return false;//break recursive
                        }
                    }
                    else
                    {
                        return false;//break recursive
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
