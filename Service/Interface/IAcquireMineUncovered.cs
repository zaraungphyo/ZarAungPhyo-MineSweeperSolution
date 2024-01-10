
namespace MineSweeperSolution.Service
{
    public interface IAcquireMineUncovered
    {
        char RowIndex { set; get; }
        int ColumnIndex { set; get; }
        void GetMineUncovered();
    }
}
