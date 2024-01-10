using System;
using System.Text.RegularExpressions;

namespace MineSweeperSolution.Service
{
    public class MineUncoveredValidator : IMineUncoveredValidator
    {
        string formatCode = @"^(?=.{2,2}$)([A-Z][0-9])";

        /// <summary>
        /// Validate user input. Accepted input such as A1,B2 etc.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool ValidateMineUncovered(string input, out string errorMessage)
        {
            try
            {
                Regex r = new Regex(formatCode);
                if (string.IsNullOrWhiteSpace(input) || !r.IsMatch(input))
                {
                    errorMessage = Constants.GeneralMessage;
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
        /// Validate column is in range against Grid Size
        /// </summary>
        /// <param name="colHeader"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool ValidateMineUncovered(int colHeader, out string errorMessage)
        {
            try
            {
                if (!Mapper.ColMapper.ContainsKey(colHeader))
                {
                    errorMessage = Constants.GeneralMessage;
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
        /// Validate row is in range against Grid Size
        /// </summary>
        /// <param name="rowHeader"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool ValidateMineUncovered(char rowHeader, out string errorMessage)
        {
            try
            {
                if (!Mapper.RowMapper.ContainsKey(rowHeader))
                {
                    errorMessage = Constants.GeneralMessage;
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
    }
}
