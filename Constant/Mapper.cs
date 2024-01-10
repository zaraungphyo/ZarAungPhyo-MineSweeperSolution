using System;
using System.Collections.Generic;
using MineSweeperSolution.Model;

namespace MineSweeperSolution
{
    public static class Mapper
    {
        public static IDictionary<char, int> RowMapper = new Dictionary<char, int>();
        public static IDictionary<int, int> ColMapper = new Dictionary<int, int>();
        public static IList<SquareLocation> MineLocations = new List<SquareLocation>();
    }
}
