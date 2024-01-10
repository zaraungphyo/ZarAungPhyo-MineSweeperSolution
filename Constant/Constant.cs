namespace MineSweeperSolution
{
    public struct  Constants
    {
        public const string MaxGridSizeParam = "maxGridSize";
        public const string MinGridSizeParam = "minGridSize";
        public const string MinNumOfMinesParam = "minNumOfMines";
        public const string MaxNumOfMinesParam = "maxNumOfMines";

        public const string MineValue = "m";
        public const string MaskCellValue = "-";
        public const string SuccessMessage = "Congratulations, you have won the game!";
        public const string FailMessage = "Oh no, you detonated a mine! Game over.";

        public const string RetryMessage = "Press any key to play again or esc key to exit...";

        public const string RequestForGridSizeMessage = "Enter the size of the grid (e.g. 4 for a 4x4 grid): ";
        public const string RequestForNumOfMineMessage = "Enter the number of mines to place on the grid (maximum is 35% of the total squares): ";

        public const string MineFieldTitle = "Here is your minefield:";
        public const string UpdatedMineFieldTitle = "Here is your updated minefield:";

        public const string GeneralMessage = "Incorect input.";
        public const string MaxGridSizeErrorMessage = "Maximum size of grid is {0}.";
        public const string MinGridSizeErrorMessage = "Minimum size of grid is {0}.";

        public const string MinNumOfMineErrorMessage = "There must be at least 1 mine.";
        public const string MaxNumOfMineErrorMessage = "Maximum number is {0}% of total sqaures.";

        public const string RequestMineUncoveredMessage = "Select a square to reveal (e.g. A1): ";
        public const string MineUncoveredErrorMessage = "This square contains {0} adjacent mines. ";
    }
}
