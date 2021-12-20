using System.IO;
using System.Collections.Generic;
using System;
using System.Text;


namespace GitPay
{
    public abstract class Menu
    {
        public int ID;
        public string[] userData;
        public List<User> users;
        public User session_user;
        public Menu()
        {
            DataLoad();
        }
        public void Run()
        {
            Console.Clear();
            while (true)
            {
                printMenu();
                System.Console.Write("Enter an option: ");
                string choice = Console.ReadLine();
                doTask(choice);

                if (choice == "0")
                {
                    break;
                }
            }
        }
        public void DataLoad()
        {
            users = new List<User>();
            userData = File.ReadAllLines("Data/Users/users.txt", Encoding.UTF8);
            for (int i = 0; i < userData.Length; i++)
            {

                if (userData[i] == "" || userData[i] == " ")
                {
                    continue;
                }

                string[] data = userData[i].Split(';');
                switch (data[5])
                {
                    case "Admin":
                        session_user = new Admin(Convert.ToInt32(data[0]), data[1], data[2], data[3], data[4], data[5], Convert.ToInt32(data[6]), Convert.ToInt32(data[7]));
                        break;
                    case "Manager":
                        session_user = new Manager(Convert.ToInt32(data[0]), data[1], data[2], data[3], data[4], data[5], Convert.ToInt32(data[6]), Convert.ToInt32(data[7]));
                        break;
                    case "Accountant":
                        session_user = new Accountant(Convert.ToInt32(data[0]), data[1], data[2], data[3], data[4], data[5], Convert.ToInt32(data[6]), Convert.ToInt32(data[7]));
                        break;
                    case "Employee":
                        session_user = new Employee(Convert.ToInt32(data[0]), data[1], data[2], data[3], data[4], data[5], Convert.ToInt32(data[6]), Convert.ToInt32(data[7]));
                        break;
                    default:
                        break;
                }
                users.Add(session_user);
                ID = Convert.ToInt32(data[0]);
            }
        }
        protected void OverrideTxt(List<User> users)
        {
            List<String> overrideData = new List<string>();
            foreach (User u in users)
            {
                overrideData.Add(u.ToString());
            }
            File.WriteAllLines("Data/Users/users.txt", overrideData);
        }
        public bool LengthCheck(string source, int limit)
        {
            if (source.Length <= limit) return true;
            else return false;
        }
        public bool numericCheck(string source)
        {
            return true;
        }
        protected void PrintAllPayrolls()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("ID\tFName\tLName\tPos\tWHours\tTotal\tOver\tBonus\tTax\tFinal");
            System.Console.WriteLine("--------------------------------------------------------------------------------");
            foreach (User u in users)
            {
                u.PrintCompactSalary();
            }
        }

        protected abstract void printMenu();
        protected abstract void doTask(string choice);
    }
}