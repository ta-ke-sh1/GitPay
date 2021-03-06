using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace GitPay
{
    public class AccountantMenu : Menu
    {
        Accountant acc;
        public AccountantMenu(User u, List<User> l)
        {
            acc = new Accountant(u);
            users = l;
        }

        protected override void printMenu()
        {
            System.Console.WriteLine("--- --- --- --- --- -- Payroll Management -- --- --- --- --- ---");
            System.Console.WriteLine();
            System.Console.WriteLine("                     1. View all Payrolls");
            System.Console.WriteLine("                      2. View Base Rates");
            System.Console.WriteLine("                     3. Change Base Rates");
            System.Console.WriteLine();
            System.Console.WriteLine("--- --- --- --- --- ---- Account Management ---- --- --- --- ---");
            System.Console.WriteLine();
            System.Console.WriteLine("                       4. Edit Password");
            System.Console.WriteLine("                      5. Edit Information");
            System.Console.WriteLine("                        6. View Payroll");
            System.Console.WriteLine("                          0. Log Out");
            System.Console.WriteLine();
            System.Console.WriteLine("--- --- --- --- --- --- --- ---- --- --- --- --- --- --- --- ---");
        }

        protected override void doTask(string choice)
        {
            switch (choice)
            {
                case "1":
                    PrintAllPayrolls(); break;
                case "2":
                    acc.ViewBaseRate(); break;
                case "3":
                    acc.ChangeBaseRate(); break;
                case "4":
                    acc.EditPassword(users, acc); break;
                case "5":
                    acc.EditInformation(users, acc); break;
                case "6":
                    acc.PrintSalary(); break;
                case "0":
                    OverrideTxt(users);
                    Console.Clear(); break;
                default:
                    Console.Clear();
                    System.Console.WriteLine("--- --- --- --- --- --- --- ---- --- --- --- --- --- --- --- ---");
                    System.Console.WriteLine("                       Invalid choice!");
                    System.Console.WriteLine("--- --- --- --- --- --- --- ---- --- --- --- --- --- --- --- ---");
                    break;
            }
        }
    }
}