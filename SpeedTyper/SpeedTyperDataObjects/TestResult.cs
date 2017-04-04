using System.ComponentModel.DataAnnotations;

namespace SpeedTyper.DataObjects
{
    public class TestResult
    {
        public int TestResultID { get; set; }
        [Display(Name ="Display Name")]
        public string DisplayName { get; set; }
        [Display(Name = "Rank")]
        public int RankID { get; set; } // This is only for retrieving
        public decimal WPM { get; set; }
        public int SecondsElapsed { get; set; }
        public string Date { get; set; }
    }
}
