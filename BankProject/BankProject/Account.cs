using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Text;

namespace BankProject
{
    public class Account
    {
        public string Username { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }

        public Account()
        {

        }

        public Account(string username, DateTime transactiondate, decimal amount)
        {
            Username = username;
            TransactionDate = transactiondate;
            Amount = amount;

        }
        public override string ToString()//Override ToString which is used to format the send file statement
        {
            string username = Username;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("el-gr");
            var transactiondateformat = TransactionDate.ToString("yyyy-MM-dd HH:mm:ss.FFF", CultureInfo.InvariantCulture);
            var amount = Amount.ToString("C2", CultureInfo.CurrentCulture);

            return ($"Your transaction was successful {username}, " +
                $"it took place on {transactiondateformat} and your current BALANCE is: {amount}");
        }
    }
}
