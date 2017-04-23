using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SpeedTyper.LogicLayer;
using SpeedTyper.DataObjects;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.Identity;

namespace SpeedTyper.WebUI.Hubs
{
    [HubName("testHub")]
    public class TestHub : Hub
    {
        ITestManager testManager;
        IUserManager userManager;
        ILevelManager levelManager;
        public TestHub(ITestManager _testManager, IUserManager _userManager, ILevelManager _levelManager)
        {
            testManager = _testManager;
            userManager = _userManager;
            levelManager = _levelManager;
        }

        public void StartTest()
        {
            TestData testData = testManager.RetrieveRandomTest();
            Clients.Caller.beginTest(testData.TestDataText, testData.DataSource, testData.TestID);
        }

        public void SubmitTest(int testID, decimal wpm, int timeElapsed)
        {
            if (wpm > 0)
            {
                if (Context.User.Identity.IsAuthenticated && wpm > 0)
                {
                    try
                    {
                        var user = userManager.RetrieveUserByUsername(Context.User.Identity.GetUserName());
                        TestResult testResult = testManager.SaveTestResults(user.UserID, testID, wpm, timeElapsed);
                        var wpmXPModifier = levelManager.GetWPMXPModifier(wpm);
                        var timeXPModifier = levelManager.GetTimeXPModifier((decimal)timeElapsed);
                        var earnedXP = levelManager.CalculateXP(wpm, wpmXPModifier, timeXPModifier);

                        string submissionString = "You have earned " + earnedXP + " XP!\n" +
                                        "WPM = " + testResult.WPM + "\n" +
                                        "WPM Modifier = " + wpmXPModifier + "\n" +
                                        "Time Modifier = " + timeXPModifier + "\n" +
                                        testResult.WPM + " x (" + wpmXPModifier + " + " + timeXPModifier + ") = " + earnedXP;

                        var appliedXPTuple = userManager.UserLevelingHandler(user, earnedXP);
                        user = appliedXPTuple.Item1;
                        int levelsGained = appliedXPTuple.Item2;
                        bool titlesEarned = appliedXPTuple.Item3;
                        string rewardString = "";
                        var rankName = Infrastructure.CacheManager.CachedRanks().Find(r => r.RankID == user.RankID).RankName;

                        if (levelsGained > 0 || titlesEarned == true)
                        {
                            if (levelsGained > 1)
                            {
                                rewardString = "Wow! Somehow, you managed to earn more than 1 level. Congrats!";
                            }
                            else if (levelsGained == 1)
                            {
                                rewardString = "You have leveled up!\nYou are now level " + user.Level +
                                               "\nYou have earned the rank: " + rankName;
                            }
                            else
                            {
                                rewardString = "You have earned the rank: " + rankName;
                            }
                        }
                        var previousLevelXPToLevel = levelManager.RetrieveXPForLevel(user.Level);
                        var xpString = Infrastructure.DisplayHelpers.ProgressBarXP(user.CurrentXP, user.XPToLevel, previousLevelXPToLevel);
                        var widthPercentString = Infrastructure.DisplayHelpers.ProgressBarWidthPercent(user.CurrentXP, user.XPToLevel, previousLevelXPToLevel);
                        var greeting = "Welcome, " + rankName + " " + user.DisplayName + "!";
                        Clients.Caller.testSubmitSuccess(submissionString, rewardString);
                        Clients.Caller.updatePage(user.CurrentXP, user.XPToLevel, xpString, widthPercentString, greeting);
                    }
                    catch (Exception ex)
                    {
                        Clients.Caller.testSubmitFailure(ex.Message);
                    }
                }
                else // Allow guests to submit scores
                {
                    try
                    {
                        testManager.SaveTestResults(userManager.CreateGuestUser().UserID, testID, wpm, timeElapsed);
                        Clients.Caller.testSubmitSuccess("Great job! You typed " + wpm + " wpm for " + timeElapsed + " seconds!\nRemember you must register to earn XP and titles!", "");
                    } catch(Exception ex)
                    {
                        Clients.Caller.testSubmitFailure(ex.Message);
                    }
                }
            }
        }

    }
}