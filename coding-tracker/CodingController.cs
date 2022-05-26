// See https://aka.ms/new-console-template for more information
using System;
using System.Configuration;
using Microsoft.Data.Sqlite;


namespace coding_tracker
{
    internal class CodingController
    {
        string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
       
       internal void Get()
       {
           List<Coding> tableData =  new List<Coding>();
           using (var connection = new SqliteConnection(connectionString)) 
           {
               using(var tableCmd = connection.CreateCommand())
               {
                   connection.Open();
                   tableCmd.CommandText = $"SELECT * FROM coding";
                   using (var reader = tableCmd.ExecuteReader())
                   {
                       if (reader.HasRows())
                       {
                           while (reader.Read())
                           {
                               tableData.Add(
                                   new Coding
                                   {
                                       Id = reader.GetInt32(0),
                                       Date = reader.GetString(1),
                                       Duration = reader.GetString(2)
                                   }); 
                           }                            
                        }
                        else
                        {
                            Console.WriteLine("\n\n No rows found\n\n");
                        }
                    }
                }
                Console.WriteLine("\n\n");
               }
               TableVisualisation.ShowTable(tableData);
        }

        internal void GetById(int id)
        {
           using (var connection = new SqliteConnection(connectionString)) 
            {
               using(var tableCmd = connection.CreateCommand())
               {
                   connection.Open();
                   tableCmd.CommandText = $"SELECT * FROM coding Where Id = '{id}'";
                   using (var reader = tableCmd.ExecuteReader())
                   {
                       if (reader.HasRows())
                       {
                           while (reader.Read())
                           {
                               tableData.Add(
                                   new Coding
                                   {
                                       Id = reader.GetInt32(0),
                                       Date = reader.GetString(1),
                                       Duration = reader.GetString(2)
                                   }); 
                           }                            
                       }
                       else
                       {
                          Console.WriteLine("\n\n No rows found\n\n");
                       }
                    }
                }
                Console.WriteLine("\n\n");
            }
               TableVisualisation.ShowTable(tableData);
        }
        

        internal void Post(Coding coding)
        {
           using (var connection = new SqliteConnection(connectionString)) 
           {
               using(var tableCmd = connection.CreateCommand)
               {
                   connection.Open();
                   tableCmd.CommandText = $"INSERT INTO Coding (date, duration) VALUES ('{coding.Date}, {coding.Duration}')";
                   tableCmd.ExecuteNonQuery();
               }
           }
        }

        internal void Delete(int id)
        {
            using (var connection = new SqliteConnection(connectionString)) 
            {
               using(var tableCmd = connection.CreateCommand())
               {
                   connection.Open();
                   tableCmd.CommandText = $"DELETE FROM coding WHERE Id = '{id}'";
                   tableCmd.ExecuteNonQuery();

                   Console.WriteLine($"\n\n Record with Id {id} was deleted. \n\n");
               }
            }
        }

        internal void Update(Coding coding)
        {
            using (var connection = new SqliteConnection(connectionString)) 
            {
               using(var tableCmd = connection.CreateCommand())
               {
                   connection.Open();
                   tableCmd.CommandText = 
                   $@"UPDATE coding SET 
                   Date = '{coding.Date}',
                   Duration = '{coding.Duration}',
                   WHERE Id = {coding.Id}";

                   tableCmd.ExecuteNonQuery();

                   Console.WriteLine($"\n\n Record with Id {coding.Id} was deleted. \n\n");
               }
            }
        }
    }
}