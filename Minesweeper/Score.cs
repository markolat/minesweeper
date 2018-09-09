using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

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
            playerName = "N/A";
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

        // Getters
        public string PlayerName { get { return playerName; } }
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
