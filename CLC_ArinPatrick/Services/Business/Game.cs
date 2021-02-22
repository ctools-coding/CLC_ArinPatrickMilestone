using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Diagnostics;

namespace Minesweeper_ArinPatrick.Services.Business
{
    public partial class Form2 : Form
    {//create buttons
        public Button[,] btnGame;
        //game size int
        public int GameSize;

        //board size of zero, user picks size
        Board board = new Board(0);

        //global variable for game over
        public int isGameOver = 0;

        //variable for diffictuly
        public int difficulty;
        //variable for score
        int mineScore;


        //stopwatch
        public Stopwatch stopWatch;

        //form two takes the variables passed in form one( difficulty and size) 
        public Form2(int gameDifficulty, int gameSize)
        {
            InitializeComponent();
            GameSize = gameSize;
            //use difficulty later in the form, so difficulty = game difficulty 
            difficulty = gameDifficulty;

            //btn  game is button] 
            btnGame = new Button[gameSize, gameSize];

            board = new Board(gameSize);

            //board sets up live neighbors, the more live neighbors, the more bombs. !!
            board.setUpLiveNeighbors(gameDifficulty);
            //calculate live neighbors is the actual number that is  on each cell.
            board.calculateLiveNeighbors();

            populateGame(gameSize);


        }

        /// <summary>
        /// creates the game board
        /// </summary>
        /// <param name="boardGameSize"></param>
        public void populateGame(int boardGameSize)
        {
            //makes the button width the size of the board game
            int buttonSize = pnl_gameboard.Width / boardGameSize;
            //creates square buttons
            pnl_gameboard.Height = pnl_gameboard.Width;

            //create new buttons and place them into the panel
            //NS = north south = column
            //EW = East West = row
            for (int EW = 0; EW < boardGameSize; EW++)
            {
                for (int NS = 0; NS < boardGameSize; NS++)
                {
                    btnGame[EW, NS] = new Button();
                    //width and height of each column
                    btnGame[EW, NS].Width = buttonSize;
                    btnGame[EW, NS].Height = buttonSize;
                    btnGame[EW, NS].MouseUp += Form2_MouseUp;
                    //put the buttons on the panel
                    pnl_gameboard.Controls.Add(btnGame[EW, NS]);
                    btnGame[EW, NS].Location = new Point(buttonSize * NS, buttonSize * EW);

                    btnGame[EW, NS].Name = EW.ToString() + "|" + NS.ToString();
                }
            }

        }

        /// <summary>
        /// right button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            //array of buttons. the array allows the board to know what button we are going to click
            string[] Sarr = (sender as Button).Name.ToString().Split('|');
            int BtnRow = int.Parse(Sarr[0]);
            int BtnCol = int.Parse(Sarr[1]);
            board.grid[BtnRow, BtnCol].Visited = true;
            isGameOver = board.IsGameOverGUI(BtnRow, BtnCol);

            //right button click puts a flag on the button.
            if (e.Button == System.Windows.Forms.MouseButtons.Right && btnGame[BtnRow, BtnCol].BackgroundImage == null)
            {
                btnGame[BtnRow, BtnCol].BackgroundImage = Image.FromFile(@"..\pictures\Flag.jpg");
                btnGame[BtnRow, BtnCol].BackgroundImageLayout = ImageLayout.Stretch;
            }
            //if we right button click the flag again, it turns the flag cell to null
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                btnGame[BtnRow, BtnCol].BackgroundImage = null;
            }

            //left button click actually clicks the cell.
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                // int 0 means that the user selected a bomb and the game is over.
                if (isGameOver == 0)
                {
                    for (int row = 0; row < GameSize; row++)
                    {
                        for (int col = 0; col < GameSize; col++)
                        {
                            btnGame[row, col].Text = board.grid[row, col].LiveNeighbors.ToString();
                            btnGame[row, col].BackColor = Color.Black;
                            btnGame[row, col].ForeColor = Color.Red;

                            //bomb image
                            if (board.grid[row, col].Live)
                            {
                                btnGame[row, col].BackgroundImage = Image.FromFile(@"..\pictures\Bomb.jpg");
                                btnGame[row, col].BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                    }
                    // when the game is over the game and the timer stops
                    stopWatch.Stop();
                    this.BackColor = Color.Red;

                    MessageBox.Show("I think I am cool and thats all that matters. \n -Tyler the creator \n   You survived for: " + lbl_stopwatch.Text);
                    //score shows 0 because they lost
                    Highscores form3 = new Highscores((mineScore * 0), difficulty, GameSize, stopWatch.Elapsed.TotalSeconds);
                    form3.ShowDialog();
                }
                //int 100 is when the user wins the game
                else if (isGameOver == 100)
                {
                    for (int row = 0; row < GameSize; row++)
                    {
                        for (int col = 0; col < GameSize; col++)
                        {
                            //the buttons turns green when the user wins
                            btnGame[row, col].Text = board.grid[row, col].LiveNeighbors.ToString();

                            btnGame[row, col].BackColor = Color.Green;
                            btnGame[row, col].ForeColor = Color.White;

                            if (board.grid[row, col].Live)
                            {
                                btnGame[row, col].BackgroundImage = Image.FromFile(@"..\pictures\Flag.jpg");
                                btnGame[row, col].BackgroundImageLayout = ImageLayout.Stretch;

                            }
                        }
                    }

                    //difficulty score calculator and formula, higher difficulty = higher score
                    if (difficulty <= 9)
                    {
                        mineScore = (int)(((50 * GameSize * difficulty) - stopWatch.Elapsed.TotalSeconds));
                    }
                    if (difficulty >= 10 && difficulty <= 13)
                    {
                        mineScore = (int)(((500 * GameSize * difficulty) - stopWatch.Elapsed.TotalSeconds));
                    }
                    if (difficulty >= 14 && difficulty <= 16)
                    {
                        mineScore = (int)(((5000 * GameSize * difficulty) - stopWatch.Elapsed.TotalSeconds));
                    }
                    if (difficulty >= 17 && difficulty <= 20)
                    {
                        mineScore = (int)(((50000 * GameSize * difficulty) - stopWatch.Elapsed.TotalSeconds));
                    }

                    this.BackColor = Color.Green;
                    //the timer stops and a win message is shown.
                    stopWatch.Stop();
                    MessageBox.Show("Good Job,\n Tyler would be happy\nYou took " + lbl_stopwatch.Text + " to figure out the game. \n Your score is " + mineScore);

                    Highscores form3 = new Highscores(mineScore, difficulty, GameSize, stopWatch.Elapsed.TotalSeconds);
                    form3.ShowDialog();

                }
                //continue method
                else
                {
                    //calling the flood fill recursive method in the board class
                    board.FloodFill(BtnRow, BtnCol);

                    for (int row = 0; row < GameSize; row++)
                    {
                        for (int col = 0; col < GameSize; col++)
                        {
                            if (board.grid[row, col].Visited)
                            {
                                btnGame[row, col].Text = board.grid[row, col].LiveNeighbors.ToString();

                            }
                        }
                    }
                }

            }
        }

        /// <summary>
        /// when our form 2 loads, we start the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2_Load(object sender, EventArgs e)
        {
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        /// <summary>
        /// timer format
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //concantinates formatted string for stopwatch
            this.lbl_stopwatch.Text = string.Format("{0:hh\\:mm\\:ss\\.ff}", stopWatch.Elapsed);
        }
    }
}
