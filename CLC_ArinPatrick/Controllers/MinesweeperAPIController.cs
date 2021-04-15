using Microsoft.AspNetCore.Mvc;
using Minesweeper_ArinPatrick.Models;
using Minesweeper_ArinPatrick.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minesweeper_ArinPatrick.Controllers
{
    //route in the url to access the api
    [ApiController]
    [Route("api/[controller]")]
    public class MinesweeperAPIController : ControllerBase
    {
        StoredGamesDAO repository = new StoredGamesDAO();

        public MinesweeperAPIController()
        {
            repository = new StoredGamesDAO();
        }


        [HttpGet]
        public IEnumerable<GameObject> Index()
        {
            return repository.GetAllGames();
        }
    }
}
