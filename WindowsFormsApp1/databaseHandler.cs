using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class databaseHandler
    {
        MySqlConnection connection;
        public databaseHandler()
        {
            string host = "localhost";
            string username = "root";
            string password = "";
            string dbName = "Trabant";

            string connectionString = $"username={username};password={password};host={host};database={dbName}";
            connection = new MySqlConnection(connectionString);

        }
        string tablename = "cars";
        public void readAll()
        {
            try
            {
                connection.Open();
                string query = $"SELECT * from cars";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    int id = read.GetInt32(read.GetOrdinal("id"));
                    string make = read.GetString(read.GetOrdinal("make"));
                    string model = read.GetString(read.GetOrdinal("model"));
                    string color = read.GetString(read.GetOrdinal("color"));
                    int year = read.GetInt32(read.GetOrdinal("year"));
                    int hp = read.GetInt32(read.GetOrdinal("hp"));
                    car onecar = new car();
                    onecar.id = id;
                    onecar.make = make;
                    onecar.model = model;
                    onecar.color = color;
                    onecar.year = year;
                    onecar.hp = hp;
                }
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"Error:");
            }
        }
        public void addone(car onecar)
        {
            try
            {
                connection.Open();
                string query = $"insert into {tablename} (make,model,color,year,hp)" +$"values ('{onecar.make}','{onecar.model}','{onecar.color}','{onecar.year}','{onecar.hp}')";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                MessageBox.Show("sikerult hozzaadni", ":)");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void deleteone(car onecar)
        {
            try
            {
                connection.Open();
                string query = $"delete from {tablename} where id = {onecar.id}";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
