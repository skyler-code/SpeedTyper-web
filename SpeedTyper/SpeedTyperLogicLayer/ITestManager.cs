using System.Collections.Generic;
using SpeedTyper.DataObjects;

namespace SpeedTyper.LogicLayer
{
    public interface ITestManager
    {
        decimal CalculateWPM(string correctWords, decimal secondsElapsed);
        List<TestResult> GetAllTopTestResults();
        List<TestResult> GetTodaysResults();
        List<TestResult> GetTop10TestResults();
        List<TestResult> GetTop30DaysResults();
        List<TestResult> GetTop90DaysResults();
        List<TestResult> GetUserLast10TestResults(int userID);
        TestData RetrieveRandomTest();
        TestResult SaveTestResults(int userID, int testID, decimal WPM, int secondsElapsed);
        string SecondsToTimeSpanFormatter(int seconds);
        string TicksToTimeSpanFormatter(long ticks);
        TestData RetrieveTestDataByID(int testID);
    }
}