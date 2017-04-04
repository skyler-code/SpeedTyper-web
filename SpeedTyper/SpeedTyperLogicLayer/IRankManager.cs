using SpeedTyper.DataObjects;
using System.Collections.Generic;

namespace SpeedTyper.LogicLayer
{
    public interface IRankManager
    {
        string RetrieveUserRankName(int rankID);
        List<Rank> RetrieveUserRanks();
    }
}