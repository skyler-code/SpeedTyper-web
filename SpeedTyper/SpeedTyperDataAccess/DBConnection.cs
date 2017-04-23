using System.Data.SqlClient;
using System.Configuration;

namespace SpeedTyper.DataAccess
{
    class DBConnection
    {
        internal static SqlConnection GetConnection()
        {
            string connString;
#if DEBUG
            connString = ConfigurationManager.ConnectionStrings["LocalSpeedTyperDB"].ConnectionString;
#else

            connString = ConfigurationManager.ConnectionStrings["AzureSpeedTyperDB"].ConnectionString;
#endif
            var conn = new SqlConnection(connString);
            return conn;
        }
    }
}
