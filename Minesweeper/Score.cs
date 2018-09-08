using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    [Serializable()]
    class Score : IComparable<Score>
    {
        private string playerName;
        private string difficultyLevel;
        private int score_time;
        private static FileStream fs;

        // Default constructor
        public Score()
        {
            playerName = "John Doe";
            difficultyLevel = "";
            score_time = 999;
        }

        // Constructor with parameters
        public Score(string playerName, string difficultyLevel, int score_time)
        {
            this.playerName = playerName;
            this.difficultyLevel = difficultyLevel;
            this.score_time = score_time;
        }

        public int Score_time { get { return score_time; } }

        // Serialization of list of Score objects
        public static void WriteScores(List<Score> listOfScores, string fileName)
        {
            BinaryFormatter bf = new BinaryFormatter();
            fs = File.OpenWrite(fileName);
            bf.Serialize(fs, listOfScores);
            fs.Dispose();
        }

        // Deserialization of list of Score objects
        public static List<Score> ReadScores(string fileName)
        {
            BinaryFormatter bf = new BinaryFormatter();
            fs = File.OpenRead(fileName);
            List<Score> lista = bf.Deserialize(fs) as List<Score>;
            fs.Dispose();
            return lista;
        }

        // ToString method
        public override string ToString()
        {
            return "Name: " + playerName + "       Time: " + score_time;
        }

        // CompareTo method from comparable interface
        public int CompareTo(Score other)
        {
            if (this.score_time < other.score_time)
                return -1;
            if (this.score_time > other.score_time)
                return 1;
            return 0;
        }
    }
}
