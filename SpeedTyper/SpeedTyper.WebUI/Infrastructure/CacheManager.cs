using SpeedTyper.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (_cache["stRanks"] == null)
            {
                _cache["stRanks"] = rankManager.RetrieveUserRanks();
            }
            return (List<Rank>)_cache["stRanks"];
        }
    }
}