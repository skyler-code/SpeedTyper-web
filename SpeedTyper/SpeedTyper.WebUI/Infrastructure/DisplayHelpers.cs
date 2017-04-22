using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedTyper.WebUI.Infrastructure
{
    public static class DisplayHelpers
    {

        public static string ProgressBarWidthPercent(double currentXP, double xpToLevel, double previousLevelXPToLevel)
        {
            currentXP = currentXP - previousLevelXPToLevel;
            xpToLevel = xpToLevel - previousLevelXPToLevel;
            double widthPercent = (currentXP / xpToLevel) * 100;
            string widthPercentString;
            if (widthPercent < 0 || widthPercent > 100)
            {
                widthPercentString = "100%";
            }
            else
            {
                widthPercentString = widthPercent + "%";
            }
            return widthPercentString;
        }

        public static string ProgressBarXP(double currentXP, double xpToLevel, double previousLevelXPToLevel)
        {
            currentXP = currentXP - previousLevelXPToLevel;
            xpToLevel = xpToLevel - previousLevelXPToLevel;
            string xpString;
            if (xpToLevel > 0)
            {
                xpString = currentXP + " / " + xpToLevel;
            }
            else
            {
                xpString = currentXP + " / 1";
            }
            return xpString;
        }
        
    }
}