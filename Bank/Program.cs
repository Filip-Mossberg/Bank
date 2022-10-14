﻿using System;
using System.Text;

namespace Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[][] MoneyAccount = new double[5][];
            MoneyAccount[0] = new double[3] { 403.25, 23400.83, 102420.63 };
            MoneyAccount[1] = new double[2] { 1394.34, 43742.04 };
            MoneyAccount[2] = new double[1] { 3493.52 };
            MoneyAccount[3] = new double[3] { 1904.78, 18304.94, 74323.20 };
            MoneyAccount[4] = new double[2] { 1452.86, 173744.14 };

            String[,] accounts = new string[5, 2];
            accounts[0, 0] = "filip"; accounts[0, 1] = "2124";
            accounts[1, 0] = "noah"; accounts[1, 1] = "4452";
            accounts[2, 0] = "bella"; accounts[2, 1] = "9832";
            accounts[3, 0] = "joakim"; accounts[3, 1] = "2532";
            accounts[4, 0] = "ulrika"; accounts[4, 1] = "2435";

            int userid = -1;
            Loggedin(accounts, MoneyAccount, userid);
        }
        public static void Loggedin(String[,] accounts, double[][] MoneyAccount, int userid)
        {
            while (userid < 0)
            {
                userid = Login(accounts);
            }
            int pick = Menu();

            switch (pick)
            {
                case 1:
                    CheckAccounts(MoneyAccount, accounts, userid);
                    break;
                case 2:
                    Console.WriteLine("Hello!");
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }

        }
        public static int Login(String[,] accounts)
        {
            int userid = 0;
            Console.WriteLine("Welcome To The Bank!\n");

            for (int attempts = 0; attempts < 3; attempts++)
            {
                Console.Write("Username:");
                String username = Console.ReadLine().ToLower();
                Console.Write("Password:");
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
                if (attempts == 2)
                {
                    Console.WriteLine("To Many Attempts!");
                    Environment.Exit(0);
                }
            }
            return userid;
        }
        public static int Menu()
        {
            Console.Clear();
            String[] menuitems = { "1. See your accounts and balance", "2. Transfer between accounts", "3. Withdraw money", "4. Log out\n" };
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
                        Console.WriteLine("Invalid Menu Pick!\n");
                    }
            }
            return pick;
        }
        public static void CheckAccounts(double[][] MoneyAccount, String[,] accounts, int userid)
        {
            Console.Clear();
            for (int WriteAccounts = 0; WriteAccounts < MoneyAccount[userid].Length; WriteAccounts++)
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
            Return(accounts, MoneyAccount, userid);
        }
        public static void Return(String[,] accounts, double[][] MoneyAccount, int userid)
        {
            Console.WriteLine("\nPress Any Button To Return To The Menu");
            Console.ReadLine();
            Loggedin(accounts, MoneyAccount, userid);
        }

    }
}
