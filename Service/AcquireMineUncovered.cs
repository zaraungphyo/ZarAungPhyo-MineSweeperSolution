using System;

namespace MineSweeperSolution.Service
{
    public class AcquireMineUncovered : MessagePrompter, IAcquireMineUncovered
    {
        char _RowIndex;
        int _ColumnIndex;
        IMineUncoveredValidator validator = new MineUncoveredValidator();
        protected override void PromptMessage(string message)
        {
            Console.Write(message);
        }

        public char RowIndex
        {
            set
            {
                _RowIndex = value;
            }
            get { return _RowIndex; }
        }
        public int ColumnIndex
        {
            set
            {
                _ColumnIndex = value;
            }
            get { return _ColumnIndex; }
        }

        public void GetMineUncovered()
        {
            try
            {
                bool isInputValid = false;
                do
                {
                    PromptMessage(Constants.RequestMineUncoveredMessage);
                    string strSquareInput = Console.ReadLine();
                    isInputValid = UncoveredMineInput(strSquareInput);
                } while (!isInputValid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UncoveredMineInput(string strSquareInput)
        {
            string message;
            bool isInputValid = false;

            if (!validator.ValidateMineUncovered(strSquareInput, out message))
            {
                PromptMessage(message);
                Console.WriteLine();
            }
            else
            {
                RowIndex = strSquareInput[0];
                ColumnIndex = Convert.ToInt32(strSquareInput[1].ToString());
                if (!validator.ValidateMineUncovered(RowIndex, out message))
                {
                    isInputValid = false;
                    PromptMessage(message);
                    Console.WriteLine();
                }
                else if (!validator.ValidateMineUncovered(ColumnIndex, out message))
                {
                    isInputValid = false;
                    PromptMessage(message);
                    Console.WriteLine();
                }
                else
                    isInputValid = true;
            }

            return isInputValid;
        }
    }
}
