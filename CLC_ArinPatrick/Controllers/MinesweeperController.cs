using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Minesweeper_ArinPatrick.Models;

namespace Minesweeper_ArinPatrick.Controllers
{
    public class MinesweeperController : Controller
    {
        public IActionResult Index()
        {
            Board board = new Board(8);
            board.setUpLiveNeighbors(7);
            board.calculateLiveNeighbors();

            List<Cell> cellList = new List<Cell>();

            foreach(Cell cell in board.grid)
            {
                cellList.Add(cell);
            }
            return View("Index", cellList);
        }

    }
}
