

namespace MineSweeperSolution.Service
{
    public interface IMineUncoveredValidator
    {
        /// <summary>
        /// Validate user input. Accepted input such as A1,B2 etc.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        bool ValidateMineUncovered(string input, out string errorMessage);

        /// <summary>
        /// Validate column is in range against Grid Size
        /// </summary>
        /// <param name="colHeader"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        bool ValidateMineUncovered(int colHeader, out string errorMessage);

        /// <summary>
        /// Validate row is in range against Grid Size
        /// </summary>
        /// <param name="rowHeader"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        bool ValidateMineUncovered(char rowHeader, out string errorMessage);
    }
}
