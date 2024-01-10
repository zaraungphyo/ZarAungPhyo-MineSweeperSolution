namespace MineSweeperSolution.Service
{
    public class GridSizeValidator : IGridSizeValidator
    {
        readonly int maxGridSize;
        readonly int minGridSize;
        public GridSizeValidator( int minGridSize,int maxGridSize)
        {
            this.minGridSize = minGridSize;
            this.maxGridSize = maxGridSize;
        }

        public bool ValidateGridSize(string input, ref int gridSize, out string errorMessage)
        {
            if (int.TryParse(input, out gridSize))
            {
                errorMessage = string.Empty;
                return true;
            }
            errorMessage = Constants.GeneralMessage;
            return false;
        }

        public bool ValidateGridSize(int gridSize, out string errorMessage)
        {
            try
            {
                if (gridSize > maxGridSize)
                {
                    errorMessage = string.Format(Constants.MaxGridSizeErrorMessage, maxGridSize);
                    return false;
                }
                if (gridSize < minGridSize)
                {
                    errorMessage = string.Format(Constants.MinGridSizeErrorMessage, minGridSize); //$"Minimum size of grid is {minGridSize}.";
                    return false;
                }
                errorMessage = string.Empty;
                return true;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
