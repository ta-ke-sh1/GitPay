using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace Payroll
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
            System.Console.WriteLine("                  2. Search Employee Payroll");
            System.Console.WriteLine("                   3. Edit Employee Payroll");
            System.Console.WriteLine("                      4. Change Base Rate");
            System.Console.WriteLine("                       5. Edit Password");
            System.Console.WriteLine("                      6. Edit Information");
            System.Console.WriteLine("                        7. View Payroll");
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
                    acc.SearchPayroll(users); break;
                case "3":
                    acc.EditPayroll(users); break;
                case "4":
                    acc.ChangeBaseRate(); break;
                case "5":
                    acc.EditPassword(); break;
                case "6":
                    acc.EditInformation(); break;
                case "7":
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

        private void PrintAllPayrolls()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("ID\tFName\tLName\tPos\tWHours\tTotal\tOver\tBonus\tTax\tFinal");
            System.Console.WriteLine("--------------------------------------------------------------------------------");
            foreach (User u in users)
            {
                u.PrintCompactSalary();
            }
        }

        
    }
}