using SpeedTyper.DataObjects;
using SpeedTyper.DataAccess;
using System;

namespace SpeedTyper.LogicLayer
{
    public class LevelManager : ILevelManager
    {
        public int CalculateXP(decimal WPM, decimal WPMXPModifier, decimal timeXPModifier)
        {
            int returnXP = 0;

            try
            {
                returnXP = (int)(WPM * (WPMXPModifier + timeXPModifier));
            }
            catch (Exception)
            {
                throw;
            }

            return returnXP;
        }

        public decimal GetWPMXPModifier(decimal WPM)
        {
            decimal xpModifier = 1.0M;
            try
            {
                xpModifier = LevelAccessor.RetrieveWPMXPModifier(WPM);
            }
            catch (Exception)
            {
                throw;
            }
            return xpModifier;
        }

        public decimal GetTimeXPModifier(decimal secondsElapsed)
        {
            decimal xpModifier = 1.0M;
            try
            {
                xpModifier = LevelAccessor.RetrieveTimeXPModifier(secondsElapsed);
            }
            catch (Exception)
            {
                throw;
            }
            return xpModifier;
        }

        public User addXPToUser(User user, int earnedXP)
        {
            user.CurrentXP = user.CurrentXP + earnedXP;
            try
            {
                var processComplete = false;
                while (processComplete == false)
                {
                    if ((user.CurrentXP >= user.XPToLevel) && (user.Level != Constants.MAXLEVEL))
                    {
                        // User has leveled.
                        user.Level = user.Level + 1;
                        user.XPToLevel = RetrieveXPForLevel(user.Level + 1);
                    }
                    else
                    {
                        processComplete = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }

        public int RetrieveXPForLevel(int userLevel)
        {
            int xpForLevel = 0;

            try
            {
                xpForLevel = LevelAccessor.RetrieveRequiredXPForLevel(userLevel);
            }
            catch (Exception)
            {

                throw;
            }

            return xpForLevel;
        }



    }
}
