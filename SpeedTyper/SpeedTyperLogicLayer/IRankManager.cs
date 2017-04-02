namespace SpeedTyper.LogicLayer
{
    public interface IRankManager
    {
        string RetrieveUserRankName(int rankID);
        void RetrieveUserRanks();
    }
}