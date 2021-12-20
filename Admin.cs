using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace GitPay
{
    public class Admin : User
    {
        public Admin(User u) : base(u) { }
        public Admin(int id, string user, string pass, string first, string last, string occ, int work, int over)
        : base(id, user, pass, first, last, occ, work, over) { }
        public override Rates GetRates()
        {
            return new AdminRate();
        }

        public string GetOpt()
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
        public void EditUser(List<User> users)
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
        public void DeleteUser(List<User> users)
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
        public void ViewAllUsers(List<User> users)
        {
            int count = 1;
            System.Console.WriteLine("--- --- --- --- --- --- --- ---- --- --- --- --- --- --- --- ---");
            System.Console.WriteLine("ID \tUser\tFName \tLName \tPos \tWorkT \tOverT ");
            foreach (User u in users)
            {
                u.PrintData();
                count++;
            }
        }
    }
}