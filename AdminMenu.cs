using System.IO;
using System.Collections.Generic;
using System;

namespace Payroll
{
    public class AdminMenu : Menu
    {
        Admin currUser;
        public AdminMenu(User u, List<User> l, int currIndex)
        {
            ID = currIndex++;
            currUser = new Admin(u);
            users = l;
        }
        protected override void printMenu()
        {
            System.Console.WriteLine("--- --- --- --- --- ---- --- Admin --- ---- --- --- --- --- ---");
            System.Console.WriteLine("");
            System.Console.WriteLine("                       1. Add new User");
            System.Console.WriteLine("                        2. Edit User");
            System.Console.WriteLine("                       3. Delete User");
            System.Console.WriteLine("                      4. View all users");
            System.Console.WriteLine("                      5. Edit Password");
            System.Console.WriteLine("                     6. Edit Information");
            System.Console.WriteLine("                       7. View salary");
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
                    currUser.AddUser(users);
                    break;
                case "2":
                    Console.Clear();
                    currUser.EditUser(users);
                    break;
                case "3":
                    Console.Clear();
                    currUser.DeleteUser(users);
                    break;
                case "4":
                    Console.Clear();
                    currUser.ViewAllUsers(users);
                    break;
                case "5":
                    Console.Clear();
                    currUser.EditPassword();
                    break;
                case "6":
                    Console.Clear();
                    currUser.EditInformation();
                    break;
                case "7":
                    Console.Clear();
                    currUser.PrintSalary();
                    break;
                case "0":
                    OverrideTxt(users);
                    Console.Clear();
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