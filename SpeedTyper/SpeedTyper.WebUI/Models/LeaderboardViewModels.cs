using SpeedTyper.DataObjects;
using System.Collections.Generic;

namespace SpeedTyper.WebUI.Models
{
    public class LeaderboardViewModels
    {
        public class TestResultsModel
        {
            public List<TestResult> TopTestResults { get; set; }
            public List<Rank> Ranks { get; set; }
        }

        public class HighestRankedPlayersModel
        {
            public List<User> TopPlayers { get; set; }
            public List<Rank> Ranks { get; set; }
        }
    }
}