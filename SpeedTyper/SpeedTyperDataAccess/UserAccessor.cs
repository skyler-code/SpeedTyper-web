using SpeedTyper.DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SpeedTyper.DataAccess
{
    public class UserAccessor
    {
        public static int VerifyUsernameAndPassword(string username, string passwordHash)
        {
            var result = 0;

            // get a connection
            var conn = DBConnection.GetConnection();

            var cmdText = @"sp_authenticate_user";

            // create a command object
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 20);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.VarChar, 100);

            // set parameter values
            cmd.Parameters["@UserName"].Value = username;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            // try-catch-finally

            try
            {
                // open a connection
                conn.Open();

                // execute and capture the result
                result = (int)cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static User RetrieveUserByUsername(string username)
        {
            User user = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_user_by_username";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 20);
            cmd.Parameters["@Username"].Value = username;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    user = new User()
                    {
                        UserID = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        DisplayName = reader.GetString(2),
                        RankID = reader.GetInt32(3),
                        Level = reader.GetInt32(4),
                        CurrentXP = reader.GetInt32(5),
                        XPToLevel = reader.GetInt32(6),
                        IsGuest = false
                    };
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return user;
        }

        public static User RetrieveUserByID(int userID)
        {
            User user = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_user_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    user = new User()
                    {
                        UserID = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        DisplayName = reader.GetString(2),
                        RankID = reader.GetInt32(3),
                        Level = reader.GetInt32(4),
                        CurrentXP = reader.GetInt32(5),
                        XPToLevel = reader.GetInt32(6),
                        IsGuest = false
                    };
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return user;
        }

        public static int CreateUser(string username, string displayname, string passwordhash)
        {
            var result = 0;

            // get a connection
            var conn = DBConnection.GetConnection();

            var cmdText = @"sp_create_user";

            // create a command object
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 20);
            cmd.Parameters.Add("@DisplayName", SqlDbType.VarChar, 20);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.VarChar, 100);

            // set parameter values
            cmd.Parameters["@UserName"].Value = username;
            cmd.Parameters["@DisplayName"].Value = displayname;
            cmd.Parameters["@PasswordHash"].Value = passwordhash;

            // try-catch-finally

            try
            {
                // open a connection
                conn.Open();

                // execute and capture the result
                result = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }


        public static int UpdateUser(int userID, string oldDisplayName, string newDisplayName, string oldPasswordHash, string newPasswordHash)
        {
            var result = 0;

            // get a connection
            var conn = DBConnection.GetConnection();

            var cmdText = @"sp_update_user";

            // create a command object
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@OldDisplayName", SqlDbType.VarChar, 20);
            cmd.Parameters.Add("@NewDisplayName", SqlDbType.VarChar, 20);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.VarChar, 100);

            // set parameter values
            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;
            cmd.Parameters["@OldDisplayName"].Value = oldDisplayName;
            cmd.Parameters["@NewDisplayName"].Value = newDisplayName;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;

            // try-catch-finally

            try
            {
                // open a connection
                conn.Open();

                // execute and capture the result
                result = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static List<Rank> RetrieveUserRankNames()
        {
            List<Rank> ranks = new List<Rank>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_rank_names";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ranks.Add(new Rank()
                        {
                            RankID = reader.GetInt32(0),
                            RankName = reader.GetString(1)
                        });
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return ranks;
        }

        public static int UpdateUserLevelInfo(int userID, int oldLevel, int newLevel, int oldCurrentXP, int newCurrentXP)
        {
            var result = 0;

            // get a connection
            var conn = DBConnection.GetConnection();

            var cmdText = @"sp_update_user_level_info";

            // create a command object
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@OldLevel", SqlDbType.Int);
            cmd.Parameters.Add("@NewLevel", SqlDbType.Int);
            cmd.Parameters.Add("@OldCurrentXP", SqlDbType.Int);
            cmd.Parameters.Add("@NewCurrentXP", SqlDbType.Int);
            // set parameter values
            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@OldLevel"].Value = oldLevel;
            cmd.Parameters["@NewLevel"].Value = newLevel;
            cmd.Parameters["@OldCurrentXP"].Value = oldCurrentXP;
            cmd.Parameters["@NewCurrentXP"].Value = newCurrentXP;

            // try-catch-finally

            try
            {
                // open a connection
                conn.Open();

                // execute and capture the result
                result = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        // This is the challenge to the throne. Since only one person can have the King title, this fires a recalibration in the database.
        // A player that is level 15 and has the highest XP will be granted the max rank id. All other level 15 players will be demoted to (max rankid) - 1
        // Returns the UserID of the new King
        public static int Succession()
        {
            var userID = 0;

            // get a connection
            var conn = DBConnection.GetConnection();

            var cmdText = @"sp_succession";

            // create a command object
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;


            // try-catch-finally

            try
            {
                // open a connection
                conn.Open();

                // execute and capture the userID
                userID = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return userID;
        }


        public static int UserRankUp(int userID)
        {
            int result = 0;

            // get a connection
            var conn = DBConnection.GetConnection();

            var cmdText = @"sp_user_rank_up";

            // create a command object
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            // set parameter values
            cmd.Parameters["@UserID"].Value = userID;

            // try-catch-finally

            try
            {
                // open a connection
                conn.Open();

                // execute and capture the row count
                result = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static List<User> LoadHighestRankingMembers()
        {
            var highestRankingMembers = new List<User>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_highest_ranking_users";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        highestRankingMembers.Add(new User()
                        {
                            DisplayName = reader.GetString(0),
                            RankID = reader.GetInt32(1),
                            CurrentXP = reader.GetInt32(2)
                        });
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return highestRankingMembers;
        }
    }

}
