using System.Data.OleDb;

namespace ProftaakGame
{
    internal class DatabaseConnection
    {
        private OleDbConnection connection;

        public DatabaseConnection() // constructor Function
        {
            const string strProject = "YourServer"; //Enter your SQL server instance name
            const string strDatabase = "YourDatabase"; //Enter your database name
            const string strUserId = "testUser"; // Enter your SQL Server User Name
            const string strPassword = "testPassword"; // Enter your SQL Server Password

            const string strconn = "data source=" + strProject +
                                   ";Persist Security Info=false;database=" + strDatabase +
                                   ";user id=" + strUserId + ";password=" +
                                   strPassword + ";Connection Timeout = 0";

            connection = new OleDbConnection(strconn);
        }

        private void test() {}
    }
}