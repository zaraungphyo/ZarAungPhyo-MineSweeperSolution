using System;

namespace MineSweeperSolution.Service
{
    public class AcquireGridSize : MessagePrompter, IAcquireGridSize
    {
        IGridSizeValidator validator;

        public AcquireGridSize(IGridSizeValidator validator)
        {
            this.validator = validator;
        }

        protected override void PromptMessage(string message)
        {
            Console.Write(message);
        }
       
        public int GetGridSize()
        {
            int gridSize = -1;
            try
            {
                bool isInputValid = false;
                do
                {
                    PromptMessage(Constants.RequestForGridSizeMessage);
                    string strGridSize = Console.ReadLine();
                    isInputValid = GridSizeInput(strGridSize, ref gridSize);
                } while (!isInputValid);
            }
            catch (Exception)
            {
                throw;
            }
            return gridSize;
        }

        public bool GridSizeInput(string strGridSize, ref int gridSize) {
            bool isInputValid = false;
            string errorMessage;

            if (!validator.ValidateGridSize(strGridSize, ref gridSize, out errorMessage))
            {
                isInputValid = false;
                gridSize = -1;
                PromptMessage(errorMessage);
                Console.WriteLine();
            }
            else
            {
                if (!validator.ValidateGridSize(gridSize, out errorMessage))
                {
                    isInputValid = false;
                    gridSize = -1;
                    PromptMessage(errorMessage);
                    Console.WriteLine();
                }
                else
                {
                    isInputValid = true;
                }
            }
            return isInputValid;
        }
    }
}
