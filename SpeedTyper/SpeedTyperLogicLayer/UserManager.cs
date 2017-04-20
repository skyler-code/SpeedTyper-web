using SpeedTyper.DataObjects;
using SpeedTyper.DataAccess;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace SpeedTyper.LogicLayer
{
    public class UserManager : IUserManager
    {
        internal string HashSHA256(string source)
        {
            var result = "";
            byte[] data;
            using (SHA256 sha256hash = SHA256.Create())
            {
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            var s = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            result = s.ToString();
            return result;
        }
        public User AuthenticateUser(string username, string password)
        {
            User user = null;

            if (username.Length < 4)
            {
                throw new ApplicationException("Invalid Username");
            }

            try
            {
                if (1 == UserAccessor.VerifyUsernameAndPassword(username, HashSHA256(password)))
                {
                    password = null;

                    // need to create a user object to use as an access token
                    // get a user object
                    user = UserAccessor.RetrieveUserByUsername(username);
                }
                else
                {
                    throw new ApplicationException("Authentication Failed!");
                }


                // return the user object
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }

        public User CreateUser(string username, string displayname, string password)
        {
            /**
             * Creates user and then returns the user if successful
             */
            Regex nameRegex = new Regex(Constants.NAMEREGEX);
            Regex passwordRegex = new Regex(Constants.PASSWORDREGEX);
            User _user = null;

            if (nameRegex.Match(username).Success == false)
            {
                throw new ApplicationException("Username must be 4-20 characters long and can only contain letters, numbers, and underscores.");
            }
            else if (nameRegex.Match(displayname).Success == false)
            {
                throw new ApplicationException("Display Name must be 4-20 characters long and can only contain letters, numbers, and underscores.");
            }
            else if (passwordRegex.Match(password).Success == false)
            {
                throw new ApplicationException("Password must have at least 8 characters, 1 letter, and 1 number");
            }


            try
            {
                // can't use a username that already exists.
                if (VerifyIfUserNameExists(username) == true)
                {
                    throw new ApplicationException("Username already exists!");
                }
                else
                {
                    if (1 == UserAccessor.CreateUser(username, displayname, HashSHA256(password)))
                    {
                        password = null;
                        _user = UserAccessor.RetrieveUserByUsername(username);
                    }
                    else
                    {
                        throw new ApplicationException("Account Creation Failed!");
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            return _user;
        }


        public User UpdateUser(int userID, string oldDisplayName, string newDisplayName, string oldPassword, string newPassword)
        {
            /**
             * Creates user and then returns the user if successful
             */
            Regex nameRegex = new Regex(Constants.NAMEREGEX);
            Regex passwordRegex = new Regex(Constants.PASSWORDREGEX);
            User _user = null;

            if (nameRegex.Match(newDisplayName).Success == false)
            {
                throw new ApplicationException("Display Name must be 4-20 characters long and can only contain letters, numbers, and underscores.");
            }
            else if (passwordRegex.Match(newPassword).Success == false)
            {
                throw new ApplicationException("New password must have at least 8 characters, 1 letter, and 1 number");
            }


            try
            {

                if (1 == UserAccessor.UpdateUser(userID, oldDisplayName, newDisplayName, HashSHA256(oldPassword), HashSHA256(newPassword)))
                {
                    oldPassword = null;
                    newPassword = null;
                    _user = UserAccessor.RetrieveUserByID(userID);
                }
                else
                {
                    throw new ApplicationException("Account Update Failed!");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _user;
        }

        public User CreateGuestUser()
        {
            return new User()
            {
                UserID = 0,
                UserName = "Guest",
                DisplayName = "<none>",
                RankID = 0,
                CurrentXP = 0,
                XPToLevel = 0,
                IsGuest = true
            };
        }

        public bool VerifyIfUserNameExists(string username)
        {
            bool userNameFound = false;
            // If we find a user with supplied username, then return true
            try
            {
                if (UserAccessor.RetrieveUserByUsername(username) != null)
                {
                    userNameFound = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userNameFound;
        }

        public User RetrieveUserByUsername(string username)
        {
            User returnUser = null;
            try
            {
                returnUser = UserAccessor.RetrieveUserByUsername(username);
            }
            catch (Exception)
            {
                throw;
            }
            return returnUser;
        }

        public Tuple<User, int, bool> UserLevelingHandler(User user, int earnedXP)
        {
            int oldLevel = user.Level;
            int oldCurrentXP = user.CurrentXP;

            LevelManager levelManager = new LevelManager();
            RankManager rankManager = new RankManager();
            bool titleEarned = false;
            int levelsEarned = 0;
            try
            {
                // Apply the xp earned to the user.
                user = levelManager.addXPToUser(user, earnedXP);
                // Save the new user info to the database
                if (1 == UserAccessor.UpdateUserLevelInfo(user.UserID, oldLevel, user.Level, oldCurrentXP, user.CurrentXP))
                {
                    levelsEarned = user.Level - oldLevel;

                    // Rank handling
                    if ((user.Level == Constants.MAXLEVEL))
                    {
                        // The user is max level so we attempt to take over the throne.
                        if (user.UserID == UserAccessor.Succession())
                        {
                            titleEarned = true;
                        }
                    }
                    else if (levelsEarned > 0) // probably redundant
                    {
                        for (int i = 0; i < levelsEarned; i++)
                        {
                            // user gets ranked up!
                            if (1 == UserAccessor.UserRankUp(user.UserID))
                            {
                                titleEarned = true;
                            }
                        }
                    }
                    // Retrieve a fresh copy of the user from the database.
                    user = UserAccessor.RetrieveUserByID(user.UserID);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Tuple.Create(user, levelsEarned, titleEarned);
        }

        public List<User> RetrieveHighestRankingMembers()
        {
            var returnList = new List<User>();
            try
            {
                returnList = UserAccessor.LoadHighestRankingMembers();
            }
            catch (Exception)
            {
                throw;
            }
            return returnList;
        }
    }
}
