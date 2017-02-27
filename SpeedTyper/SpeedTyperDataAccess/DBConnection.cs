using System.Data.SqlClient;

namespace SpeedTyper.DataAccess
{
    class DBConnection
    {
        internal static SqlConnection GetConnection()
        {
            var connString = @"Data Source=localhost;Initial Catalog=SpeedTyperDB;Integrated Security=True";
            var conn = new SqlConnection(connString);
            return conn;
        }
    }
}
