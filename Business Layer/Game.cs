using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer
{
    class Game
    {

            /// <summary>
            /// playable game method, asks user for input (size and difficulty) 
            /// </summary>
            /// <param name="args"></param>
            static void Main(string[] args)
            {
                Console.WriteLine("Between 1-12, pick the size of the square board you would like : ");
                //Reads what person puts in console
                int size = int.Parse(Console.ReadLine());
                Board board = new Board(size);
                Console.WriteLine("Between 1-50, how difficult would you like the game to be (recommended 15): ");
                //reads difficulty
                board.Difficulty = int.Parse(Console.ReadLine());

                board.setUpLiveNeighbors(board.Difficulty);
                board.calculateLiveNeighbors();


                //if you want, remove comment // and this will give you the unrevieled board
                printBoard(board);
                Console.ReadKey();



                //local variables for the game over loop
                bool GameOver = false;
                int RowNumber;
                int ColumnNumber;

                while (!GameOver)
                {
                    printBoardDuringGame(board);
                    Console.WriteLine("Please enter a column number: ");
                    //user input
                    ColumnNumber = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please enter a row number: ");
                    // user input
                    RowNumber = int.Parse(Console.ReadLine());


                    //revealing the cell
                    board.grid[RowNumber - 1, ColumnNumber - 1].Visited = true;

                    board.FloodFill(RowNumber - 1, ColumnNumber - 1);


                    GameOver = IsGameOver(board, RowNumber, ColumnNumber);

                }
                //final board(key) when the game is over
                printBoard(board);
                Console.ReadKey();
            }

            /// <summary>
            /// Checks and sees if the game is over, either the user hits a bomb, or their are no more squares to reveal that arent bombs
            /// </summary>
            /// <param name="board"></param>
            /// <param name="row"></param>
            /// <param name="col"></param>
            /// <returns></returns>
            static bool IsGameOver(Board board, int row, int col)
            {
                //return breaks the loop 
                if (board.grid[row - 1, col - 1].Visited == true && board.grid[row - 1, col - 1].Live == true)
                {
                    Console.WriteLine("You exploded");
                    return true;
                }
                for (int i = 0; i < board.Size; i++)
                {
                    for (int j = 0; j < board.Size; j++)
                    {
                        if (board.grid[i, j].Visited == false && board.grid[i, j].Live == false)
                        {
                            return false;
                        }
                    }
                }
                //once we have no more visited cells that are not bombs, we win the game
                Console.WriteLine("Congrats you won the game");
                return true;


            }

            /// <summary>
            /// this print board method is the key, this is a revealed board
            /// </summary>
            /// <param name="board"></param>
            static void printBoard(Board board)
            {
                //sets up the top numbers
                for (int i = 0; i < board.Size; i++)
                {
                    Console.Write("  " + (i + 1) + "  ");
                }
                Console.WriteLine();
                for (int i = 0; i < board.Size; i++)
                {
                    Console.WriteLine();

                    //middle of the board with number of live neighbors inside

                    for (int j = 0; j < board.Size; j++)
                    {
                        Console.Write("| " + board.grid[i, j].LiveNeighbors + " |");
                    }

                    //column numbers
                    Console.Write(i + 1);

                    Console.WriteLine();

                }
            }
            /// <summary>
            /// this is the game board that is playable, squares are hidden for the user to reveal, squares that have no Live neighbors are marked with "~"
            /// </summary>
            /// <param name="board"></param>
            static void printBoardDuringGame(Board board)
            {
                //top numbers
                for (int i = 0; i < board.Size; i++)
                {
                    Console.Write("  " + (i + 1) + "  ");
                }
                Console.WriteLine();

                for (int i = 0; i < board.Size; i++)
                {

                    for (int j = 0; j < board.Size; j++)
                    {
                        //fills the board with ? marks if the cell is not visited
                        if (board.grid[i, j].Visited == false)
                        {
                            Console.Write("| ? |");
                        }

                        if (board.grid[i, j].LiveNeighbors == 0 && board.grid[i, j].Live == false && board.grid[i, j].Visited == true)
                        {
                            Console.Write("| ~ |");
                        }

                        //mark the board with * as a bomb if it is visited and if it is a bomb
                        if (board.grid[i, j].Visited == true && board.grid[i, j].Live == true)
                        {
                            Console.Write("| * |");
                        }

                        //if the cell is visited, but no bomb, we get the live neighbors around the cell 
                        if (board.grid[i, j].Visited == true && board.grid[i, j].Live == false && board.grid[i, j].LiveNeighbors != 0)
                        {

                            Console.Write("| " + board.grid[i, j].LiveNeighbors + " |");
                        }
                    }




                    //row numbers
                    Console.Write(i + 1);
                    Console.WriteLine();

                }
                Console.WriteLine();
            }
        }
}
