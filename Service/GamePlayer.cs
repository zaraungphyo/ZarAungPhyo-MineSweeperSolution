using MineSweeperSolution.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using MineSweeperSolution.Utility;

namespace MineSweeperSolution.Service
{
    public sealed class GamePlayer : MessagePrompter
    {
        int GridSize = -1;
        int GridCells = -1;
        int NumOfMine = -1;
        IGridGenerator gridGenerator;
        IAcquireMineUncovered acquireMineUncovered;
        private static readonly Lazy<GamePlayer> lazy = new Lazy<GamePlayer>(() => new GamePlayer());
        bool gameController = false;
        bool exitApplication = false;
        object lockObj = new object();
        Thread startControllerThread;
        private GamePlayer()
        { }

        public static GamePlayer Instance { get { return lazy.Value; } }

        public void StartGame()
        {
            startControllerThread = new Thread(new ThreadStart(GamePlayer.Instance.GameController));
            Console.WriteLine("Welcome to Minesweeper!");
            RequestGridSize();
            RequestNumOfMines();
            PopulateGrid();
            RequestToUncoverSquare();
        }
        protected override void PromptMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Prompt User to enter Grid Size
        /// </summary>
        void RequestGridSize()
        {
            int MinGridSize = ConfigHelper.ConfigValue(Constants.MinGridSizeParam, 2);
            int MaxGridSize = ConfigHelper.ConfigValue(Constants.MaxGridSizeParam, 10);
            //request for user input for grid size
            IGridSizeValidator gridSizeValidator = new GridSizeValidator(MinGridSize, MaxGridSize);
            IAcquireGridSize acquireGridSize = new AcquireGridSize(gridSizeValidator);
            GridSize = acquireGridSize.GetGridSize();
            GridCells = GridSize * GridSize;
        }

        /// <summary>
        /// Prompt User to place number of mine
        /// </summary>
        void RequestNumOfMines()
        {
            int maxNumOfMines = ConfigHelper.ConfigValue(Constants.MaxNumOfMinesParam, 35);
            int minNumOfMines = ConfigHelper.ConfigValue(Constants.MinNumOfMinesParam, 0);
            //request for user input for number of mine
            INumOfMineValidator validator = new NumOfMineValidator(maxNumOfMines, minNumOfMines);
            IAcquireNumOfMines acquireNumOfMines = new AcquireNumOfMines(validator);
            NumOfMine = acquireNumOfMines.GetNumOfMines(GridCells);
        }

        /// <summary>
        /// Populate Grid and Setup Mine
        /// </summary>
        void PopulateGrid()
        {
            //populate grid
            gridGenerator = new GridGenerator(GridSize, NumOfMine);
            gridGenerator.GenerateGrid();
            gridGenerator.SetupMine();
        }

        /// <summary>
        /// Prompt User to uncover square
        /// </summary>
        void RequestToUncoverSquare()
        {
            startControllerThread.Start();
            while (!gameController)
            {
                lock (lockObj)
                {
                    acquireMineUncovered = new AcquireMineUncovered();
                    acquireMineUncovered.GetMineUncovered();
                    RevealUncoveredSquare();
                }
            }
        }

        /// <summary>
        /// Reveal uncovered square
        /// </summary>
        void RevealUncoveredSquare()
        {
            SquareLocation selectedSquareLocation = new SquareLocation()
            {
                RowIndex = gridGenerator.GetActualRowIndex(acquireMineUncovered.RowIndex),
                ColumnIndex = gridGenerator.GetActualColumnIndex(acquireMineUncovered.ColumnIndex)
            };

            gridGenerator.UpdateSelectedSquare(selectedSquareLocation);
            IRevealMine revealMine = new RevealMine(gridGenerator.MineFieldLocator);


            if (!revealMine.RevealMineField())
            {
                revealMine.RevealAdjacentMine( gridGenerator.GenerateGrid);
                PromptMessage(string.Format(Constants.MineUncoveredErrorMessage, gridGenerator.AdjacentMineCounter));
            }
            else
            {
                PrintGameStatus(Constants.FailMessage, selectedSquareLocation);
                gameController = false;
            }
        }

        /// <summary>
        /// Track game progress Win or fail
        /// </summary>
        public void GameController()
        {
            try
            {
                while (!exitApplication)
                {
                    string gameProgress = string.Empty;
                    lock (lockObj)
                    {
                        if (acquireMineUncovered != null && gridGenerator != null)
                        {
                            SquareLocation selectedSquareLocation = new SquareLocation();
                            selectedSquareLocation.RowIndex = gridGenerator.GetActualRowIndex(acquireMineUncovered.RowIndex);
                            selectedSquareLocation.ColumnIndex = gridGenerator.GetActualColumnIndex(acquireMineUncovered.ColumnIndex);

                            if (selectedSquareLocation.RowIndex == -1 || selectedSquareLocation.ColumnIndex == -1)
                            {
                                continue;
                            }

                            gameController = GameTracker.Track(gridGenerator.MaskedGrid, selectedSquareLocation.RowIndex, selectedSquareLocation.ColumnIndex, ref gameProgress);
                            if (gameController)
                            {
                                PrintGameStatus(gameProgress, selectedSquareLocation);
                            }
                        }

                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        void PrintGameStatus(string message, SquareLocation selectedSquareLocation)
        {
            gridGenerator.GenerateGrid(new List<SquareLocation>() { selectedSquareLocation });
            PromptMessage(message);
            Console.Write(Constants.RetryMessage);
            var readKey =   Console.ReadKey(true);
            exitApplication = string.Compare("Escape", readKey.Key.ToString()) == 0;
            if (!exitApplication)
            {
                Console.WriteLine();
                StartGame();
                startControllerThread.Abort();
                startControllerThread = null;
            }
            else
            {
                Environment.Exit(0);
            } 
        }
    }
}
