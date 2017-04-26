using SpeedTyper.DataObjects;
using System.Collections.Generic;

namespace SpeedTyper.WebUI.Models
{
    public class HomeViewModel
    {
        public DataObjects.User User { get; set; }
        public string Greeting { get; set; }
        public int PreviousLevelXPToLevel { get; set; }

    }

    public class RankViewModel
    {
        public List<Rank> RankList { get; set; }
        public List<int> RequiredXPList { get; set; }
    }
}