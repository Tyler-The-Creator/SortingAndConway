using ConwaysGameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class ConwaysGameOfLifeTests
    {
        private readonly int[,] BoardA = new int[4, 4]
        {
            {0, 0, 0, 0},
            {0, 1, 1, 0},
            {0, 0, 1, 0},
            {0, 0, 0, 0}
        };

        private readonly int[,] BoardB = new int[4, 4]
        {
            {0, 0, 0, 0},
            {0, 0, 1, 0},
            {0, 0, 1, 0},
            {0, 0, 0, 0}
        };

        [TestMethod]
        public void Count_LivingCells_GetNumberOfLivingNeighbouringCells()
        {
            var gameOfLife = new GameOfLife(4, 2);
            gameOfLife.Board = BoardA;
            for (var x = 1; x < gameOfLife.BoardSize - 1; x++)
            {
                for (var y = 1; y < gameOfLife.BoardSize - 1; y++)
                {
                    var numberOfLiveNeighbours = gameOfLife.GetLivingNeighbourCellCount(x, y);
                    if (x == 1 && y == 1)
                    {
                        Assert.AreEqual(2, numberOfLiveNeighbours);
                    }
                    else if (x == 1 && y == 2)
                    {
                        Assert.AreEqual(2, numberOfLiveNeighbours);
                    }
                    else if (x == 2 && y == 1)
                    {
                        Assert.AreEqual(3, numberOfLiveNeighbours);
                    }
                    else if (x == 2 && y == 3)
                    {
                        Assert.AreEqual(2, numberOfLiveNeighbours);
                    }
                }
            }
        }

        [TestMethod]
        public void Alter_CellStateChangedToLiving_AlterCellStateToLiving()
        {
            var gameOfLife = new GameOfLife(4, 2);
            gameOfLife.Board = BoardA;
            int x = 1, y = 2;
            var currentCellValue = gameOfLife.Board[x, y];
            var numberOfLiveNeighbours = gameOfLife.GetLivingNeighbourCellCount(x, y);
            gameOfLife.AlterCellStateAccordingToSimulationRules(x, y, currentCellValue, numberOfLiveNeighbours);
            Assert.AreEqual(1, BoardA[x, y]);
        }

        [TestMethod]
        public void Alter_CellStateChangedToDead_AlterCellStateToDead()
        {
            var gameOfLife = new GameOfLife(4, 2);
            gameOfLife.Board = BoardB;
            int x = 2, y = 1;
            var currentCellValue = gameOfLife.Board[x, y];
            var numberOfLiveNeighbours = gameOfLife.GetLivingNeighbourCellCount(x, y);
            gameOfLife.AlterCellStateAccordingToSimulationRules(x, y, currentCellValue, numberOfLiveNeighbours);
            Assert.AreEqual(0, BoardB[x, y]);
        }

        [TestMethod]
        public void Alter_CellStateUnchanged_CellStateNotAltered()
        {
            var gameOfLife = new GameOfLife(4, 2);
            gameOfLife.Board = BoardA;
            int x = 1, y = 1;
            var currentCellValue = gameOfLife.Board[x, y];
            var numberOfLiveNeighbours = gameOfLife.GetLivingNeighbourCellCount(x, y);
            gameOfLife.AlterCellStateAccordingToSimulationRules(x, y, currentCellValue, numberOfLiveNeighbours);
            Assert.AreEqual(1, BoardA[x, y]);
        }

        [TestMethod]
        public void Tick_RunGeneration_SuccessfulTick()
        {
            var gameOfLife = new GameOfLife(4, 2);
            gameOfLife.Board = BoardA;
            gameOfLife.Tick();
            Assert.AreEqual(1, BoardA[1, 1]);
            Assert.AreEqual(1, BoardA[1, 2]);
            Assert.AreEqual(1, BoardA[2, 1]);
            Assert.AreEqual(1, BoardA[2, 2]);
        }

        [TestMethod]
        public void Count_NumberOfLivingCells_CountBeforeTick()
        {
            var gameOfLife = new GameOfLife(4, 2);
            gameOfLife.Board = BoardA;
            var numberOfLivingCells = gameOfLife.CountLivingCellsOnBoard();
            Assert.AreEqual(3, numberOfLivingCells);
        }

        [TestMethod]
        public void Count_NumberOfLivingCells_CountAfterTick()
        {
            var gameOfLife = new GameOfLife(4, 2);
            gameOfLife.Board = BoardA;
            gameOfLife.Tick();
            var numberOfLivingCells = gameOfLife.CountLivingCellsOnBoard();
            Assert.AreEqual(4, numberOfLivingCells);
        }
    }
}
