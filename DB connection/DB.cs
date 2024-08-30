using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VAG
{
    public static class DBConnection
    {
        private static SqlConnection connection;
        private static string connectionString;

        public static SqlConnection GetConnection()
        {
            if (connection == null || connection.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    LoadConnectionString();
                    connection = new SqlConnection(connectionString);
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to connect to the database: " + ex.Message);
                }
            }
            return connection;
        }

        private static void LoadConnectionString()
        {
            try
            {
                string[] lines = File.ReadAllLines("database.txt");
                connectionString = lines[0].Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to read property file: " + ex.Message);
            }
        }
    }

    public static class PropertyUtil
    {
        public static string GetPropertyString(string filename)
        {
            try
            {
                string[] lines = File.ReadAllLines(filename);
                string connectionString = lines[0].Trim();
                return connectionString;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to read property file: " + ex.Message);
                return null;
            }
        }
    }
}
