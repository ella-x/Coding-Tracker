// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Data.Sqlite;


namespace coding_tracker
{
    internal class Coding
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Duration { get; set; }
        
    }
}