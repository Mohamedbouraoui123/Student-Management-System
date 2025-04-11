using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Transparent_Form
{
    public class DBconnect
    {
        private MySqlConnection connection;

        public DBconnect(string server, string user, string password, string database)
        {
            // Use string interpolation to include variables in the connection string
            string connectionString = "Server={server};Database={database};Uid={user};Pwd={password};";
            connection = new MySqlConnection(connectionString);
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }

        public void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }

        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }
}
