using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperSolution.Service
{
    /// <summary>
    /// To get english alphabet based on numeric input
    /// </summary>
    public class AlphabetPrinter: IAlphabetPrinter
    {
        /// <summary>
        /// Use to print row header base on grid dimension
        /// </summary>
        /// <param name="idx"></param>
        /// <returns>English Character</returns>
        public char getAlphabet(int idx)
        {
            try
            {
                char alphabet = Enumerable.Range(idx, 1).Select(i => Convert.ToChar('A' + i)).First();
                return alphabet;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
