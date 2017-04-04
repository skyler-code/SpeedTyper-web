using System.ComponentModel.DataAnnotations;

namespace SpeedTyper.DataObjects
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
        [Display(Name = "Rank")]
        public int RankID { get; set; }
        public int Level { get; set; }
        [Display(Name = "Earned XP")]
        public int CurrentXP { get; set; }
        public int XPToLevel { get; set; }
        public bool IsGuest { get; set; }
    }
}
