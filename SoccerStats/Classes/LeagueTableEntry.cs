using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerStats.Classes
{
    public class LeagueTableEntry
    {
        public string Team { get; set; }

        public int Wins { get; set; }

        public int Draws { get; set; }

        public int Loses { get; set; }

        public int Points { get; set; }
    }
}