using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDb.Model
{
    public class City
    {
        public int ID { get; set; }
        public string CityName { get; set; }
    }

    public class CityList
    {
        public List<City> Cities { get; set; }
        public CityList() { }

        public List<City> LoadCities()
        {
            Cities = new List<City>();
            // Replace with your actual database connection string
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\teach\source\repos\TestDb\DB\Hogworts1.accdb;Persist Security Info=True";

            using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ID, CityName FROM Cities";

                using (System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection))
                {
                    System.Data.OleDb.OleDbDataReader reader = command.ExecuteReader();

                    Cities = new List<City>();

                    while (reader.Read())
                    {
                        Cities.Add(new City { ID = reader.GetInt32(0), CityName = reader.GetString(1) });
                    }
                }
                connection.Close();
            }
            Console.WriteLine(Cities);
            return Cities;
        }
    }
}
