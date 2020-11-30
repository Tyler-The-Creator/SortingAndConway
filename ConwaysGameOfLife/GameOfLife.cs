using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConwaysGameOfLife
{
    public class GameOfLife
    {
        public int[,] Board { get; set; }
        public int BoardSize { get; set; }
        public int NumberOfGenerations { get; set; }

        public GameOfLife(int boardSize, int numberOfGenerations)
        {
            BoardSize = boardSize;
            NumberOfGenerations = numberOfGenerations;
            Board = new int[BoardSize, BoardSize];
        }

        // Randomise the initial state of the board
        public void RandomiseCellsToInitialiseBoard()
        {
            var random = new Random();
            for (var x = 1; x < BoardSize - 1; x++)
            {
                for (var y = 1; y < BoardSize - 1; y++)
                {
                    Board[x, y] = random.Next(2);
                }
            }
        }

        // Prints out the board for the user to see each generation
        public void PrintBoard()
        {
            Console.Write("|");
            for (var i = 0; i < BoardSize * 4 - 1; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("|");
            for (var x = 0; x < BoardSize; x++)
            {
                Console.Write("| ");
                for (var y = 0; y < BoardSize; y++)
                {
                    Console.Write($"{Board[x, y]} | ");
                }
                Console.WriteLine();
                Console.Write("|");
                for (var i = 0; i < BoardSize * 4 - 1; i++)
                {
                    Console.Write("-");
                }
                Console.Write("|");
                Console.WriteLine();
            }
        }

        // Get the number of living neighbouring cells of the cell we are currently on
        public int GetLivingNeighbourCellCount(int x, int y)
        {
            var neighbours = new List<int>
            {
                Board[x, y - 1],
                Board[x + 1, y - 1],
                Board[x + 1, y],
                Board[x + 1, y + 1],
                Board[x, y + 1],
                Board[x - 1, y + 1],
                Board[x - 1, y],
                Board[x - 1, y - 1]
            };
            return neighbours.Count(n => n == 1);
        }

        // A tick is the term used to indicate each generation of the simulation
        public void Tick()
        {
            // Start loop at (1,1) and end at (length-1, length-1) so that we ignore the padded cells
            for (var x = 1; x < BoardSize - 1; x++)
            {
                for (var y = 1; y < BoardSize - 1; y++)
                {
                    var currentCellValue = Board[x, y];
                    var numberOfLiveNeighbours = GetLivingNeighbourCellCount(x, y);
                    AlterCellStateAccordingToSimulationRules(x, y, currentCellValue, numberOfLiveNeighbours);
                }
            }
        }

        // Run the simulation rules on the current cell
        public void AlterCellStateAccordingToSimulationRules(int x, int y, int currentCellValue, int numberOfLiveNeighbours)
        {
            // Rule: Any live cell with less than two live neighbours dies due to underpopulation.
            if (currentCellValue == 1 && numberOfLiveNeighbours < 2)
            {
                Board[x, y] = 0;
            }
            // Rule: Any live cell with more than three live neighbours dies due to overpopulation.
            else if (currentCellValue == 1 && numberOfLiveNeighbours > 3)
            {
                Board[x, y] = 0;
            }
            // Rule: Any dead cell with exactly three live neighbours becomes a live cell due to reproduction.
            else if (currentCellValue == 0 && numberOfLiveNeighbours == 3)
            {
                Board[x, y] = 1;
            }
            // Rule: Any live cell with two or three live neighbours lives on to the next generation. (if live it lives, if dead it stays dead)
            else if (currentCellValue == 1 && (numberOfLiveNeighbours == 2 || numberOfLiveNeighbours == 3))
            {
                Board[x, y] = 1;
            }
            // Rule: All other cells remain the same
            else
            {
                Board[x, y] = currentCellValue;
            }
        }

        // Counts the number of living cells currently on the board
        public int CountLivingCellsOnBoard()
        {
            var count = 0;
            for (var x = 1; x < BoardSize - 1; x++)
            {
                for (var y = 1; y < BoardSize - 1; y++)
                {
                    count += Board[x, y];
                }
            }

            return count;
        }

        public static void Main()
        {
            int boardSize, numberOfGenerations;
            Console.WriteLine("!!! Welcome to Conway's Game of Life !!!\n");
            
            // Get the board size
            Console.WriteLine("Please enter a number for the boardsize. (It will determine the board size, e.g. 5 generates a 5x5 board):");
            var input = Console.ReadLine();
            while (!int.TryParse(input, out boardSize))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                input = Console.ReadLine();
            }

            // Get the number of generations
            Console.WriteLine("Please enter the number of generations for the simulation:");
            input = Console.ReadLine();
            while (!int.TryParse(input, out numberOfGenerations))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                input = Console.ReadLine();
            }

            // Pad the board with dead cells so that we don't try iterate out of bounds
            var paddedBoardSize = boardSize + 2;

            // Initialise the board and set the initial state
            var gameOfLife = new GameOfLife(paddedBoardSize, numberOfGenerations);
            gameOfLife.RandomiseCellsToInitialiseBoard();

            // Display the initial state of the board
            Console.WriteLine("\nInitial State:");
            gameOfLife.PrintBoard();

            // Run and display each tick (generation) of the simulation
            var generation = 0;
            while (generation < gameOfLife.NumberOfGenerations)
            {
                // The simulation will end earlier than the specified number of generations provided if all the cells are dead.
                if (gameOfLife.CountLivingCellsOnBoard() == 0)
                {
                    Console.WriteLine("The simulation has ended because all cells have died.");
                    break;
                }
                else
                {
                    gameOfLife.Tick();
                    Console.WriteLine($"\nGeneration: {generation + 1}");
                    gameOfLife.PrintBoard();
                    generation++;
                    Thread.Sleep(1000);
                }
            }

            Console.WriteLine("\nPress any key to quit.");
            Console.ReadKey();
        }
    }
}
