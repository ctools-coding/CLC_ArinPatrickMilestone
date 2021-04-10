using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Minesweeper_ArinPatrick.Models;
using Minesweeper_ArinPatrick.Services.Business;
using Minesweeper_ArinPatrick.Utility;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
namespace Minesweeper_ArinPatrick.Controllers
{
    public class MinesweeperController : Controller
    {
        static Board board;
        Game game = new Game();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CustomAuthorization]
        public IActionResult Index()
        {
            board = new Board(8);
            board.setUpLiveNeighbors(7);
            board.calculateLiveNeighbors();

            List<Cell> cellList = new List<Cell>();

            foreach (Cell cell in board.grid)
            {
                cellList.Add(cell);
            }
            return View("Index", cellList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public IActionResult PartialBoard(string location)
        {
            string[] coordinates = location.Split(',');
            int index = int.Parse(coordinates[0]);
            int row = int.Parse(coordinates[1]);
            int col = int.Parse(coordinates[2]);
            board.grid[row, col].Visited = true;

            board.FloodFill(row, col);

            board.IsGameOverGUI(row, col);

            List<Cell> cellList = new List<Cell>();

            foreach (Cell cell in board.grid)
            {
                cellList.Add(cell);
            }
            return PartialView(cellList);
        }

        /// <summary>
        /// handle button click 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        /// 
        public IActionResult HandleCellClick(string location)
        {
            string[] coordinates = location.Split(',');
            int row = int.Parse(coordinates[1]);
            int col = int.Parse(coordinates[2]);
            board.grid[row, col].Visited = true;

            board.FloodFill(row, col);

            board.IsGameOverGUI(row, col);

            List<Cell> cellList = new List<Cell>();

            foreach (Cell cell in board.grid)
            {
                cellList.Add(cell);
            }
   
            return PartialView("PartialBoard", cellList);
        }

        /// <summary>
        /// Right button click  takes the location of button and places a flag on it
        /// This is done by updating partial Board
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public IActionResult OnRightButtonClick(string location)
        {
            string[] coordinates = location.Split(',');
            int row = int.Parse(coordinates[1]);
            int col = int.Parse(coordinates[2]);

            board.grid[row,col].Flagged = !board.grid[row,col].Flagged;

            List<Cell> cellList = new List<Cell>();

            foreach (Cell cell in board.grid)
            {
                cellList.Add(cell);
            }
            return PartialView("PartialBoard", cellList);
        }
        /// <summary>
        /// Methods that need to be applied to every single cell
        /// Location determined by row and col
        /// flood fill
        /// isgameover shows if the game can keep going or not
        /// add the cell to the board
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public IActionResult OneCell(string location)
        {
            int bttnInt = int.Parse(location);
            string[] coordinates = location.Split(',');
            int row = int.Parse(coordinates[0]);
            int col = int.Parse(coordinates[1]);
            board.grid[row, col].Visited = true;

            board.FloodFill(row, col);

            board.IsGameOverGUI(row, col);

            List<Cell> cellList = new List<Cell>();

            foreach (Cell cell in board.grid)
            {
                cellList.Add(cell);
            }
            return PartialView(cellList.ElementAt(bttnInt));
        }

        /// <summary>
        /// Checks if that game is over. This is displayed under the game board indicating where the game is at
        ///   1 == win
        ///  -1 == lose
        ///   0 == ongoing game
        /// </summary>
        /// <returns></returns>
        public IActionResult CheckGameOver()
        {
            List<Cell> cellList = new List<Cell>();

            foreach (Cell cell in board.grid)
            {
                cellList.Add(cell);
            }

            if (game.gameOver(cellList) == 1)
            {
                ViewBag.win = "Congrats, you won!";
            }
            else if(game.gameOver(cellList) == -1)
            {
                ViewBag.win = "Aw see now you're a loser.";
            }
            else if(game.gameOver(cellList) == 0)
            {
                ViewBag.win = "Nope. Try again.";
            }
            return PartialView("_overPartial");
        }
        public IActionResult OnSave()
        {
            List<Cell> cellList = new List<Cell>();

            foreach (Cell cell in board.grid)
            {
                cellList.Add(cell);
            } 

            StoredGamesDAO storedDAO = new StoredGamesDAO();
            GameObject gameObject = new GameObject(1, JsonConvert.SerializeObject(cellList), HttpContext.Session.GetString("username"));
            bool success = storedDAO.SaveGame(gameObject);

            Tuple<bool, string> resultsTuple = new Tuple<bool, string>(success, JsonConvert.SerializeObject(cellList));

            return View("Results", resultsTuple);
        }
        public IActionResult OnLoad()
        {
            StoredGamesDAO storedDAO = new StoredGamesDAO();
     
            GameObject gameObject = storedDAO.LoadGame(HttpContext.Session.GetString("username"));

            List<Cell> cellList = JsonConvert.DeserializeObject<List<Cell>>(gameObject.JSONString);

            for(int i = 0; i < board.Size; i++)
            {
                for(int j = 0; j < board.Size; j++)
                {
                    board.grid[i,j] = cellList[(i * board.Size) + j];
                }
            }
            return View("Index", cellList);
        }
    }
}