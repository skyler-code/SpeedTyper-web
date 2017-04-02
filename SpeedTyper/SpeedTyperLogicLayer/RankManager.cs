using SpeedTyper.DataAccess;
using SpeedTyper.DataObjects;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace SpeedTyper.LogicLayer
{
    public class RankManager : IRankManager
    {
        private List<Rank> _ranks = null;


        public void RetrieveUserRanks()
        {
            try
            {
                _ranks = UserAccessor.RetrieveUserRankNames();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string RetrieveUserRankName(int rankID)
        {
            string rankName = "";
            if (_ranks == null)
            {
                RetrieveUserRanks();
            }
            rankName = _ranks[_ranks.FindIndex(r => r.RankID.Equals(rankID))].RankName;

            return rankName;
        }

    }
}
