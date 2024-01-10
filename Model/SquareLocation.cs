
namespace MineSweeperSolution.Model
{
    public class SquareLocation
    {
        private int _RowIndex = 0;
        private int _ColumnIndex = 0;
        public SquareLocation()
        {
            RowIndex = -1;
            ColumnIndex = -1;
        }
        public int RowIndex
        {
            get
            {
                return _RowIndex;
            }
            set { _RowIndex = value; }
        }
        public int ColumnIndex
        {
            get
            {
                return _ColumnIndex;
            }
            set { _ColumnIndex = value; }
        }
    }
}
