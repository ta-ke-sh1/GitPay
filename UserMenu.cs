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
                    EditInformation();
                    break;
                case "2":
                    Console.Clear();
                    EditPassword();
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

        private void EditPassword(){
            System.Console.WriteLine("Enter old password: ");
            string pass = Console.ReadLine();
            if(session_user.Login(session_user.Username,pass)){
                System.Console.WriteLine("Enter new password: ");
                string newPassword = Console.ReadLine();
                session_user.Password = newPassword;
                System.Console.WriteLine("Password changed!");
            }
            else{
                System.Console.WriteLine("Wrong password!");
            }
        }

        private void EditInformation()
        {
            System.Console.WriteLine("Enter old password: ");
            string pass = Console.ReadLine();
            if (session_user.Login(session_user.Username, pass))
            {
                System.Console.WriteLine("Enter your First Name: ");
                session_user.FirstName = Console.ReadLine();
                System.Console.WriteLine("Enter your Last Name: ");
                session_user.LastName = Console.ReadLine();
                System.Console.WriteLine("User information changed!");
            }
            else
            {
                System.Console.WriteLine("Wrong password!");
            }
        }
    }
}