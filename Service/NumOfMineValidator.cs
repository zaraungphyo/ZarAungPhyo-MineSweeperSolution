using System;

namespace MineSweeperSolution.Service
{
    public class NumOfMineValidator : INumOfMineValidator
    {
        readonly decimal maxNumOfMine;
        readonly int minNumOfMine;

        public NumOfMineValidator(int maxNumOfMine,int minNumOfMine)
        {
            this.maxNumOfMine = maxNumOfMine / 100m;//= 0.35;
            this.minNumOfMine = minNumOfMine;
            decimal x = maxNumOfMine / 100m;
        }

        /// <summary>
        /// To validate number of Mine format
        /// </summary>
        /// <param name="input"></param>
        /// <param name="numOfMine"></param>
        /// <returns>true if input is numeric; otherwise is false</returns>
        public bool ValidateNumOfMine(string input, ref int numOfMine, out string errorMessage)
        {
            try
            {
                if (int.TryParse(input, out numOfMine))
                {
                    errorMessage = string.Empty;
                    return true;
                }
                errorMessage = Constants.GeneralMessage;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To validate number of Mine format
        /// </summary>
        /// <param name="NumOfMine"></param>
        /// <param name="errorMessage"></param>
        ///  <returns>true NumOfMine is greater than 0; otherwise is false</returns>
        public bool ValidateNumOfMine(int NumOfMine, int totalCells, out string errorMessage)
        {
            try
            {
                if (NumOfMine <= minNumOfMine)
                {
                    errorMessage = Constants.MinNumOfMineErrorMessage;
                    return false;
                }
                if (!ValidateNumOfMine(NumOfMine, totalCells))
                {
                    errorMessage = string.Format(Constants.MaxNumOfMineErrorMessage,( maxNumOfMine * 100).ToString("G29"));
                    return false;
                }
                errorMessage = string.Empty;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// To validate number of Mine against total grid cells
        /// </summary>
        /// <param name="NumOfMine"></param>
        /// <param name="totalSquares"></param>
        /// <returns></returns>
        private bool ValidateNumOfMine(int NumOfMine, int totalCells)
        {
            try
            {
                decimal _maxNumOfMine = Math.Truncate(maxNumOfMine * totalCells);

                if (NumOfMine <= _maxNumOfMine)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
