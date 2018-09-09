using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Minesweeper
{
    [Serializable()]
    class Score : IComparable<Score>
    {
        private static FileStream fs;

        // Default constructor
        public Score()
        {
            PlayerName = "N/A";
            DifficultyLevel = "";
            Score_time = 999;
        }

        // Constructor with parameters
        public Score(string playerName, string difficultyLevel, int score_time)
        {
            PlayerName = playerName;
            DifficultyLevel = difficultyLevel;
            Score_time = score_time;
        }

        // Getters
        public string PlayerName { get; }
        public int Score_time { get; }
        public string DifficultyLevel { get; }

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

        // CompareTo method from comparable interface (used for Sort method on list of scores)
        public int CompareTo(Score other)
        {
            if (this.Score_time < other.Score_time)
                return -1;
            if (this.Score_time > other.Score_time)
                return 1;
            return 0;
        }
    }
}
