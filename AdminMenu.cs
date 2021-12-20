using System.IO;
using System.Collections.Generic;
using System;

namespace GitPay
{
    public class AdminMenu : Menu
    {
        Admin adm;
        public AdminMenu(User u, List<User> l, int currIndex)
        {
            ID = currIndex++;
            adm = new Admin(u);
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
            System.Console.WriteLine();
            System.Console.WriteLine("--- --- --- --- --- - Account  Management - --- --- --- --- --- ");
            System.Console.WriteLine();
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
                    AddUser(users);
                    break;
                case "2":
                    Console.Clear();
                    adm.EditUser(users);
                    break;
                case "3":
                    Console.Clear();
                    adm.DeleteUser(users);
                    break;
                case "4":
                    Console.Clear();
                    adm.ViewAllUsers(users);
                    break;
                case "5":
                    Console.Clear();
                    adm.EditPassword(users, adm);
                    break;
                case "6":
                    Console.Clear();
                    adm.EditInformation(users, adm);
                    break;
                case "7":
                    Console.Clear();
                    adm.PrintSalary();
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

        public void AddUser(List<User> users)
        {
            System.Console.WriteLine("--- --- --- --- --- --- --- ---- --- --- --- --- --- --- --- ---");
            Console.Write("Enter the username          : ");
            string username = Console.ReadLine();

            Console.Write("Enter the password          : ");
            string password = Console.ReadLine();

            Console.Write("Enter the user's first name : ");
            string firstName = Console.ReadLine();

            Console.Write("Enter the user's last name  : ");
            string lastName = Console.ReadLine();

            string occupation = adm.GetOpt();

            ID++;

            switch (occupation)
            {
                case "Admin":
                    users.Add(new Admin(ID, username, "0", firstName, lastName, occupation, 0, 0)); break;
                case "Accountant":
                    users.Add(new Accountant(ID, username, "0", firstName, lastName, occupation, 0, 0)); break;
                case "Employee":
                    users.Add(new Admin(ID, username, "0", firstName, lastName, occupation, 0, 0)); break;
                case "Manager":
                    users.Add(new Manager(ID, username, "0", firstName, lastName, occupation, 0, 0)); break;
                default:
                    users.Add(new Employee(ID, username, "0", firstName, lastName, occupation, 0, 0)); break;
            }
        }
    }
}