using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minesweeper_ArinPatrick.Models
{
    public class Cell
    {
        //cell class that encapsluates everything we need to create the game
        public Cell()
        {
            Row = -1;
            Column = -1;
            Visited = false;
            Live = false;
            LiveNeighbors = 0;
            Flagged = false;
            Id = 0;
        }
        public int Row { get; set; }
        public int Column { get; set; }
        public Boolean Visited { get; set; }
        public Boolean Live { get; set; }
        public int LiveNeighbors { get; set; }
        public int Id { get; set; }
        public Boolean Flagged { get; set; }
    }
}
