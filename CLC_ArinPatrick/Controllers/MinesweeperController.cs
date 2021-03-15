using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Minesweeper_ArinPatrick.Models;
using Minesweeper_ArinPatrick.Services.Business;

namespace Minesweeper_ArinPatrick.Controllers
{
    public class MinesweeperController : Controller
    {
        public static Board board;
        Game game = new Game();

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
        public IActionResult HandleCellClick(string location)
        {
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
            ViewBag.win = "Good job, you won!";
            ViewBag.lose = "You lost. Get better.";
            return View("Index", cellList);
        }

        public IActionResult OnRightButtonClick(string location)
        {
            string[] coordinates = location.Split(',');
            int row = int.Parse(coordinates[0]);
            int col = int.Parse(coordinates[1]);

            board.grid[row,col].Flagged = !board.grid[row,col].Flagged;

            List<Cell> cellList = new List<Cell>();

            foreach (Cell cell in board.grid)
            {
                cellList.Add(cell);
            }
            return View("Index", cellList);
        }
        public IActionResult CheckGameOver()
        {
            List<Cell> cellList = new List<Cell>();

            foreach (Cell cell in board.grid)
            {
                cellList.Add(cell);
            }

            if (game.gameOver(cellList))
            {
                ViewBag.win = "Congrats, you won!";
            }
            else
            {
                ViewBag.win = "Nope. Try again.";
            }
            return View("_overPartial", cellList);
        }
    }
}