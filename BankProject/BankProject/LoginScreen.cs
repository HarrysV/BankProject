using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace BankProject
{

    public class LoginScreen
    {
        int tries = 1;
        public void Connection(string connectionstring)//Establishing Connection With the right credentials
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                while (tries <= 3)
                {
                    Console.Write("Enter Username:");
                    string username = Console.ReadLine();

                    Console.Write("\nEnter Password:");
                    string password = ReadPassword();

                    //hash and save password
                    // string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                    // check a password
                    // bool validPassword = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

                    string hashedPassword = " ";

                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand($"SELECT password FROM users WHERE username=@username", conn))//Query for password to be hashed
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        SqlDataReader data = cmd.ExecuteReader();
                        while (data.Read())
                        {
                            hashedPassword = data[0].ToString();
                        }
                    }
                    conn.Close();
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand($"SELECT * FROM users WHERE username = @username AND password=@password ", conn))//Query to check data validation
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);
                        try
                        {
                            int id = (int)cmd.ExecuteScalar();
                            conn.Close();
                            bool validPassword = BCrypt.Net.BCrypt.Verify(password, hashedPassword);//Validation of HashedPassword
                            if (id > 0 & validPassword == true)
                            {
                                Console.WriteLine("\nSuccessful Login.\n");
                                Console.WriteLine("Press any Key to Continue.");
                                Console.ReadKey();
                                Console.Clear();

                                AppMenu menuselector = new AppMenu();
                                menuselector.Menu(username);
                            }
                            else
                            {
                                IncorrectPassword(connectionstring);
                            }
                        }
                        catch
                        {
                            IncorrectPassword(connectionstring);                            
                        }
                    }
                }                                                  
            }
        }
        private void IncorrectPassword(string connectionstring)//Incorrect Password tries
        {
            Console.WriteLine("\nIncorrect Username or Password.\n");
            Console.WriteLine($"{3 - tries} remaining Login Attempts.\n");
            Console.WriteLine("Press any Key to Continue.");
            Console.ReadKey();
            Console.Clear();
            if (tries == 3)
            {
                Console.WriteLine("Exiting the Application...");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
            tries++;
            Connection(connectionstring);

        }
        private static string ReadPassword()//Encryption of pass on the console
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }
    }
    
}







