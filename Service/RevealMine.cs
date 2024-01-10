using MineSweeperSolution.Model;
using System;
using System.Collections.Generic;

namespace MineSweeperSolution.Service
{
    public delegate void DelegateGridGenerator(IList<SquareLocation> locations);
    public class RevealMine: MessagePrompter, IRevealMine
    {
        IMineLocator mineLocator;
        public RevealMine(IMineLocator mineLocator)
        {
            this.mineLocator = mineLocator;
        }

        protected override void PromptMessage(string message) {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Display Number of mines contain in selected square.
        /// </summary>
        /// <returns>true if selected square contain mine; otherwise is false</returns>
        public bool RevealMineField()
        {
            try
            {
                int rowIndex = mineLocator.GetMineField.SelectedSquareLocation.RowIndex;
                int colIndex = mineLocator.GetMineField.SelectedSquareLocation.ColumnIndex;
                bool UncoverSquareHasMine = mineLocator.HasMine(rowIndex, colIndex);             
                return UncoverSquareHasMine;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// uncover all adjacent squares until it reaches squares that do have adjacent mines
        /// </summary>
        /// <param name="UpdateAdjacentSquare"></param>
        public void RevealAdjacentMine(DelegateGridGenerator UpdateAdjacentSquare)
        {
            try
            {
                IAdjacentMineCalculator adjacentMineCalculator = new AdjacentMineCalculator(mineLocator);
                adjacentMineCalculator.FindAdjacentMine(UpdateAdjacentSquare);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
