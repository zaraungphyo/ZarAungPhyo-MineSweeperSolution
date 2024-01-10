namespace MineSweeperSolution.Service
{
    public interface IGridSizeValidator
    {
        /// <summary>
        /// To validate the grid size
        /// </summary>
        /// <param name="input"></param>
        /// <param name="gridSize"></param>
        /// <param name="errorMessage"></param>
        /// <returns>true if input is numeric; otherwise is false</returns>
        bool ValidateGridSize(string input, ref int gridSize, out string errorMessage);


        /// <summary>
        /// To validate the grid size
        /// </summary>
        /// <param name="gridSize"></param>
        /// <param name="errorMessage"></param>
        /// <returns>true if input is within maximun and minimun grid size; otherwise is false</returns>
        bool ValidateGridSize(int gridSize, out string errorMessage);

    }
}
