using System.Collections.Generic;
using System;

namespace Payroll
{
    public class UserMenu : Menu
    {
        public UserMenu(User u, List<User> listUsers)
        {
            session_user = u;
            users = listUsers;
        }
        protected override void printMenu()
        {
            System.Console.WriteLine("--- --- --- --- --- --- -- User Menu -- --- --- --- --- --- ---");
            System.Console.WriteLine();
            System.Console.WriteLine("                1. Edit personal infomation");
            System.Console.WriteLine("                     2. Edit Password");
            System.Console.WriteLine("                       3. View Payroll");
            System.Console.WriteLine("                         0. Log Out");
            System.Console.WriteLine();
            System.Console.WriteLine("--- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---");
        }
        protected override void doTask(string choice)
        {
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    session_user.EditInformation();
                    break;
                case "2":
                    Console.Clear();
                    session_user.EditPassword();
                    break;
                case "3":
                    Console.Clear();
                    session_user.PrintSalary();
                    break;
                case "0":
                    Console.Clear();
                    OverrideTxt(users);
                    break;
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