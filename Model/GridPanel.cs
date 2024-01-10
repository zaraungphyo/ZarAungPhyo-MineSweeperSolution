using System.Collections.Generic;

namespace MineSweeperSolution.Model
{
    public class GridPanel
    {
        private int _NumOfRows = 0;
        private int _NumOfColumns = 0;
        private string[,] _Grid = null;
        public string[,] Grid
        {
            get
            {
                return _Grid;
            }
            set { _Grid = value; }
        }

        /// <summary>
        /// all revealed adjacent squares locations
        /// </summary>
        public IList<SquareLocation> SquareLocations { get; set; }

        /// <summary>
        /// selected square (grid cell) by user iniput
        /// </summary>
        public SquareLocation SelectedSquareLocation { get; set; }
        public int NumOfRows
        {
            get
            {

                return _NumOfRows;
            }
            set { _NumOfRows = value; }
        }
        public int NumOfColumns
        {
            get
            {
                return _NumOfColumns;
            }
            set { _NumOfColumns = value; }
        }
    }
}
