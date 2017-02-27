using SpeedTyper.DataAccess;
using SpeedTyper.DataObjects;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace SpeedTyper.LogicLayer
{
    public class RankManager
    {
        private RankIconLoader rankIconLoader = new RankIconLoader();

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

        public ImageSource RetrieveRankIcon(int rankID)
        {
            ImageSource rankIcon;

            try
            {
                rankIcon = rankIconLoader.LoadRankIcon(rankID);
            }
            catch (Exception)
            {
                throw;
            }

            return rankIcon;
        }

        public List<ImageSource> RetrieveRankIcons(List<int> rankIDs)
        {
            List<ImageSource> returnList = new List<ImageSource>();

            try
            {
                foreach (int rankID in rankIDs)
                {
                    returnList.Add(rankIconLoader.LoadRankIcon(rankID));
                }
            }
            catch (Exception)
            {

                throw;
            }
            return returnList;
        }

    }
}
