using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Dao
{
    public class DatabaseConnector
    {
        private string connectionString = "Data Source=SAM;Initial Catalog=VirtualArtGallery;Integrated Security=True";
        private SqlConnection connection;

        public DatabaseConnector(string connectionString)
        {
            try
            {
                this.connectionString = connectionString;
                this.connection = new SqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Connection opened successfully.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error while opening connection: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
            }
        }


    }
}
