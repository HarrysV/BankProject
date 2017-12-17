using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BankProject
{
    public class FileAccess
    {
        public void SendStatement(List<Account> list, string username)//Send Statement(all users)
        {
            DateTime date = new DateTime();
            date = DateTime.Now;
            string filename = @"C:\Users\Harrys Valvis\source\repos\BankProject\statement_" + username + "_"
                + date.Day + "_" + date.Month + "_" + date.Year + ".txt";

            if (!File.Exists(filename))
            {
                File.Create(filename).Dispose();
                using (StreamWriter sw = File.CreateText(filename))
                {
                    foreach (var item in list)
                    {
                        sw.WriteLine(item);
                        Console.WriteLine();
                    }
                }
            }
            else if (File.Exists(filename))
            {
                using (StreamWriter sw = new StreamWriter(filename, true))
                {
                    foreach (var item in list)
                    {
                        sw.WriteLine(item);
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
