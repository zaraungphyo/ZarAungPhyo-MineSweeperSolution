using System;

namespace MineSweeperSolution.Service
{
    public class AcquireNumOfMines : MessagePrompter, IAcquireNumOfMines
    {
        INumOfMineValidator validator;

        public AcquireNumOfMines(INumOfMineValidator validator)
        {
            this.validator = validator;
        }

        protected override void PromptMessage(string message)
        {
            Console.Write(message);
        }

        public int GetNumOfMines(int GridCells)
        {
            int NumOfMine = -1;
            try
            {
                bool isInputValid = false;
                do
                {
                    PromptMessage(Constants.RequestForNumOfMineMessage);
                    string strNumOfMine = Console.ReadLine();
                    isInputValid = NumOfMineInput(strNumOfMine, GridCells, ref NumOfMine);

                } while (!isInputValid);
            }
            catch (Exception)
            {
                throw;
            }
            return NumOfMine;
        }

        public bool NumOfMineInput(string strNumOfMine,int GridCells, ref int NumOfMine)
        {
            bool isInputValid = false;
            string errorMessage;
            if (!validator.ValidateNumOfMine(strNumOfMine, ref NumOfMine, out errorMessage))
            {
                isInputValid = false;
                NumOfMine = -1;
                PromptMessage(errorMessage);
                Console.WriteLine();
            }
            else
            {
                if (!validator.ValidateNumOfMine(NumOfMine, GridCells, out errorMessage))
                {
                    isInputValid = false;
                    NumOfMine = -1;
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
