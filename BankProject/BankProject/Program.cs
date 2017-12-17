using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;


namespace BankProject
{
    class Program
    {
        static void Main(string[] args)
        {
            DbAccess dbAccess = new DbAccess();
            dbAccess.CheckDbConnection(dbAccess.ConnectionString);
           
            Console.ReadKey();
        }
    }
}
