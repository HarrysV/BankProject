using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BankProject
{
    public class AppMenu
    {
        public void Menu(string username)//Console App Menu for Members and Admin
        {
            if (username == "admin")
            {
                Console.Write("1.View Cooperative's Internal Bank Account.\n2.View Members' Bank Accounts." +
                    "\n3.Deposit to Member's Bank Account.\n4.Withdraw from Member's Bank Account." +
                    "\n5.Send Today's Transactions Statement\n6.Exit the Application." +
                    "\nPlease pick a Number from 1 to 6 to start your Transaction:");
                string choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Cooperative Internal Bank Account.\n");
                        BankAccount bac = new BankAccount();//Initialize the DbAccess and the Bank Account menu method
                        DbAccess db = new DbAccess();
                        bac.ViewOneAccount(db.ConnectionString, username);
                        break;
                    case "2":
                        Console.WriteLine("Members' Bank Accounts.\n");
                        BankAccount bac2 = new BankAccount();
                        DbAccess db2 = new DbAccess();
                        bac2.ViewAllAccounts(db2.ConnectionString, username);
                        break;
                    case "3":
                        Console.WriteLine("Deposit to Member's Bank Account.\n");                        
                        try
                        {
                            Console.WriteLine("\nPlease specify the User you want to Deposit to: ");//Input the user to deposit to
                            string recipient = Console.ReadLine();
                            Console.WriteLine("\nPlease specify the Amount to Deposit:");//The user inputs deposit amount
                            decimal deposit = decimal.Parse(Console.ReadLine());
                            if ((recipient == "admin" || recipient == "user1" || recipient == "user2") && deposit > 0)
                            {
                                BankAccount bac3 = new BankAccount();
                                DbAccess db3 = new DbAccess();
                                bac3.DepositToAcc(db3.ConnectionString, username, recipient, deposit);
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect Input Data.\n\nPlease Try Again.");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("\nIncorrect Input Data.\n\nPlease Try Again.");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Withdraw from Member's Account.\n");
                        try
                        {
                            Console.WriteLine("\nPlease specify the User you want to Withdraw from: ");                        
                            string withdrawer = Console.ReadLine();
                            Console.WriteLine("\nPlease specify the Amount to Withdraw: ");
                            decimal withdraw = decimal.Parse(Console.ReadLine());
                            if ((withdrawer == "user1" || withdrawer == "user2") && withdraw > 0)
                            {
                                BankAccount bac4 = new BankAccount();
                                DbAccess db4 = new DbAccess();
                                bac4.WithdrawFromMember(db4.ConnectionString, username, withdrawer, withdraw);
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect Input Data.\n\nPlease Try Again.");
                            }
                        }
                        catch 
                        {
                            Console.WriteLine("\nIncorrect Input Data.\n\nPlease Try Again.");
                        }
                        break;
                    case "5":
                        Console.WriteLine("Sending Today's Statement.\n");
                        FileAccess fileAccess = new FileAccess();
                        fileAccess.SendStatement(BankAccount.filelist, username);
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                        break;
                    case "6":
                        Console.WriteLine("Exiting the Application.");
                        Thread.Sleep(2000);                        
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("You picked an Invalid Transaction.");
                        break;
                }
                Console.WriteLine("\nPress any Key to Continue.");
                Console.ReadKey();
                Console.Clear();
                Menu(username);
            }
            else
            {
                Console.Write("1.View Bank Account.\n2.Deposit to Cooperative's Internal Bank Account." +
                    "\n3.Deposit to another Member's Bank Account.\n4.Send Today's Transactions Statement" +
                    "\n5.Exit the Application\nPlease pick a Number from 1 to 5 to start your Transaction:");
                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Member's Bank Account.\n");
                        BankAccount bac = new BankAccount();//Initialize the DbAccess and the Bank Account menu method
                        DbAccess db = new DbAccess();
                        bac.ViewOneAccount(db.ConnectionString, username);
                        break;
                    case "2":
                        Console.WriteLine("Deposit to Cooperative Bank Account.\n");
                        try
                        {
                            Console.WriteLine("Please specify the User you want to Deposit to: ");//Input the user to deposit to
                            string recipient = Console.ReadLine();
                            Console.WriteLine("Please specify the Amount to Deposit:");//The user inputs deposit amount
                            decimal deposit = decimal.Parse(Console.ReadLine());
                            if (recipient == "admin" && deposit > 0)
                            {
                                BankAccount bac2 = new BankAccount();
                                DbAccess db2 = new DbAccess();
                                bac2.DepositToAcc(db2.ConnectionString, username, recipient, deposit);
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect Input Data.\n\nPlease Try Again.");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("\nIncorrect Input Data.\n\nPlease Try Again.");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Deposit to another Member's Bank Account.\n");
                        try
                        {
                            Console.WriteLine("\nPlease specify the User you want to Deposit to: ");//Input the user to deposit to
                            string recipient = Console.ReadLine();
                            Console.WriteLine("\nPlease specify the Amount to Deposit:");//The user inputs deposit amount
                            decimal deposit = decimal.Parse(Console.ReadLine());
                            if ((recipient == "user1" || recipient == "user2") && deposit > 0)
                            {
                                BankAccount bac3 = new BankAccount();
                                DbAccess db3 = new DbAccess();
                                bac3.DepositToAcc(db3.ConnectionString, username, recipient, deposit); 
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect Input Data.\n\nPlease Try Again.");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("\nIncorrect Input Data.\n\nPlease Try Again.");                            
                        }
                        break;
                    case "4":
                        Console.WriteLine("Sending Today's Statement.\n");
                        FileAccess fileaccess = new FileAccess();
                        fileaccess.SendStatement(BankAccount.filelist, username);
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                        break;
                    case "5":
                        Console.WriteLine("Exiting the Application.");
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("You picked an Invalid Transaction.");
                        break;
                }
                Console.WriteLine("\nPress any Key to Continue.");
                Console.ReadKey();
                Console.Clear();
                Menu(username);
            }
        }
    }
}
