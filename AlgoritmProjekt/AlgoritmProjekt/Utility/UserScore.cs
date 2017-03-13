using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility
{
    internal class ScoreName : IComparable<ScoreName>
    {
        internal int score;
        internal string name;

        public ScoreName(int score, string name)
        {
            this.score = score;
            this.name = name; 
        }

        public int CompareTo(ScoreName other)
        {
            return other.score.CompareTo(this.score);
        }
    }

    class UserScore
    {
        public List<ScoreName> HighScore;

        public UserScore()
        {
            HighScore = new List<ScoreName>();
        }

        public void SortScores()
        {
            HighScore.Sort();
        }

        public void AddUserScore(int score, string name)
        {
            HighScore.Add(new ScoreName(score, name));
        }
    }
}
