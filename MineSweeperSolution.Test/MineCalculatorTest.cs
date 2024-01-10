using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeperSolution.Model;
using MineSweeperSolution.Service;
using System;

namespace MineSweeperSolution.Test
{
    [TestClass]
    public class MineCalculatorTest
    {
        private IMineLocator mineLocator;
        
        // Game 1 -> Test 4x4 Grids and 3 mines
        [TestMethod]
        [DataRow(4, 3)]
        public void AllocateMines_4Grids_3Mines(int size, int numOfMine)
        {
            GridGenerator gridGenerator = new GridGenerator(size, numOfMine);
            mineLocator = gridGenerator.MineFieldLocator;
            int[,] mineLocations = new int[3, 2];
            // A3
            mineLocations[0, 0] = 0;
            mineLocations[0, 1] = 2;

            // B1
            mineLocations[1, 0] = 1; // row B
            mineLocations[1, 1] = 0; // column 1

            // C2
            mineLocations[2, 0] = 2;
            mineLocations[2, 1] = 1;

            mineLocator.AllocateMines(mineLocations);
            var field = mineLocator.GetMineField;

            // Number of Rows
            Assert.AreEqual(4, field.NumOfRows);

            // Number of Columns
            Assert.AreEqual(4, field.NumOfColumns);

            // Number of Mines
            Assert.AreEqual(3, field.NumOfMine);

            // Test Grids including Mines and adjacent mine counts
            // Check A Row
            Assert.AreEqual("1", field.Grid[0, 0]); //if selected square output is 1, passed otherwise failed.
            Assert.AreEqual("2", field.Grid[0, 1]);
            Assert.AreEqual("m", field.Grid[0, 2]);
            Assert.AreEqual("1", field.Grid[0, 3]);

            // Check B Row
            Assert.AreEqual("m", field.Grid[1, 0]);
            Assert.AreEqual("3", field.Grid[1, 1]);
            Assert.AreEqual("2", field.Grid[1, 2]);
            Assert.AreEqual("1", field.Grid[1, 3]);

            // Check C Row
            Assert.AreEqual("2", field.Grid[2, 0]);
            Assert.AreEqual("m", field.Grid[2, 1]);
            Assert.AreEqual("1", field.Grid[2, 2]);
            Assert.AreEqual("0", field.Grid[2, 3]);

            // Check D Row
            Assert.AreEqual("1", field.Grid[3, 0]);
            Assert.AreEqual("1", field.Grid[3, 1]);
            Assert.AreEqual("1", field.Grid[3, 2]);
            Assert.AreEqual("0", field.Grid[3, 3]);
        }

        // Game 2 -> Test 5x5 Grids and 4 mines
        [TestMethod]
        [DataRow(5, 4)]
        public void AllocateMines_5Grids_4Mines(int size, int numOfMine)
        {
            GridGenerator gridGenerator = new GridGenerator(size, numOfMine);
            mineLocator = gridGenerator.MineFieldLocator;
            int[,] mineLocations = new int[4, 2];
            // A5
            mineLocations[0, 0] = 0;
            mineLocations[0, 1] = 4;

            // B1
            mineLocations[1, 0] = 1;
            mineLocations[1, 1] = 0;

            // D1
            mineLocations[2, 0] = 3;
            mineLocations[2, 1] = 0;

            // D2
            mineLocations[3, 0] = 3;
            mineLocations[3, 1] = 1;

            mineLocator.AllocateMines(mineLocations);
            var field = mineLocator.GetMineField;

            // Number of Rows
            Assert.AreEqual(5, field.NumOfRows);

            // Number of Columns
            Assert.AreEqual(5, field.NumOfColumns);

            // Number of Mines
            Assert.AreEqual(4, field.NumOfMine);

            // Test Grids including Mines and adjacent mine counts
            // Check A Row
            Assert.AreEqual("1", field.Grid[0, 0]); //if selected square output is 1, passed otherwise failed.
            Assert.AreEqual("1", field.Grid[0, 1]);
            Assert.AreEqual("0", field.Grid[0, 2]);
            Assert.AreEqual("1", field.Grid[0, 3]);
            Assert.AreEqual("m", field.Grid[0, 4]);

            // Check B Row
            Assert.AreEqual("m", field.Grid[1, 0]);
            Assert.AreEqual("1", field.Grid[1, 1]);
            Assert.AreEqual("0", field.Grid[1, 2]);
            Assert.AreEqual("1", field.Grid[1, 3]);
            Assert.AreEqual("1", field.Grid[1, 4]);

            // Check C Row
            Assert.AreEqual("3", field.Grid[2, 0]);
            Assert.AreEqual("3", field.Grid[2, 1]);
            Assert.AreEqual("1", field.Grid[2, 2]);
            Assert.AreEqual("0", field.Grid[2, 3]);
            Assert.AreEqual("0", field.Grid[2, 4]);

            // Check D Row
            Assert.AreEqual("m", field.Grid[3, 0]);
            Assert.AreEqual("m", field.Grid[3, 1]);
            Assert.AreEqual("1", field.Grid[3, 2]);
            Assert.AreEqual("0", field.Grid[3, 3]);
            Assert.AreEqual("0", field.Grid[3, 4]);

            // Check E Row
            Assert.AreEqual("2", field.Grid[4, 0]);
            Assert.AreEqual("2", field.Grid[4, 1]);
            Assert.AreEqual("1", field.Grid[4, 2]);
            Assert.AreEqual("0", field.Grid[4, 3]);
            Assert.AreEqual("0", field.Grid[4, 4]);
        }
    }
}
