using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BankProject
{
   public class DbAccess
    {
        public string ConnectionString { get; } = @"Data Source=GEORGIALAPTOP\SQLEXPRESS;Initial Catalog=afdemp_csharp_1;Integrated Security = true;";

        public DbAccess()
        {
            
        }

        public void CheckDbConnection(string connectionstring)// Checking Connection Status
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                Console.WriteLine("Checking Database Connectivity Please Wait....\n");
                try
                {
                    conn.Open();
                    var i = conn.State;
                    Console.WriteLine($"Database is {i}\n");
                    Thread.Sleep(3000);
                    Console.WriteLine("Successful Connection.\n\nRedirecting to the Login Screen.");
                    Thread.Sleep(3000);
                    Console.Clear();
                    conn.Close();

                    LoginScreen r = new LoginScreen();
                    DbAccess db = new DbAccess();

                    r.Connection(db.ConnectionString);
                }
                catch
                {
                    Console.WriteLine("An ERROR occurred while establishing a connection to SQL Server." +
                        "\n\nExiting the application...");
                    Thread.Sleep(4000);
                    Environment.Exit(0);


                }
            }

        }
    }
}
