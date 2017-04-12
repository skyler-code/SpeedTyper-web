using System.Data.SqlClient;

namespace SpeedTyper.DataAccess
{
    class DBConnection
    {
        internal static SqlConnection GetConnection()
        {
            string connString;
#if DEBUG
            connString = @"Data Source=localhost;Initial Catalog=SpeedTyperDB;Integrated Security=True";
#else

            connString = @"Server=tcp:speedtyper.database.windows.net,1433;Initial Catalog=SpeedTyperDB;Persist Security Info=False;User ID=reader;Password=SpeedPass1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
#endif
            var conn = new SqlConnection(connString);
            return conn;
        }
    }
}
