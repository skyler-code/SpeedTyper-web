using System;
using System.Collections.Generic;
using SpeedTyper.DataObjects;

namespace SpeedTyper.LogicLayer
{
    public interface IUserManager
    {
        User AuthenticateUser(string username, string password);
        User CreateGuestUser();
        User CreateUser(string username, string displayname, string password);
        List<User> RetrieveHighestRankingMembers();
        User UpdateUser(int userID, string oldDisplayName, string newDisplayName, string oldPassword, string newPassword);
        Tuple<User, int, bool> UserLevelingHandler(User user, int earnedXP);
        bool VerifyIfUserNameExists(string username);
    }
}