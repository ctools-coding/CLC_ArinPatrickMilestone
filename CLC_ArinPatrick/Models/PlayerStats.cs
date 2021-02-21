using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minesweeper_ArinPatrick.Models
{
    public class PlayerStats : IComparable<PlayerStats>
    {
        //player stats getters and setters
        public string name { get; private set; }
        public int mineScore { get; private set; }

        public int difficulty { get; private set; }

        public int gridSize { get; private set; }

        public double timeElapsed { get; private set; }

        //player stats constructor
        public PlayerStats(string name, int score, int difficulty, int size, double time)
        {
            this.name = name;
            this.mineScore = score;
            this.difficulty = difficulty;
            this.gridSize = size;
            this.timeElapsed = time;
        }

        public int CompareTo(PlayerStats other)
        {
            return other.mineScore.CompareTo(mineScore);
        }
    }
}

