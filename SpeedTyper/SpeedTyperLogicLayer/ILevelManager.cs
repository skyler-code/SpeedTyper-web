using SpeedTyper.DataObjects;

namespace SpeedTyper.LogicLayer
{
    public interface ILevelManager
    {
        User addXPToUser(User user, int earnedXP);
        int CalculateXP(decimal WPM, decimal WPMXPModifier, decimal timeXPModifier);
        decimal GetTimeXPModifier(decimal secondsElapsed);
        decimal GetWPMXPModifier(decimal WPM);
        int RetrieveXPForLevel(int userLevel);
    }
}