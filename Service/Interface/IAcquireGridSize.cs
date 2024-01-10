
namespace MineSweeperSolution.Service
{
    public interface IAcquireGridSize
    {
        int GetGridSize();
        bool GridSizeInput(string strGridSize, ref int gridSize);
    }
}
