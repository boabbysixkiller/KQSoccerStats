using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerStats.Classes
{
    public class Result
    {
        public DateTime MatchDate { get; set; }

        public string HomeTeam { get; set; }

        public int HomeGoals { get; set; }

        public string AwayTeam { get; set; }

        public int AwayGoals { get; set; }
    }
}