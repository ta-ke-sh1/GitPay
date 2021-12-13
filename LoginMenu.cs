using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace Payroll
{
    public class LoginMenu : Menu
    {
        int count = 6;
        public LoginMenu()
        {
            
        }

        protected override void printMenu()
        {
            System.Console.WriteLine("--- --- --- --- --- -- Payroll Management -- --- --- --- --- ---");
            System.Console.WriteLine("");
            System.Console.WriteLine("                           1. Login");
            System.Console.WriteLine("                           0. Exit");
            System.Console.WriteLine();
            System.Console.WriteLine("--- --- --- --- --- --- --- ---- --- --- --- --- --- --- --- ---");
        }

        protected override void doTask(string choice)
        {
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    if (Login(count))
                    {
                        count = 6;
                        GetMenu(session_user.Occupation);
                    }
                    else
                    {
                        System.Console.WriteLine("Incorrect Credentials! Please re-try!");
                        System.Console.WriteLine("         (" + (count - 1) + ") chances left!");
                        count--;
                    }
                    break;
                case "0":
                    Console.Clear();
                    System.Console.WriteLine("--- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---");
                    System.Console.WriteLine("");
                    System.Console.WriteLine("   ████████                      ██  ██                   ");
                    System.Console.WriteLine("   ██                            ██  ██ ");
                    System.Console.WriteLine("   ██  ▓▓▓▓  ▓▓▓▓▓▓  ██▓▓▓▓  ▓▓▓▓██  ██▓▓▓▓  ▓▓  ██  ▓▓▓▓▓▓");
                    System.Console.WriteLine("   ██    ██  ██  ██  ██  ██  ██  ██  ██  ██  ██  ██  ██  ██");
                    System.Console.WriteLine("   ████████  ██████  ██████  ██████  ██████  ██████  ██████");
                    System.Console.WriteLine("                                                 ██  ██      ");
                    System.Console.WriteLine("   ████████████████████████████████████████  ██████  ██████  ");
                    System.Console.WriteLine("");
                    System.Console.WriteLine("--- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---");
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    System.Console.WriteLine("--- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---");
                    System.Console.WriteLine("                       Invalid choice!");
                    System.Console.WriteLine("--- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---");
                    break;
            }
        }

        public bool Login(int count)
        {
            if (count > 0)
            {
                System.Console.WriteLine("--- --- --- --- --- --- ---- Login ---- --- --- --- --- --- ---");
                System.Console.WriteLine();
                System.Console.Write("                  Enter Username: ");
                string username = Console.ReadLine();
                System.Console.WriteLine();
                System.Console.Write("                  Enter Password: ");
                string password = Console.ReadLine();
                foreach (User u in users)
                {
                    if (u.Login(username, password))
                    {
                        session_user = u;
                        return true;
                    }
                }
                return false;
            }
            else
            {
                System.Console.WriteLine("Number of tries exceed! The program will now exit!");
                return false;
            }
        }

        public void GetMenu(string role)
        {
            switch (role)
            {
                case "Admin":
                    Console.Clear();
                    AdminMenu adminMenu = new AdminMenu(session_user, users, ID);
                    adminMenu.Run();
                    break;
                case "Accountant":
                    Console.Clear();
                    AccountantMenu accountantMenu = new AccountantMenu(session_user, users);
                    accountantMenu.Run();
                    break;
                case "Manager":
                    Console.Clear();
                    UserMenu managerMenu = new UserMenu(session_user, users);
                    managerMenu.Run();
                    break;
                case "Employee":
                    Console.Clear();
                    UserMenu userMenu = new UserMenu(session_user, users);
                    userMenu.Run();
                    break;
                default:
                    System.Console.WriteLine("Invalid User type!");
                    break;
            }
        }
    }
}