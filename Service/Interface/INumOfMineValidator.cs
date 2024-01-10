
namespace MineSweeperSolution.Service
{
    public interface INumOfMineValidator
    {
        
        /// <summary>
        /// To validate number of Mine format
        /// </summary>
        /// <param name="input"></param>
        /// <param name="numOfMine"></param>
        /// <param name="errorMessage"></param>
        /// <returns>true if input is numeric; otherwise is false</returns>
        bool ValidateNumOfMine(string input, ref int numOfMine, out string errorMessage);

        /// <summary>
        /// To validate number of Mine format
        /// </summary>
        /// <param name="NumOfMine"></param>
        /// <param name="errorMessage"></param>
        ///  <returns>true NumOfMine is greater than 0; otherwise is false</returns>
        bool ValidateNumOfMine(int NumOfMine, int totalCells, out string errorMessage);
    }
}
