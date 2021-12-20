using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace GitPay
{
    public class ManagerMenu : Menu
    {
        Manager manager;
        public ManagerMenu(User u, List<User> l)
        {
            manager = new Manager(u);
            users = l;
        }
        protected override void printMenu()
        {
            System.Console.WriteLine("--- --- --- --- --- -- Payroll Management -- --- --- --- --- ---");
            System.Console.WriteLine();
            System.Console.WriteLine("                     1. View all Payrolls");
            System.Console.WriteLine("                  2. Search Employee Payroll");
            System.Console.WriteLine("                   3. Edit Employee Payroll");
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
                    manager.SearchPayroll(users); break;
                case "3":
                    manager.EditPayroll(users); break;
                case "4":
                    manager.EditPassword(users, manager); break;
                case "5":
                    manager.EditInformation(users, manager); break;
                case "6":
                    manager.PrintSalary(); break;
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