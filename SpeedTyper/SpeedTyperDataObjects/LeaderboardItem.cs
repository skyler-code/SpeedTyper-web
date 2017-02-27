using System.Windows.Media;

namespace SpeedTyper.DataObjects
{
    public class LeaderboardItem
    {
        public int BoardRanking { get; set; }
        public ImageSource RankIcon { get; set; }
        public string RankName { get; set; }
        public string DisplayName { get; set; }
        public decimal WPM { get; set; }
        public string Date { get; set; }
        public int CurrentXP { get; set; }
    }
}
