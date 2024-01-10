using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeperSolution.Service;
using MineSweeperSolution.Utility;
using System;

namespace MineSweeperSolution.Test
{
    [TestClass]
    public class AcquireGridSizeTest
    {
        private readonly IGridSizeValidator validator;
        private readonly IAcquireGridSize gridSize;
        private readonly int MinGridSize = ConfigHelper.ConfigValue(Constants.MinGridSizeParam, 2);
        private readonly int MaxGridSize = ConfigHelper.ConfigValue(Constants.MaxGridSizeParam, 10);
        public AcquireGridSizeTest()
        {
            validator = new GridSizeValidator(MinGridSize, MaxGridSize);
            gridSize = new AcquireGridSize(validator);
        }

        [TestMethod]
        [DataRow("AA")]
        [DataRow("A1")]
        [DataRow("4A")]
        public void GetGridSize_InputIsString_ReturnFalseOutputErrorMessage(string strGridSize)
        {
            int result = -1;
            bool returnVal = gridSize.GridSizeInput(strGridSize, ref result);

            Assert.IsFalse(returnVal);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        [DataRow("-1")]
        [DataRow("1")]
        [DataRow("0")]
        public void GetGridSize_InputIsLessThanMinValue_ReturnFalseOutputErrorMessage(string strGridSize)
        {        
            int result = -1;
            bool returnVal = gridSize.GridSizeInput(strGridSize, ref result);

            Assert.IsFalse(returnVal);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        [DataRow("11")]
        [DataRow("12")]
        [DataRow("100")]
        public void GetGridSize_InputIsGreaterThanMaxValue_ReturnFalseOutputErrorMessage(string strGridSize)
        {
            int result = -1;
            bool returnVal = gridSize.GridSizeInput(strGridSize, ref result);

            Assert.IsFalse(returnVal);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        [DataRow("2")]
        [DataRow("8")]
        [DataRow("10")]
        public void GetGridSize_InputIsValid_ReturnTrue(string strGridSize)
        {
            int result = -1;
            bool returnVal = gridSize.GridSizeInput(strGridSize, ref result);

            Assert.IsTrue(returnVal);
            Assert.AreEqual(Convert.ToInt32(strGridSize), result);
        }
    }
}
