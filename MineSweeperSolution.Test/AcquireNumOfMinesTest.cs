using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeperSolution.Service;
using MineSweeperSolution.Utility;

namespace MineSweeperSolution.Test
{
    [TestClass]
    public class AcquireNumOfMinesTest
    {
        private readonly INumOfMineValidator validator;
        private readonly IAcquireNumOfMines numOfMines;
        int maxNumOfMines = ConfigHelper.ConfigValue(Constants.MaxNumOfMinesParam, 35);
        int minNumOfMines = ConfigHelper.ConfigValue(Constants.MinNumOfMinesParam, 0);
        public AcquireNumOfMinesTest()
        {
            validator = new NumOfMineValidator(maxNumOfMines, minNumOfMines);
            numOfMines = new AcquireNumOfMines(validator);
        }

        [TestMethod]
        [DataRow("AA", 4)]
        [DataRow("A1", 4)]
        [DataRow("4A", 4)]
        public void NumOfMineInput_InputIsString_ReturnFalseOutputErrorMessage(string strNumOfMine, int GridCells)
        {
            int result = -1;
            bool returnVal = numOfMines.NumOfMineInput(strNumOfMine, GridCells, ref result);

            Assert.IsFalse(returnVal);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        [DataRow("-1", 4)]
        [DataRow("0", 4)]
        public void NumOfMineInput_InputIsLessThanMinValue_ReturnFalseOutputErrorMessage(string strNumOfMine, int GridCells)
        {
            int result = -1;
            bool returnVal = numOfMines.NumOfMineInput(strNumOfMine, GridCells, ref result);

            Assert.IsFalse(returnVal);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        [DataRow("6", 16)]
        [DataRow("10", 16)]
        [DataRow("16", 16)]
        [DataRow("100", 16)]
        public void NumOfMineInput_InputIsGreaterThanMin35Percent_ReturnFalseOutputErrorMessage(string strNumOfMine, int GridCells)
        {
            int result = -1;
            bool returnVal = numOfMines.NumOfMineInput(strNumOfMine, GridCells, ref result);

            Assert.IsFalse(returnVal);
            Assert.AreEqual(-1, result);
        }
    }
}
