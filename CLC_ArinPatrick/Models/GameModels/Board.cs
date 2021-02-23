using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minesweeper_ArinPatrick.Models
{
    public class Board
    {

            //board properties

            public Cell[,] grid; //multidimentional array
            public int Size { get; set; }
            public int Difficulty { get; set; }

            //board constructor creates grid with cells at each location
            public Board(int size)
            {
                //initializes board
                Size = size;
                grid = new Cell[size, size];

                //forloop populates the board
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        grid[i, j] = new Cell();
                    }
                }
            }

            /// <summary>
            /// randomly sets bombs that are live on the board for live neighbors to look at 
            /// </summary>
            /// <param name="difficulty"></param>
            public void setUpLiveNeighbors(int difficulty)
            {
                Difficulty = difficulty;
                //setting up all the live bombs in the grid
                Random random = new Random();

                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        int ranNum = random.Next(100);
                        if (ranNum <= Difficulty)
                        {
                            //making the bombs live
                            grid[i, j].Live = true;
                        }
                    }
                }
            }

            /// <summary>
            /// each cell checks the cell around is and adds one number to the live neighbors for every bomb it is touching
            /// </summary>
            public void calculateLiveNeighbors()
            {

                //we are going to be incrimenting the neighbors by one if a bomb is found next to it
                for (int row = 0; row < Size; row++)
                {
                    for (int col = 0; col < Size; col++)
                    {
                        if (grid[row, col].Live)
                        {
                            grid[row, col].LiveNeighbors = 9;
                        }
                        else
                        // Size -1 because we do not want it to hit the total size
                        //this checks the neighbors around the cell to find the live bombs
                        //this is challenging because we must stay inside the board, if we start at box one, we cant check outside the board because it doesnt exist.
                        {
                            //down
                            if (row < Size - 1 && grid[row + 1, col].Live)
                            {
                                grid[row, col].LiveNeighbors++;
                            }
                            //up
                            if (row > 0 && grid[row - 1, col].Live)
                            {
                                grid[row, col].LiveNeighbors++;
                            }
                            //right
                            if (col < Size - 1 && grid[row, col + 1].Live)
                            {
                                grid[row, col].LiveNeighbors++;
                            }
                            //left
                            if (col > 0 && grid[row, col - 1].Live)
                            {
                                grid[row, col].LiveNeighbors++;
                            }
                            //downright
                            if (row < Size - 1 && col < Size - 1 && grid[row + 1, col + 1].Live)
                            {
                                grid[row, col].LiveNeighbors++;
                            }
                            //upleft
                            if (row > 0 && col > 0 && grid[row - 1, col - 1].Live)
                            {
                                grid[row, col].LiveNeighbors++;
                            }
                            //up right
                            if (row > 0 && col < Size - 1 && grid[row - 1, col + 1].Live)
                            {
                                grid[row, col].LiveNeighbors++;
                            }
                            //downleft
                            if (row < Size - 1 && col > 0 && grid[row + 1, col - 1].Live)
                            {
                                grid[row, col].LiveNeighbors++;
                            }
                        }
                    }
                }

            }

            // This method and logic was built in unison with kayla, and mack.
            /// <summary>
            /// recursive method that checks for 0 and replaces them with ~
            /// </summary>
            /// <param name="row"></param>
            /// <param name="column"></param>
            public void FloodFill(int row, int column)
            {
                int a = row;
                int b = column;


                if (a < Size - 1 && grid[a, b].LiveNeighbors == 0 && grid[a + 1, b].Visited == false)
                {
                    grid[a + 1, b].Visited = true;
                    FloodFill(a + 1, b);
                }

                if (a > 0 && grid[a, b].LiveNeighbors == 0 && grid[a - 1, b].Visited == false)
                {

                    grid[a - 1, b].Visited = true;
                    FloodFill(a - 1, b);

                }

                if (b < Size - 1 && grid[a, b].LiveNeighbors == 0 && grid[a, b + 1].Visited == false)
                {
                    grid[a, b + 1].Visited = true;
                    FloodFill(a, b + 1);
                }

                if (b > 0 && grid[a, b].LiveNeighbors == 0 && grid[a, b - 1].Visited == false)
                {
                    grid[a, b - 1].Visited = true;
                    FloodFill(a, b - 1);
                }

                if (a < Size - 1 && b < Size - 1 && grid[a, b].LiveNeighbors == 0 && grid[a + 1, b + 1].Visited == false)
                {
                    grid[a + 1, b + 1].Visited = true;
                    FloodFill(a + 1, b + 1);
                }

                if (a > 0 && b > 0 && grid[a, b].LiveNeighbors == 0 && grid[a - 1, b - 1].Visited == false)
                {
                    grid[a - 1, b - 1].Visited = true;
                    FloodFill(a - 1, b - 1);
                }

                if (a > 0 && b < Size - 1 && grid[a, b].LiveNeighbors == 0 && grid[a - 1, b + 1].Visited == false)
                {
                    grid[a - 1, b + 1].Visited = true;
                    FloodFill(a - 1, b + 1);
                }

                if (a < Size - 1 && b > 0 && grid[a, b].LiveNeighbors == 0 && grid[a + 1, b - 1].Visited == false)
                {
                    grid[a + 1, b - 1].Visited = true;
                    FloodFill(a + 1, b - 1);
                }
            }



            /// <summary>
            /// Checks and sees if the game is over, either the user hits a bomb, or their are no more squares to reveal that arent bombs
            /// </summary>
            /// <param name="board"></param>
            /// <param name="row"></param>
            /// <param name="col"></param>
            /// <returns></returns>
            public int IsGameOverGUI(int row, int col)
            {
                //return breaks the loop 
                if (grid[row, col].Visited == true && grid[row, col].Live == true)
                {
                    // lose is 0 because zero has losing energy
                    return 0;
                }
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        if (grid[i, j].Visited == false && grid[i, j].Live == false)
                        {
                            //50 is coninue the game, halfway energy
                            return 50;
                        }
                    }
                }
                //once we have no more visited cells that are not bombs, we win the game

                //100 gives winning energy
                return 100;


            }



        }

    }
