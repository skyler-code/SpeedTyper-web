using System;
using Microsoft.AspNet.SignalR;
using SpeedTyper.LogicLayer;
using SpeedTyper.DataObjects;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.Identity;
using SpeedTyper.WebUI.Infrastructure;
using System.Security.Cryptography;
using System.Text;

namespace SpeedTyper.WebUI.Hubs
{
    [HubName("testHub")]
    public class TestHub : Hub
    {
        ITestManager testManager;
        IUserManager userManager;
        ILevelManager levelManager;
        private int endTimerCountdown = 120;
        public TestHub(ITestManager _testManager, IUserManager _userManager, ILevelManager _levelManager)
        {
            testManager = _testManager;
            userManager = _userManager;
            levelManager = _levelManager;
        }

        public void StartTest()
        {
            TestData testData = testManager.RetrieveRandomTest();
            Clients.Caller.beginTest(testData.TestDataText, testData.DataSource, testData.TestID, endTimerCountdown);

        }

        public void SubmitTest(int testID, decimal wpm, int timeElapsed, int _endTimerCountdown, string testData, string dataSource, string startTime, string startTimeHash)
        {
            if (wpm > 0)
            {
                if (InvalidTest(testID, startTime, _endTimerCountdown, testData, dataSource, timeElapsed, startTimeHash))
                {
                    Clients.Caller.testSubmitFailure("Cheater!");
                    return;
                }
                if (Context.User.Identity.IsAuthenticated)
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
                        var rankName = CacheManager.CachedRanks().Find(r => r.RankID == user.RankID).RankName;

                        if (levelsGained > 0 || titlesEarned)
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
                        var previousLevelXPToLevel = CacheManager.RequiredXPForLevelList()[user.Level];
                        var xpString = DisplayHelpers.ProgressBarXP(user.CurrentXP, user.XPToLevel, previousLevelXPToLevel);
                        var widthPercentString = DisplayHelpers.ProgressBarWidthPercent(user.CurrentXP, user.XPToLevel, previousLevelXPToLevel);
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
                    }
                    catch (Exception ex)
                    {
                        Clients.Caller.testSubmitFailure(ex.Message);
                    }
                }
            }
        }
        /// <summary>
        /// Returns true is test is fake.
        /// </summary>
        /// <param name="testID"></param>
        /// <param name="startTime"></param>
        /// <param name="_endTimerCountdown"></param>
        /// <param name="testData"></param>
        /// <param name="dataSource"></param>
        /// <param name="timeElapsed"></param>
        /// <param name="startTimeHash"></param>
        /// <returns></returns>
        internal bool InvalidTest(int testID, string startTime, int _endTimerCountdown, string testData, string dataSource, int timeElapsed, string startTimeHash)
        {
            TestData testVerification = testManager.RetrieveTestDataByID(testID);
            string verifyHash = HashSHA1(startTime + testID);
            if (_endTimerCountdown != endTimerCountdown ||
                !testVerification.TestDataText.StartsWith(testData) ||
                !testVerification.DataSource.Equals(dataSource) ||
                timeElapsed <= 0 ||
                !verifyHash.Equals(startTimeHash))
            {
                return true;
            }
            return false;
        }

        internal string HashSHA1(string source)
        {
            var result = "";
            byte[] data;
            using (SHA1 sha1hash = SHA1.Create())
            {
                data = sha1hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            var s = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            result = s.ToString();
            return result;
        }
    }
}