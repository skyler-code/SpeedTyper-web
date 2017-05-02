using SpeedTyper.DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SpeedTyper.DataAccess
{
    public class TestAccessor
    {
        public static TestData RetrieveRandomTest()
        {
            TestData testData = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_random_test";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    testData = new TestData()
                    {
                        TestID = reader.GetInt32(0),
                        TestDataText = reader.GetString(1),
                        DataSource = reader.GetString(2)
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

            return testData;
        }

        public static int SaveTestResults(int userID, int testID, decimal WPM, int secondsElapsed)
        {
            int result = 0;

            // get a connection
            var conn = DBConnection.GetConnection();

            var cmdText = @"sp_insert_test_result";

            // create a command object
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@TestID", SqlDbType.Int);
            cmd.Parameters.Add("@WPM", SqlDbType.Decimal);
            cmd.Parameters.Add("@SecondsElapsed", SqlDbType.Int);

            // set parameter values
            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@TestID"].Value = testID;
            cmd.Parameters["@WPM"].Value = WPM;
            cmd.Parameters["@SecondsElapsed"].Value = secondsElapsed;

            // try-catch-finally

            try
            {
                // open a connection
                conn.Open();

                // execute and capture the testResultID
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

        public static TestResult RetrieveTestResultsByID(int testResultID)
        {
            TestResult testResult = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_test_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TestResultID", SqlDbType.Int);
            cmd.Parameters["@TestResultID"].Value = testResultID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    testResult = new TestResult()
                    {
                        TestResultID = reader.GetInt32(0),
                        RankID = reader.GetInt32(1),
                        WPM = reader.GetDecimal(2),
                        SecondsElapsed = reader.GetInt32(3),
                        Date = TimeZoneInfo.ConvertTimeFromUtc(reader.GetDateTime(4), Constants.CENTRALTIMEINFO).ToString("MMM/dd/yyyy")
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

            return testResult;
        }

        public static TestData RetrieveTestDataByID(int testID)
        {
            TestData testData = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_testdata_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TestID", testID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    testData = new TestData()
                    {
                        TestID = reader.GetInt32(0),
                        TestDataText = reader.GetString(1),
                        DataSource = reader.GetString(2)
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

            return testData;
        }

        public static List<TestResult> RetrieveTop10TestResults()
        {
            var testResult = new List<TestResult>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_top_10_test_scores";
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
                        testResult.Add(new TestResult()
                        {
                            RankID = reader.GetInt32(0),
                            DisplayName = reader.GetString(1),
                            WPM = reader.GetDecimal(2),
                            Date = TimeZoneInfo.ConvertTimeFromUtc(reader.GetDateTime(4), Constants.CENTRALTIMEINFO).ToString("MM/dd/yyyy hh:mm tt")
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

            return testResult;
        }

        public static List<TestResult> RetrieveAllTopTestResults()
        {
            var testResult = new List<TestResult>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_all_top_test_scores";
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
                        testResult.Add(new TestResult()
                        {
                            RankID = reader.GetInt32(0),
                            DisplayName = reader.GetString(1),
                            WPM = reader.GetDecimal(2),
                            Date = TimeZoneInfo.ConvertTimeFromUtc(reader.GetDateTime(4), Constants.CENTRALTIMEINFO).ToString("MM/dd/yyyy hh:mm tt")
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

            return testResult;
        }

        public static List<TestResult> RetrieveLast90DaysTestResults()
        {
            var testResult = new List<TestResult>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_last_90_days_test_scores";
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
                        testResult.Add(new TestResult()
                        {
                            RankID = reader.GetInt32(0),
                            DisplayName = reader.GetString(1),
                            WPM = reader.GetDecimal(2),
                            Date = TimeZoneInfo.ConvertTimeFromUtc(reader.GetDateTime(4), Constants.CENTRALTIMEINFO).ToString("MM/dd/yyyy hh:mm tt")
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

            return testResult;
        }

        public static List<TestResult> RetrieveLast30DaysTestResults()
        {
            var testResult = new List<TestResult>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_last_30_days_test_scores";
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
                        testResult.Add(new TestResult()
                        {
                            RankID = reader.GetInt32(0),
                            DisplayName = reader.GetString(1),
                            WPM = reader.GetDecimal(2),
                            Date = TimeZoneInfo.ConvertTimeFromUtc(reader.GetDateTime(4), Constants.CENTRALTIMEINFO).ToString("MM/dd/yyyy hh:mm tt")
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

            return testResult;
        }

        public static List<TestResult> RetrieveTodaysTestResults()
        {
            var testResult = new List<TestResult>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_todays_test_scores";
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
                        testResult.Add(new TestResult()
                        {
                            RankID = reader.GetInt32(0),
                            DisplayName = reader.GetString(1),
                            WPM = reader.GetDecimal(2),
                            Date = TimeZoneInfo.ConvertTimeFromUtc(reader.GetDateTime(4), Constants.CENTRALTIMEINFO).ToString("MM/dd/yyyy hh:mm tt")
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

            return testResult;
        }

        public static List<TestResult> RetrieveUserLast10TestResults(int userID)
        {
            var testResult = new List<TestResult>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_user_last_10_scores";
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
                    while (reader.Read())
                    {
                        testResult.Add(new TestResult()
                        {
                            WPM = reader.GetDecimal(0),
                            SecondsElapsed = reader.GetInt32(1),
                            Date = TimeZoneInfo.ConvertTimeFromUtc(reader.GetDateTime(4), Constants.CENTRALTIMEINFO).ToString("MM/dd/yyyy hh:mm tt")
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

            return testResult;
        }
    }


}
