using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class Score
    {
        private string playerName;
        private string difficultyLevel;
        private int score_time;

        // Constructor
        public Score(string playerName, string difficultyLevel, int score_time)
        {
            this.playerName = playerName;
            this.difficultyLevel = difficultyLevel;
            this.score_time = score_time;
        }

        public override string ToString()
        {
            return "Name: " + playerName + ", Category: " + difficultyLevel + ", Time: " + score_time;
        }
    }
}
