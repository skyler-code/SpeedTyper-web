using SpeedTyper.DataAccess;
using SpeedTyper.DataObjects;
using System;
using System.Collections.Generic;
using System.Web.Caching;

namespace SpeedTyper.LogicLayer
{
    public class RankManager : IRankManager
    {
        private List<Rank> _ranks = null;


        public List<Rank> RetrieveUserRanks()
        {
            try
            {
                if (_ranks == null)
                {
                    _ranks = UserAccessor.RetrieveUserRankNames();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _ranks;
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
