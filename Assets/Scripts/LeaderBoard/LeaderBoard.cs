using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leaderboard
{
    public class Score
    {
        public string name { get; set; }
        public int positinon { get; set; }
        public string score { get; set; }
    }

    public class LeaderBoard
    {
        public List<Score> Scores = new List<Score>();
    }
}

