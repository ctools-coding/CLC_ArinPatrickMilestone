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

        public bool gameOver(List<Cell> cellList)
        {
            //bool gameOver = false;

            for(int square = 1; square < cellList.Count; square++)
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
                    return true;
                }
                if(cellList.ElementAt(square).Visited == true && cellList.ElementAt(square).Live == true)
                {
                    return false;
                }

            }
            return false;
        }
    }
}