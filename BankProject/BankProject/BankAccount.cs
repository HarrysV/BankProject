using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Threading;

namespace BankProject
{
    public class BankAccount
    {
        public static List<Account> filelist = new List<Account>();
        private decimal amount;
        //View one Account Method(admin)
        public void ViewOneAccount(string connectionstring, string username)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"SELECT username, transaction_date, amount " +
                    $"FROM accounts INNER JOIN users ON users.id = accounts.user_id WHERE username=@username", conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    SqlDataReader myData = cmd.ExecuteReader();
                    while (myData.Read())
                    {
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("el-gr");
                        Console.OutputEncoding = Encoding.UTF8;
                        Console.WriteLine($"Hello {myData[0].ToString()} your last transaction date was on: " +
                            $"{myData[1].ToString()} and your current balance is: {myData[2].ToString()}€");
                    }
                }
            }
        }
        //View All Accounts Method(admin)
        public void ViewAllAccounts(string connectionstring, string username)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"SELECT username, transaction_date, amount " +
                    $"FROM accounts INNER JOIN users ON users.id = accounts.user_id WHERE username!=@username", conn))
                {

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("el-gr"); ;
                    Console.OutputEncoding = Encoding.UTF8;
                    cmd.Parameters.AddWithValue("@username", username);

                    SqlDataReader myData = cmd.ExecuteReader();
                    while (myData.Read())
                    {
                        for (int i = 0; i < myData.FieldCount; i++)
                        {
                            if (i != 2)
                            {
                                Console.Write($"{myData[i].ToString()}\t");
                            }
                            else
                            {
                                Console.Write($"{myData[i].ToString()}€\t");
                            }
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
        //Deposit Method(all users)
        public void DepositToAcc(string connectionstring, string username, string recipient, decimal deposit)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand($"SELECT amount FROM accounts INNER JOIN users " +
                    $"ON users.id = accounts.user_id WHERE username=@username", conn))
                {
                    command.Parameters.AddWithValue("@username", username);

                    SqlDataReader data = command.ExecuteReader();

                    while (data.Read())
                    {
                        amount = data.GetDecimal(0);
                    }
                }
                conn.Close();

                if (deposit <= amount && recipient != username)
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand($"UPDATE accounts SET amount=amount-@deposit " +
                        $"WHERE user_id= (select id from users where username=@username)", conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@deposit", deposit);
                        cmd.ExecuteNonQuery();

                    }
                    using (SqlCommand cmd = new SqlCommand($"UPDATE accounts SET amount=amount+@deposit " +
                        $"WHERE user_id= (SELECT id FROM users WHERE username=@recipient)", conn))
                    {
                        cmd.Parameters.AddWithValue("@recipient", recipient);
                        cmd.Parameters.AddWithValue("@deposit", deposit);
                        cmd.ExecuteNonQuery();
                    }
                    Console.WriteLine("\nSuccessful transaction. Thank you.");
                    amount = amount - deposit;
                    filelist.Add(new Account { Username = username, TransactionDate = DateTime.Now, Amount = amount });

                }
                else
                {
                    Console.WriteLine("Error.We were unable to fulfill the transaction.\n\nPlease try again.");
                }
            }
        }
        //Withdraw Method(admin)
        public void WithdrawFromMember(string connectionstring, string username, string withdrawer, decimal withdraw)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand($"SELECT amount FROM accounts INNER JOIN users " +
                    $"ON users.id = accounts.user_id WHERE username=@withdrawer", conn))
                {
                    command.Parameters.AddWithValue("@withdrawer", withdrawer);

                    SqlDataReader data = command.ExecuteReader();

                    while (data.Read())
                    {
                        amount = data.GetDecimal(0);
                    }
                }
                conn.Close();

                if (withdraw <= amount && withdrawer != username)
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand($"UPDATE accounts SET amount=amount-@withdraw " +
                        $"WHERE user_id= (select id from users where username=@withdrawer)", conn))
                    {
                        cmd.Parameters.AddWithValue("@withdrawer", withdrawer);
                        cmd.Parameters.AddWithValue("@withdraw", withdraw);
                        cmd.ExecuteNonQuery();

                    }
                    using (SqlCommand cmd = new SqlCommand($"UPDATE accounts SET amount=amount+@withdraw " +
                        $"WHERE user_id= (SELECT id FROM users WHERE username=@username)", conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@withdraw", withdraw);
                        cmd.ExecuteNonQuery();
                    }
                    Console.WriteLine("\nSuccessful transaction. Thank you.");
                    using (SqlCommand command = new SqlCommand($"SELECT amount FROM accounts INNER JOIN users " +
                        $"ON users.id = accounts.user_id WHERE username=@username", conn))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        SqlDataReader data = command.ExecuteReader();

                        while (data.Read())
                        {
                            amount = data.GetDecimal(0);
                        }
                    }
                    conn.Close();

                    amount = amount + withdraw;
                    filelist.Add(new Account { Username = username, TransactionDate = DateTime.Now, Amount = amount });
                }
                else
                {
                    Console.WriteLine("Error.We were unable to fulfill the transaction.\n\nPlease try again.");
                }
            }
        }
    }
}
