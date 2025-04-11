using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Transparent_Form
{
    class CourseClass
    {
        DBconnect connect = new DBconnect("localhost", "root", "yourpassword", "studentdb");

        // Create a function to insert course
        public bool InsertCourse(string cName, int hr, string desc)
        {
            string query = "INSERT INTO `course`(`CourseName`, `CourseHour`, `Description`) VALUES (@cn,@ch,@desc)";
            MySqlCommand command = new MySqlCommand(query, connect.GetConnection());

            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = cName;
            command.Parameters.Add("@ch", MySqlDbType.Int32).Value = hr;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;

            try
            {
                connect.OpenConnection();
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Insert Error: " + ex.Message);
                return false;
            }
            finally
            {
                connect.CloseConnection();
            }
        }

        // Create a function to get course list
        public DataTable GetCourse(MySqlCommand command)
        {
            DataTable table = new DataTable();
            command.Connection = connect.GetConnection();

            try
            {
                connect.OpenConnection();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get Course Error: " + ex.Message);
            }
            finally
            {
                connect.CloseConnection();
            }

            return table;
        }

        // Create an update function for course edit
        public bool UpdateCourse(int id, string cName, int hr, string desc)
        {
            string query = "UPDATE `course` SET `CourseName`=@cn, `CourseHour`=@ch, `Description`=@desc WHERE `CourseId`=@id";
            MySqlCommand command = new MySqlCommand(query, connect.GetConnection());

            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = cName;
            command.Parameters.Add("@ch", MySqlDbType.Int32).Value = hr;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;

            try
            {
                connect.OpenConnection();
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update Error: " + ex.Message);
                return false;
            }
            finally
            {
                connect.CloseConnection();
            }
        }

        // Create a function to delete a course
        public bool DeleteCourse(int id)
        {
            string query = "DELETE FROM `course` WHERE `CourseId`=@id";
            MySqlCommand command = new MySqlCommand(query, connect.GetConnection());

            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            try
            {
                connect.OpenConnection();
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete Error: " + ex.Message);
                return false;
            }
            finally
            {
                connect.CloseConnection();
            }
        }
    }
}
