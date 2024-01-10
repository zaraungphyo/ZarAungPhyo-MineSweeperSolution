

namespace MineSweeperSolution.Service
{
    public interface IAcquireNumOfMines
    {
        int GetNumOfMines(int GridCells);

        bool NumOfMineInput(string strNumOfMine, int GridCells, ref int NumOfMine);
    }
}
