using SpeedTyper.DataAccess;
using SpeedTyper.DataObjects;
using System;
using System.Collections.Generic;

namespace SpeedTyper.LogicLayer
{
    public class LeaderboardManager
    {
        private RankManager _rankManager = new RankManager();

        public LeaderboardManager(RankManager rankManager)
        {
            _rankManager = rankManager;
        }
        public List<LeaderboardItem> RetrieveTop10Leaderboard()
        {
            var _returnBoard = new List<LeaderboardItem>();
            try
            {
                var top10Results = TestAccessor.RetrieveTop10TestResults();

                var boardRanking = 1;
                foreach (TestResult t in top10Results)
                {
                    _returnBoard.Add(new LeaderboardItem()
                    {
                        BoardRanking = boardRanking,
                        RankIcon = _rankManager.RetrieveRankIcon(t.RankID),
                        RankName = _rankManager.RetrieveUserRankName(t.RankID),
                        DisplayName = t.DisplayName,
                        WPM = t.WPM,
                        Date = t.Date
                    });
                    boardRanking++;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _returnBoard;
        }

        public List<LeaderboardItem> RetrieveAllResultsLeaderboard()
        {
            var _returnBoard = new List<LeaderboardItem>();
            try
            {
                var topResults = TestAccessor.RetrieveAllTopTestResults();

                var boardRanking = 1;
                foreach (TestResult t in topResults)
                {
                    _returnBoard.Add(new LeaderboardItem()
                    {
                        BoardRanking = boardRanking,
                        RankIcon = _rankManager.RetrieveRankIcon(t.RankID),
                        RankName = _rankManager.RetrieveUserRankName(t.RankID),
                        DisplayName = t.DisplayName,
                        WPM = t.WPM,
                        Date = t.Date
                    });
                    boardRanking++;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _returnBoard;
        }

        public List<LeaderboardItem> RetrieveLast90DaysResultsLeaderboard()
        {
            var _returnBoard = new List<LeaderboardItem>();
            try
            {
                var topResults = TestAccessor.RetrieveLast90DaysTestResults();

                var boardRanking = 1;
                foreach (TestResult t in topResults)
                {
                    _returnBoard.Add(new LeaderboardItem()
                    {
                        BoardRanking = boardRanking,
                        RankIcon = _rankManager.RetrieveRankIcon(t.RankID),
                        RankName = _rankManager.RetrieveUserRankName(t.RankID),
                        DisplayName = t.DisplayName,
                        WPM = t.WPM,
                        Date = t.Date
                    });
                    boardRanking++;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _returnBoard;
        }

        public List<LeaderboardItem> RetrieveLast30DaysResultsLeaderboard()
        {
            var _returnBoard = new List<LeaderboardItem>();
            try
            {
                var topResults = TestAccessor.RetrieveLast30DaysTestResults();

                var boardRanking = 1;
                foreach (TestResult t in topResults)
                {
                    _returnBoard.Add(new LeaderboardItem()
                    {
                        BoardRanking = boardRanking,
                        RankIcon = _rankManager.RetrieveRankIcon(t.RankID),
                        RankName = _rankManager.RetrieveUserRankName(t.RankID),
                        DisplayName = t.DisplayName,
                        WPM = t.WPM,
                        Date = t.Date
                    });
                    boardRanking++;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _returnBoard;
        }

        public List<LeaderboardItem> RetrieveTodaysResultsLeaderboard()
        {
            var _returnBoard = new List<LeaderboardItem>();
            try
            {
                var topResults = TestAccessor.RetrieveTodaysTestResults();

                var boardRanking = 1;
                foreach (TestResult t in topResults)
                {
                    _returnBoard.Add(new LeaderboardItem()
                    {
                        BoardRanking = boardRanking,
                        RankIcon = _rankManager.RetrieveRankIcon(t.RankID),
                        RankName = _rankManager.RetrieveUserRankName(t.RankID),
                        DisplayName = t.DisplayName,
                        WPM = t.WPM,
                        Date = t.Date
                    });
                    boardRanking++;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _returnBoard;
        }

        public List<LeaderboardItem> RetrieveHighestRankingMembers()
        {
            var _returnBoard = new List<LeaderboardItem>();
            try
            {
                var highestRankingMembers = UserAccessor.LoadHighestRankingMembers();

                foreach (User user in highestRankingMembers)
                {
                    _returnBoard.Add(new LeaderboardItem()
                    {
                        RankIcon = _rankManager.RetrieveRankIcon(user.RankID),
                        RankName = _rankManager.RetrieveUserRankName(user.RankID),
                        DisplayName = user.DisplayName,
                        CurrentXP = user.CurrentXP
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _returnBoard;
        }


    }
}
