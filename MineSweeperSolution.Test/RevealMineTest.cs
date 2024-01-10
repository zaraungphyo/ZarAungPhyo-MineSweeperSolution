using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeperSolution.Model;
using MineSweeperSolution.Service;
using System;

namespace MineSweeperSolution.Test
{
    [TestClass]
    public class RevealMineTest
    {
        private readonly GridGenerator gridGenerator;

        public RevealMineTest()
        {
            gridGenerator = new GridGenerator(4, 3);
            IMineLocator mineLocator = gridGenerator.MineFieldLocator;
            int[,] mineLocations = new int[4, 2];
            // A2
            mineLocations[0, 0] = 0;
            mineLocations[0, 1] = 1;

            // B2
            mineLocations[1, 0] = 1;
            mineLocations[1, 1] = 1;

            // C1
            mineLocations[2, 0] = 2;
            mineLocations[2, 1] = 0;

            mineLocator.AllocateMines(mineLocations);
        }

        [TestMethod]
        [DataRow('D', 4)]  // Reveal A3A4B3B4C2C3C4D2D3D4
        public void RevealAdjacentMine_InputD4_Reveal(char rowIndex, int colIndex)
        {
            SquareLocation selectedSquareLocation = new SquareLocation()
            {
                RowIndex = gridGenerator.GetActualRowIndex(rowIndex),
                ColumnIndex = gridGenerator.GetActualColumnIndex(colIndex)
            };
            gridGenerator.UpdateMine(selectedSquareLocation);
            IRevealMine revealMine = new RevealMine(gridGenerator.MineFieldLocator);
            revealMine.RevealAdjacentMine(true, gridGenerator.GenerateGrid);
            string[,] maskedGrid = gridGenerator.MaskedGridSize;

            //if selected square is D4, [A3, A4, B3, B4, C2, C3, C4, D2, D3, D4] squares are revealed. Otherwise, hidden.
            // Check A Row
            Assert.AreEqual("_", maskedGrid[0, 0]); 
            Assert.AreEqual("_", maskedGrid[0, 1]);
            Assert.AreEqual("2", maskedGrid[0, 2]);
            Assert.AreEqual("0", maskedGrid[0, 3]);

            // Check B Row
            Assert.AreEqual("_", maskedGrid[1, 0]);
            Assert.AreEqual("_", maskedGrid[1, 1]);
            Assert.AreEqual("2", maskedGrid[1, 2]);
            Assert.AreEqual("0", maskedGrid[1, 3]);

            // Check C Row
            Assert.AreEqual("_", maskedGrid[2, 0]);
            Assert.AreEqual("2", maskedGrid[2, 1]);
            Assert.AreEqual("1", maskedGrid[2, 2]);
            Assert.AreEqual("0", maskedGrid[2, 3]);

            // Check D Row
            Assert.AreEqual("_", maskedGrid[3, 0]);
            Assert.AreEqual("1", maskedGrid[3, 1]);
            Assert.AreEqual("0", maskedGrid[3, 2]);
            Assert.AreEqual("0", maskedGrid[3, 3]);
        }
    }
}
