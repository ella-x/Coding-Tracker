// See https://aka.ms/new-console-template for more information
using Microsoft.Data.Sqlite;


namespace coding_tracker
{
    internal class DatabaseManager
    {
        internal void CreateTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
               connection.Open();
               var tableCmd = connection.CreateCommand();
               tableCmd.CommandText = 
                    @"CREATE TABLE IF NOT EXISTS coding (
                        Id INTEGER PRIMARY KEY AUTOICEMENT,
                        Date TEXT,
                        Duration TEXT
                    )";
                    tableCmd.ExecuteNonQuery();
                    
                //connection.Close();


            }
        }
    }
}