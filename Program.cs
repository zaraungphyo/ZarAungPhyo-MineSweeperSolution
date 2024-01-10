using System;
using MineSweeperSolution.Service;
namespace MineSweeperSolution
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                GamePlayer.Instance.StartGame();
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

