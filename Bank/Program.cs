using System;

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

            Login(accounts);
            menu();
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
                }
            }
            return userid;
        }
        public static void menu()
        {
            Console.Clear();
            String[] menuitems = { "1. See your accounts and balance", "2. Transfer between accounts", "3. Withdraw money", "4. Log out\n" };
            foreach (string list in menuitems) Console.WriteLine(list);
        }
    }
}
