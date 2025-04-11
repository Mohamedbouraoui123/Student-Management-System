using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Transparent_Form
{
    class StudentClass
    {
        private DBconnect connect;

        public StudentClass()
        {
            // Initialize the database connection
            connect = new DBconnect("localhost", "root", "yourpassword", "studentdb");

            // Test connection on initialization
            if (!TestConnection())
            {
                throw new Exception("Failed to connect to database. Please check your credentials.");
            }
        }

        private bool TestConnection()
        {
            try
            {
                connect.OpenConnection();
                connect.CloseConnection();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Connection error: " + ex.Message);
                return false;
            }
        }

        public bool InsertStudent(string fname, string lname, DateTime bdate, string gender, string phone, string address, byte[] img)
        {
            string query = "INSERT INTO `student`(`StdFirstName`, `StdLastName`, `Birthdate`, `Gender`, `Phone`, `Address`, `Photo`) " +
                           "VALUES(@fn, @ln, @bd, @gd, @ph, @adr, @img)";

            try
            {
                using (MySqlCommand command = new MySqlCommand(query, connect.GetConnection()))
                {
                    command.Parameters.AddWithValue("@fn", fname);
                    command.Parameters.AddWithValue("@ln", lname);
                    command.Parameters.AddWithValue("@bd", bdate);
                    command.Parameters.AddWithValue("@gd", gender);
                    command.Parameters.AddWithValue("@ph", phone);
                    command.Parameters.AddWithValue("@adr", address);
                    command.Parameters.AddWithValue("@img", img ?? (object)DBNull.Value);

                    connect.OpenConnection();
                    return command.ExecuteNonQuery() == 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting student: " + ex.Message);
                return false;
            }
            finally
            {
                connect.CloseConnection();
            }
        }

        public DataTable GetStudentList(MySqlCommand command)
        {
            DataTable table = new DataTable();

            try
            {
                command.Connection = connect.GetConnection();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    connect.OpenConnection();
                    adapter.Fill(table);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting student list: " + ex.Message);
            }
            finally
            {
                connect.CloseConnection();
            }

            return table;
        }

        public bool DeleteStudent(int id)
        {
            string query = "DELETE FROM `student` WHERE `StdId` = @id";

            try
            {
                using (MySqlCommand command = new MySqlCommand(query, connect.GetConnection()))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connect.OpenConnection();
                    return command.ExecuteNonQuery() == 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting student: " + ex.Message);
                return false;
            }
            finally
            {
                connect.CloseConnection();
            }
        }
    }
}
