// See https://aka.ms/new-console-template for more information
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Collections;
using Microsoft.Data.Sqlite;

namespace coding_tracker
{
    class Program
    {
        string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        static void Main(string[] args)
        {
            DatabaseManager databaseManager = new();
            GetUserInput getUserInput = new();
            
            databaseManager.CreateTable(connectionString);
            getUserInput.MainMenu();
           
        }
    }
}

 