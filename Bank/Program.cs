using System;
using System.Text;

namespace Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Jagged Array with all the money accounts 
            double[][] MoneyAccount = new double[5][];
            MoneyAccount[0] = new double[3] { 403.25, 23400.83, 102420.63 };
            MoneyAccount[1] = new double[2] { 1394.34, 43742.04 };
            MoneyAccount[2] = new double[1] { 3493.52 };
            MoneyAccount[3] = new double[3] { 1904.78, 18304.94, 74323.20 };
            MoneyAccount[4] = new double[2] { 1452.86, 173744.14 };

            // 2d Array with all the ser accounts
            String[,] accounts = new string[5, 2];
            accounts[0, 0] = "filip"; accounts[0, 1] = "2124";
            accounts[1, 0] = "noah"; accounts[1, 1] = "4452";
            accounts[2, 0] = "bella"; accounts[2, 1] = "9832";
            accounts[3, 0] = "joakim"; accounts[3, 1] = "2532";
            accounts[4, 0] = "ulrika"; accounts[4, 1] = "2435";

            int userid = -1; // A variable that is used to know who the user is 
            program(accounts, MoneyAccount, userid); 
        }
        public static void program(String[,] accounts, double[][] MoneyAccount, int userid) // Program Method that runts the program
        {
            if (userid < 0)
            {
                userid = Login(accounts); 
            }
            int pick = Menu(accounts, userid); 

            switch (pick) // Switch that takes the input from the menu and does whatever the user decides
            {
                case 1:
                    CheckAccounts(MoneyAccount, accounts, userid); 
                    Return(accounts, MoneyAccount, userid);
                    break;
                case 2:
                    TransferAndWithdraw(accounts, MoneyAccount, userid, 1);
                    CheckAccounts(MoneyAccount, accounts, userid);
                    Return(accounts, MoneyAccount, userid);
                    break;
                case 3:
                    TransferAndWithdraw(accounts, MoneyAccount, userid, 2);
                    CheckAccounts(MoneyAccount, accounts, userid);
                    Return(accounts, MoneyAccount, userid);
                    break;
                case 4:
                    Console.Clear();
                    program(accounts, MoneyAccount, -1);
                    break;
            }
        }
        public static int Login(String[,] accounts) // User Login Method that checks the user input againt the accounts array if there is a match
        {
            int userid = 0;
            Console.WriteLine("Welcome To The Bank!\n");

            for (int attempts = 0; attempts < 3; attempts++)
            {
                Console.Write("Username:");
                String username = Console.ReadLine().ToLower();
                Console.Write("Pin-Code:");
                String pincode = Console.ReadLine();

                for (int finduser = 0; finduser < 5; finduser++)
                {
                    if (username == accounts[finduser, 0] && pincode == accounts[finduser, 1])
                    {
                        userid = finduser;

                        attempts = 10;
                        finduser = 10;
                    }
                    else if (finduser == 4)
                    {
                        Console.Clear();
                        Console.WriteLine("Wrong Username Or Pin-Code!\n");
                    }
                }
                if (attempts == 2) // If the attempts = 3 then the user is thrown out
                {
                    Console.WriteLine("To Many Attempts!");
                    Environment.Exit(0);
                }
            }
            return userid;
        }
        public static int Menu(String[,] accounts, int userid) // Lists the menu list and lets the user pick a option from the menu
        {
            Console.Clear();
            String[] menuitems = { "1. See your accounts and balance", "2. Transfer between accounts", "3. Withdraw money", "4. Log out\n" };
            Console.WriteLine("Welcome {0}!\n", accounts[userid, 0]);
            foreach (String list in menuitems) Console.WriteLine(list);
            int pick = 0;

            for (int attempts = 0; attempts < 3; attempts++)
            {
                    var menupick = Console.ReadLine();
                    bool check = int.TryParse(menupick, out pick);
                    if (check == true && pick < 5 && pick > 0)
                    {
                        attempts = 10;
                    }
                    else if (attempts == 2)
                    {
                        Console.WriteLine("To Many Attempts!");
                        Environment.Exit(0);
                    }
                    else 
                    {
                        Console.WriteLine("Invalid Menu Pick!\n", attempts);
                    }
            }
            return pick;
        }
        public static void CheckAccounts(double[][] MoneyAccount, String[,] accounts, int userid) // Writes out the amount of accounts the user has with the correct currencys 
        {
            Console.Clear();
            for (int WriteAccounts = 0; WriteAccounts < MoneyAccount[userid].Length; WriteAccounts++) // MoneyAccount[userid].Length is used to check how many accounts the user have
            {
                switch (WriteAccounts)
                {

                    case 0:
                        StringBuilder Type1 = new StringBuilder("[1] Payroll account: ");
                        Type1.AppendFormat("{0:C}", MoneyAccount[userid][0]);
                        Console.WriteLine(Type1.ToString());
                        break;
                    case 1:
                        StringBuilder Type2 = new StringBuilder("[2] Share account: ");
                        Type2.AppendFormat("{0:C}", MoneyAccount[userid][1]);
                        Console.WriteLine(Type2.ToString());
                        break;
                    case 2:
                        StringBuilder Type3 = new StringBuilder("[3] Savings account: ");
                        Type3.AppendFormat("{0:C}", MoneyAccount[userid][2]);
                        Console.WriteLine(Type3.ToString());
                        break;
                }
            }
        }
        public static void Return(String[,] accounts, double[][] MoneyAccount, int userid) // Returns the user back to the program method
        {
            Console.WriteLine("\nPress Any Button To Return To The Menu");
            Console.ReadLine();
            program(accounts, MoneyAccount, userid);
        }
        public static void TransferAndWithdraw(String[,] accounts, double[][] MoneyAccount, int userid, int Decider) // List of methods that together does the Transfer and Withdraw functions
        {
            CheckAccounts(MoneyAccount, accounts, userid);
            (int From, int To) = FromTo(accounts, MoneyAccount, userid, Decider);
            double amount = Amount(MoneyAccount, userid, From);
            Message(amount, To, From, Decider);
            PinCode(accounts, userid);
            String[] Save = { From.ToString(), To.ToString(), amount.ToString(), userid.ToString() }; // String array that saves important values so that its easier to send to the next method
            UpdateValues(MoneyAccount, Save, Decider); 
        }
        public static (int,int) FromTo(String[,] accounts, double[][] MoneyAccount, int userid, int Decider) // Takes the user input from account and to account
        {
            int From = 0;
            int To = 0;

            for (int FromToAmount = 0; FromToAmount < 2; FromToAmount++)
            {
                for (int attempts = 0; attempts < 3; attempts++)
                {
                    String[] Type = { "\nTransfer From:", "\nTransfer To:" };
                    Console.Write(Type[FromToAmount]);
                    var FromTo = Console.ReadLine();
                    bool check = int.TryParse(FromTo, out int pick);

                    if (check == true && pick <= MoneyAccount[userid].Length && pick > 0)
                    {
                        switch (FromToAmount)
                        {
                            case 0:
                                From = pick;
                                attempts = 10;
                                if (Decider == 2) // Decider is used to check if the user want to Transfer money or Withdraw money
                                {
                                    FromToAmount = 10;
                                }
                                break;
                            case 1:
                                if (pick != From)
                                {
                                    To = pick;
                                    attempts = 10;
                                }
                                else
                                {
                                    if (attempts == 2)
                                    {
                                        Console.WriteLine("To Many Attempts!");
                                        Environment.Exit(0);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Account Pick!", attempts);
                                    }
                                }
                                break;
                        }
                    }
                    else if (attempts == 2)
                    {
                        Console.WriteLine("To Many Attempts!");
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Account Pick!", attempts);
                    }
                }
            }
            return (From, To);
        }
        public static double Amount(double[][] MoneyAccount, int userid, int From) // Takes the user input amount to see what amout is getting Transfered or Withdrawn
        {
            double Amount = 0;

            for (int attempts = 0; attempts < 3; attempts++)
            {
                Console.Write("\nAmount:");
                var AmmountPick = Console.ReadLine();
                bool check = double.TryParse(AmmountPick, out Amount);
                if (check == true && Amount < MoneyAccount[userid][From - 1] && Amount > 0)
                {
                    Math.Round((Amount), 2);
                    attempts = 10;
                }
                else if (attempts == 2)
                {
                    Console.WriteLine("To Many Attempts!");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Invalid Amount!", attempts);
                }
            }
            return Amount;
        }
        public static void Message(double Amount, int To, int From, int Decider) // Messages that says what is about to happen based on the users erlier inputs
        {
            Console.Clear();
            if (Decider == 1)
            {
                StringBuilder TransferMessage = new StringBuilder("You Will Transfer ");
                TransferMessage.AppendFormat("{0:C}", Amount);
                TransferMessage.AppendFormat(" To Account {0}", To);
                Console.WriteLine(TransferMessage.ToString());
            }
            else
            {
                StringBuilder WithdrawMessage = new StringBuilder("You Will Withdraw ");
                WithdrawMessage.AppendFormat("{0:C}", Amount);
                WithdrawMessage.AppendFormat(" From Account {0}", From);
                Console.WriteLine(WithdrawMessage.ToString());
            }
        }
        public static void PinCode(String[,] accounts, int userid) // Method that checks if the user Pin-Code is correct before making the Transfer or Withdrawn action
        {
            for (int attempts = 0; attempts < 3; attempts++)
            {
                Console.WriteLine("Confirm By Entering Your Pin-Code:");
                var PinCode = Console.ReadLine();
                bool check = int.TryParse(PinCode, out int Pin);
                if (check == true && Pin == int.Parse(accounts[userid, 1]))
                {
                    attempts = 10;
                }
                else if (attempts == 2)
                {
                    Console.WriteLine("To Many Attempts!");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Invalid Pin-Code!", attempts);
                }
            }
        }
        public static void UpdateValues(double[][] MoneyAccount, string[] save, int Decider) // Updates the values 
        {
            int From = int.Parse(save[0]);
            double Amount = double.Parse(save[2]);
            int userid = int.Parse(save[3]);

            if (Decider == 1)
            {
                int To = int.Parse(save[1]);
                MoneyAccount[userid][From - 1] = MoneyAccount[userid][From - 1] - Amount;
                MoneyAccount[userid][To - 1] = MoneyAccount[userid][To - 1] + Amount;
            }
            else
            {
                MoneyAccount[userid][From - 1] = MoneyAccount[userid][From - 1] - Amount;
                Console.WriteLine(MoneyAccount[From - 1]);
            }
        }
    }
}
