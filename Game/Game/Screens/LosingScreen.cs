using Game.UI;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Game.Screens
{
    public class LosingScreen : GameScreen
    {
        List<string> scores;

        public LosingScreen()
        {
            scores = new List<string>();
        }

        public bool CheckScore(int Score)
        {
            if (scores.Count < 10)
                return true;

            if (Score > Int32.Parse(scores[scores.Count - 1].Split(',')[1]))
                return true;
            else
                return false;
        }

        public void Initialize()
        {
            scores.AddRange(System.IO.File.ReadAllLines("Text/HighScores.txt"));
        }

        public void AddScore(int Score, string Name)
        {
            scores.Add(Name + "," + Score);

            scores.Sort(compareHighScores);

            scores.RemoveAt(scores.Count - 1);

            System.IO.File.WriteAllLines("Text/HighScores.txt", scores);

        }

        private static int compareHighScores(string x1, string y1)
        {
            string[] x = x1.Split(',');
            string[] y = y1.Split(',');

            if(Int32.Parse(x[1]) > Int32.Parse(y[1])) return 1;
            else if (Int32.Parse(x[1]) < Int32.Parse(y[1])) return -1;
            else return 0;
        }
    }
}
