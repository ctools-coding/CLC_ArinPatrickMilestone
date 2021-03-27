using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Minesweeper_ArinPatrick.Models;

namespace Minesweeper_ArinPatrick.Services.Business
{
    public class Game
    {  
        int liveCount = 0;
        int safeCount = 0;
        public int gameOver(List<Cell> cellList)
        {
            int gameOver = 0;

            for(int square = 0; square < cellList.Count(); square++)
            {
                if (cellList.ElementAt(square).Live == true && cellList.ElementAt(square).Visited == false)
                {
                    liveCount++;
                }
                if(cellList.ElementAt(square).Live == false && cellList.ElementAt(square).Visited == true)
                {
                    safeCount++;
                }

                if(cellList.Count() - safeCount == liveCount)
                {
                    return gameOver = 1;
                }
                if (cellList.ElementAt(square).Visited == true && cellList.ElementAt(square).Live == true)
                {
                    return gameOver = -1;
                }

            }
            return gameOver;
        }
    }
}