using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Classes
{
    public class Score
    {
        public string Name;
        public int ScoreValue;

        public Score(string name, int scoreValue)
        {
            this.Name = name;
            this.ScoreValue = scoreValue;
        }
    }
}
