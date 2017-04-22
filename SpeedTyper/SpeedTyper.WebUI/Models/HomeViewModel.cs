using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedTyper.WebUI.Models
{
    public class HomeViewModel
    {
        public DataObjects.User User { get; set; }
        public string Greeting { get; set; }
        public int PreviousLevelXPToLevel { get; set; }

    }
}