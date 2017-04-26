using SpeedTyper.DataObjects;
using System.Collections.Generic;
using System.Web;
using SpeedTyper.LogicLayer;

namespace SpeedTyper.WebUI.Infrastructure
{
    public static class CacheManager
    {
        private static System.Web.Caching.Cache _cache = HttpContext.Current.Cache;
        public static List<Rank> CachedRanks()
        {
            var rankManager = new RankManager();
            if (_cache["RankList"] == null)
            {
                _cache["RankList"] = rankManager.RetrieveUserRanks();
            }
            return (List<Rank>)_cache["RankList"];
        }

        public static List<int> RequiredXPForLevelList()
        {
            if(_cache["RequiredXPList"] == null)
            {
                var levelManager = new LevelManager();
                var reqXPList = new List<int>();
                for(int i = 0; i <= Constants.MAXLEVEL + 1; i++)
                {
                    reqXPList.Add(levelManager.RetrieveXPForLevel(i));
                }
                _cache["RequiredXPList"] = reqXPList;
            }
            return (List<int>)_cache["RequiredXPList"];
        }
    }
}