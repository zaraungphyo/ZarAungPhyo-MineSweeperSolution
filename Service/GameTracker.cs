using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperSolution.Service
{
    public static class GameTracker
    {
        public static bool Track(string[,] MaskedGrid, int UncoveredRowIndex, int UncoveredColumnIndex, ref string strGameProgress)
        {
            bool GameStatus = false; //true when game is either failed or success
            int AllNonMineCellRevealedCounter = 0;
            bool exit = false;
            try
            {
                if (UncoveredRowIndex > -1 && UncoveredRowIndex < MaskedGrid.GetLength(0) && UncoveredColumnIndex > -1 && UncoveredColumnIndex < MaskedGrid.GetLength(1))
                {
                    string cellValue = MaskedGrid[UncoveredRowIndex, UncoveredColumnIndex];
                    if (cellValue == Constants.MineValue)
                    {
                        strGameProgress = Constants.FailMessage;
                        GameStatus = true;
                        return GameStatus;
                    }
                }
                for (int row = 0; row < MaskedGrid.GetLength(0); row++)
                {
                    if (exit)
                    {
                        break;
                    }
                    for (int col = 0; col < MaskedGrid.GetLength(1); col++)
                    {
                        if (!Mapper.MineLocations.Any(s => s.RowIndex == row && s.ColumnIndex == col) && MaskedGrid[row, col] == Constants.MaskCellValue)
                        {
                            AllNonMineCellRevealedCounter =- 1;
                     //       strGameProgress = Constants.SuccessMessage;
                            GameStatus = false;
                            exit = true;
                            break;
                        }
                        else
                        {
                            AllNonMineCellRevealedCounter = 0;
                        }
                    }
                }
                if (AllNonMineCellRevealedCounter == 0)
                {
                    strGameProgress = Constants.SuccessMessage;
                    GameStatus = true;
                }
                return GameStatus;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
