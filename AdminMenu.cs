using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace Payroll
{
    public class AdminMenu : Menu
    {
        public AdminMenu(User u, List<User> l, int currIndex)
        {
            ID = currIndex++;
            session_user = u;
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
            System.Console.WriteLine("                       5. View salary");
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
                    AddUser();
                    break;
                case "2":
                    Console.Clear();
                    EditUser();
                    break;
                case "3":
                    Console.Clear();
                    DeleteUser();
                    break;
                case "4":
                    Console.Clear();
                    ViewAllUsers();
                    break;
                case "5":
                    Console.Clear();
                    session_user.PrintSalary();
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
        private void AddUser()
        {
            Console.Write("Enter the username          : ");
            string username = Console.ReadLine();

            Console.Write("Enter the password          : ");
            string password = Console.ReadLine();

            Console.Write("Enter the user's first name : ");
            string firstName = Console.ReadLine();

            Console.Write("Enter the user's last name  : ");
            string lastName = Console.ReadLine();

            string occupation = GetOpt();
            
            ID++;

            switch (occupation)
            {
                case "Admin":
                    users.Add(new Admin(ID, username, "0", firstName, lastName, occupation, 0, 0));
                    break;
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

        private string GetOpt()
        {
            System.Console.WriteLine("Please select an occupation ");
            System.Console.WriteLine("1. Admin");
            System.Console.WriteLine("2. Accountant");
            System.Console.WriteLine("3. Manager");
            System.Console.WriteLine("4. Employee");
            System.Console.Write("Your choice: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    return "Admin";
                case "2":
                    return "Accountant";
                case "3":
                    return "Manager";
                case "4":
                    return "Employee";
                default:
                    System.Console.WriteLine("Invalid choice! Setted to Employee");
                    return "Employee";
            }
        }
        private void EditUser()
        {
            System.Console.Write("Enter an ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            foreach (User u in users)
            {
                if (u.ID == id)
                {
                    Console.Write("Enter the new password      : ");
                    u.Password = Console.ReadLine();

                    Console.Write("Enter the user's first name : ");
                    u.FirstName = Console.ReadLine();

                    Console.Write("Enter the user's last name  : ");
                    u.LastName = Console.ReadLine();

                    u.Occupation = GetOpt();
                    return;
                }
            }
            System.Console.WriteLine("No such user exists with that ID!");
        }
        private void DeleteUser()
        {
            System.Console.Write("Enter an ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            foreach (User u in users)
            {
                if (u.ID == id)
                {
                    users.Remove(u);
                    return;
                }
            }
            System.Console.WriteLine("No such user exists with that ID!");
        }
        private void ViewAllUsers()
        {
            int count = 1;
            System.Console.WriteLine("ID \tUser\tFName \tLName \tPos \tWorkT \tOverT ");
            foreach (User u in users)
            {
                u.PrintData();
                count++;
            }
        }
    }
}