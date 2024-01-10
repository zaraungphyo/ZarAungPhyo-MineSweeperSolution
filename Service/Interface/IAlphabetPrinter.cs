
namespace MineSweeperSolution.Service
{
    /// <summary>
    /// To get english alphabet based on numeric input
    /// </summary>
    public interface IAlphabetPrinter
    {
        /// <summary>
        /// Use to print row header base on grid dimension
        /// </summary>
        /// <param name="idx"></param>
        /// <returns>English Character</returns>
        char getAlphabet(int idx);
    }
}
