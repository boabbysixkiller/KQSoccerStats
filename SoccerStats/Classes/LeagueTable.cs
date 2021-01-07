using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerStats.Classes
{
    public class LeagueTable
    {
        public string Name { get; set; }

        public List<LeagueTableEntry> Table { get; set; }
    }
}