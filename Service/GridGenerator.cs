using System;
using System.Collections.Generic;
using MineSweeperSolution.Model;

namespace MineSweeperSolution.Service
{
    public class GridGenerator :MessagePrompter, IGridGenerator
    {
        private Mine mine;
        private string[,] maskedGrid;
        private IAlphabetPrinter alphabetPrinter = null;
        private IMineLocator mineLocator = null;
        private int adjacentMineCounter = 0;
        public GridGenerator(int size,int numOfMine)
        {
            mine = new Mine();
            mine.NumOfMine = numOfMine;
            mine.NumOfRows = size;
            mine.NumOfColumns = size;
            mine.Grid = new string[mine.NumOfRows, mine.NumOfColumns];
            mine.Grid = FillGrid(mine.Grid, "0");
            maskedGrid = (string[,])mine.Grid.Clone();
            alphabetPrinter = new AlphabetPrinter();
            mineLocator = new MineLocator(mine);
        }

        public void UpdateSelectedSquare(SquareLocation selectedSquareLocation) {
            mine.SelectedSquareLocation = selectedSquareLocation;
        }

        public IMineLocator MineFieldLocator { get { return mineLocator; } }

        /// <summary>
        /// it is used to display user how many adjacent mine counter contains in their selected square i.e This square contains {0} adjacent mines.
        /// </summary>
        public int AdjacentMineCounter { get { return adjacentMineCounter; }  }
        public string[,] MaskedGrid { get { return maskedGrid; } }
        public int GetActualRowIndex(char cIndex) {
            if (!Mapper.RowMapper.ContainsKey(cIndex))
            {
                return -1;
            }
            int actualRowIndex = Mapper.RowMapper[cIndex];
            return actualRowIndex;
        }

        public int GetActualColumnIndex(int Index)
        {
            if (!Mapper.ColMapper.ContainsKey(Index))
            {
                return -1;
            }
            int actualColwIndex = Mapper.ColMapper[Index];
            return actualColwIndex;
        }

        protected override void PromptMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void SetupMine()
        {
            int[,] mineLocations = new int[mine.NumOfMine, 2];
            Random rnd = new Random();
            for (int i = 0; i < mine.NumOfMine; i++)
            {
                //rowIndex
                mineLocations[i, 0] = rnd.Next(0, mine.Grid.GetLength(0));
                //columnIndex
                mineLocations[i, 1] = rnd.Next(0, mine.Grid.GetLength(1));
            }
            mineLocator.AllocateMines(mineLocations);
        }

        /// <summary>
        /// mask and populate square in grid
        /// </summary>
        public void GenerateGrid()
        {
            try
            {
                string[,] grid = FillGrid(maskedGrid, Constants.MaskCellValue);

                PromptMessage(Constants.MineFieldTitle);

                //print column header
                Console.Write("{0 ,-15}", "");
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    Console.Write("{0 ,-15}", PrintColumnHeader(col));
                }
                Console.WriteLine();

                //print content
                for (int row = 0; row < grid.GetLength(0); row++)
                {
                    //print row header
                    Console.Write("{0, -15}", PrintRowHeader(row));

                    for (int col = 0; col < grid.GetLength(1); col++)
                    {
                        //print content body
                        Console.Write("{0 ,-15}", grid[row, col]);
                    }
                    //enter new line every row's columns printed
                    Console.WriteLine();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update and populate Mine Field upon user uncovered square
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        public void GenerateGrid(IList<SquareLocation> locations)
        {
            try
            {
                PromptMessage(Constants.UpdatedMineFieldTitle);
                foreach (SquareLocation location in locations)
                {
                    //Merge all uncovered squares to maskedGrid in order to display to user
                    string strRevealedSquareValue = mine.Grid[location.RowIndex, location.ColumnIndex];
                    maskedGrid[location.RowIndex, location.ColumnIndex] = strRevealedSquareValue;
                }

                //get selected square's adjacent mine counter
                int revealedSelectedSquareValue;
                if (int.TryParse(maskedGrid[mine.SelectedSquareLocation.RowIndex, mine.SelectedSquareLocation.ColumnIndex], out revealedSelectedSquareValue))
                {
                    adjacentMineCounter = Convert.ToInt32(revealedSelectedSquareValue);
                }

                //print column header
                Console.Write("{0 ,-15}", "");
                for (int col = 0; col < maskedGrid.GetLength(1); col++)
                {
                    Console.Write("{0 ,-15}", PrintColumnHeader(col));
                }
                Console.WriteLine();

                //print content
                for (int row = 0; row < maskedGrid.GetLength(0); row++)
                {
                    //print row header
                    Console.Write("{0, -15}", PrintRowHeader(row));

                    for (int col = 0; col < maskedGrid.GetLength(1); col++)
                    {
                        //print content body
                        Console.Write("{0 ,-15}", maskedGrid[row, col]);
                    }
                    //enter new line every row's columns printed
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// mask grid cell
        /// </summary>
        /// <param name="gsize"></param>
        /// <returns></returns>
        protected string[,] FillGrid(string[,] gsize, string mask)
        {
            try
            {
                for (int row = 0; row < gsize.GetLength(0); row++)
                {
                    for (int col = 0; col < gsize.GetLength(1); col++)
                    {
                        gsize[row, col] = mask;
                    }
                }
                return gsize;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Print Grid Column Header
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        protected int PrintColumnHeader(int idx)
        {
            int column = (idx + 1);
            Mapper.ColMapper[column] = idx;
            return column;
        }

        /// <summary>
        /// Print Grid Row Header
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        protected char PrintRowHeader(int idx = 0)
        {
            char _char = alphabetPrinter.getAlphabet(idx);
            Mapper.RowMapper[_char] = idx;
            return _char;
        }
    }
}
