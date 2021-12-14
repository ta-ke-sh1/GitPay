using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace Payroll
{
    public abstract class User
    {
        private const int BASE_RATE = 1000;
        public int ID { get; set; }
        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (value.Length > 6)
                {
                    System.Console.WriteLine("Password cannot be longer than 6 characters, set as the first 6 characters instead!");
                    password = limitChar(value, 6);
                }
                else
                {
                    password = value;
                }
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Occupation { get; set; }
        private int wHours;
        public int Workhours
        {
            get { return wHours; }
            set
            {
                if (wHours > 160)
                {
                    overtime += (value - 160);
                    wHours = 160;
                }
                else
                {
                    if (value + Overtime >= 160)
                    {
                        wHours = 160;
                        overtime = value + Overtime - 160;
                    }
                    else
                    {
                        wHours = value + Overtime;
                        overtime = 0;
                    }
                }
            }
        }
        public int Overtime
        {
            get { return overtime; }
            set
            {
                if (Workhours == 160)
                {
                    overtime = value;
                }
                else
                {
                    overtime = Workhours + value - 160;
                    wHours = 160;
                }
            }
        }
        private int overtime;
        public Rates rates;
        public User(User u)
        {
            ID = u.ID;
            Username = u.Username;
            Password = u.Password;
            FirstName = u.FirstName;
            LastName = u.LastName;
            Occupation = u.Occupation;
            Workhours = u.Workhours;
            Overtime = u.Overtime;
            rates = u.GetRates();
        }
        public User(int id, string user, string pass, string first, string last, string occ, int work, int over)
        {
            ID = id;
            Username = user;
            Password = pass;
            FirstName = first;
            LastName = last;
            Occupation = occ;
            Workhours = work;
            Overtime = over;
            rates = GetRates();
        }
        public abstract Rates GetRates();
        public bool Login(string username, string password)
        {
            if (username == Username && password == Password)
            {
                return true;
            }
            return false;
        }
        public void PrintSalary()
        {
            double total = 0;

            total = (Workhours / 160.0 * rates.BaseRate * BASE_RATE);
            double bonus = Overtime * rates.Overtime * BASE_RATE / 100;
            System.Console.WriteLine("--------- --------- --------- --------- --------- ");
            System.Console.WriteLine("Employee ID   : " + ID);
            System.Console.WriteLine("Full Name     : " + LastName + " " + FirstName);
            System.Console.WriteLine("Occupation    : " + Occupation);
            System.Console.WriteLine("Worked Hours  : " + Workhours + "/160\t" + "  Overtime Hours : " + Overtime);
            System.Console.WriteLine("Salary Rate   : " + rates.BaseRate + "\t" + "  Overtime Rate  : " + rates.Overtime);
            System.Console.WriteLine("Base Rate     : " + BASE_RATE + "\t  Overtime Base  : " + BASE_RATE / 100);
            System.Console.WriteLine("Pre-tax       : " + total.ToString("0.0") + "\t  Bonus          : " + bonus);

            total += bonus;
            double tax_amount = rates.Tax * total / 10;
            System.Console.WriteLine("Tax Amount    : " + tax_amount.ToString("0.0") + "\t  Tax Rate       : " + rates.Tax * 10 + "%");

            System.Console.WriteLine("--------- --------- --------- --------- --------- ");
            total -= tax_amount;
            System.Console.WriteLine("Total        $: " + total.ToString("0.0"));
            System.Console.WriteLine("--------- --------- --------- --------- --------- ");
        }

        public void PrintCompactSalary()
        {
            double total = (Workhours / 160.0 * rates.BaseRate * BASE_RATE);
            double tax_amount = rates.Tax * total / 10;
            double bonus = Overtime * rates.Overtime * BASE_RATE / 100;
            double final = total + bonus - tax_amount;
            System.Console.WriteLine(ID + "\t" + FirstName
                                    + "\t" + LastName + "\t"
                                    + limitChar(Occupation, 3) + "\t" + Workhours + "\t" + total
                                    + "\t" + Overtime + "\t" + bonus + "\t" + tax_amount
                                    + "\t" + final);
        }
        public void PrintData()
        {
            System.Console.WriteLine(ID + "\t" + Username + "\t" + limitChar(FirstName, 6) + "\t" + limitChar(LastName, 6) + "\t" + limitChar(Occupation, 3) + "\t" + Workhours + "\t" + Overtime);
        }

        public override string ToString()
        {
            return ID + ";" + Username + ";" + Password + ";" + FirstName + ";" + LastName + ";" + Occupation + ";" + Workhours + ";" + Overtime;
        }

        public string limitChar(string input, int limit)
        {
            if (input.Length > limit)
            {
                input = input.Substring(0, limit);
            }
            return input;
        }

        public void EditPassword()
        {
            System.Console.WriteLine("Enter old password: ");
            string pass = Console.ReadLine();
            if (Login(Username, pass))
            {
                System.Console.WriteLine("Enter new password: ");
                string newPassword = Console.ReadLine();
                Password = newPassword;
                System.Console.WriteLine("Password changed!");
            }
            else
            {
                System.Console.WriteLine("Wrong password!");
            }
        }
        public void EditInformation()
        {
            System.Console.WriteLine("Enter old password: ");
            string pass = Console.ReadLine();
            if (Login(Username, pass))
            {
                System.Console.WriteLine("Enter your First Name: ");
                FirstName = Console.ReadLine();
                System.Console.WriteLine("Enter your Last Name: ");
                LastName = Console.ReadLine();
                System.Console.WriteLine("User information changed!");
            }
            else
            {
                System.Console.WriteLine("Wrong password!");
            }
        }
        public bool isInt(string input)
        {
            return int.TryParse(input, out int numeric);
        }

        public bool isDouble(string input)
        {
            double d;
            return double.TryParse(input, out d);
        }
    }
    public class Admin : User
    {
        public Admin(User u) : base(u) { }
        public Admin(int id, string user, string pass, string first, string last, string occ, int work, int over)
        : base(id, user, pass, first, last, occ, work, over) { }
        public override Rates GetRates()
        {
            return new AdminRate();
        }

        public void AddUser(List<User> users)
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
            System.Console.WriteLine("ID \tUser\tFName \tLName \tPos \tWorkT \tOverT ");
            foreach (User u in users)
            {
                u.PrintData();
                count++;
            }
        }
    }

    public class Manager : User
    {
        public Manager(User u) : base(u) { }
        public Manager(int id, string user, string pass, string first, string last, string occ, int work, int over)
        : base(id, user, pass, first, last, occ, work, over) { }
        public override Rates GetRates()
        {
            return new ManagerRate();
        }
    }

    public class Employee : User
    {
        public Employee(User u) : base(u) { }
        public Employee(int id, string user, string pass, string first, string last, string occ, int work, int over)
        : base(id, user, pass, first, last, occ, work, over) { }
        public override Rates GetRates()
        {
            return new EmployeeRate();
        }
    }

    public class Accountant : User
    {
        private string inp;
        public Accountant(User u) : base(u) { }
        public Accountant(int id, string user, string pass, string first, string last, string occ, int work, int over)
        : base(id, user, pass, first, last, occ, work, over) { }
        public override Rates GetRates()
        {
            return new AccountantRate();
        }

        public void EditPayroll(List<User> users)
        {
            do
            {
                System.Console.WriteLine("Enter the employee's ID: ");
                inp = Console.ReadLine();
            } while (!isInt(inp));

            int id = Convert.ToInt32(inp);
            foreach (User u in users)
            {
                if (u.ID == id)
                {
                    System.Console.WriteLine("ID\tFName\tLName\tPos\tWHours\tTotal\tOver\tBonus\tTax\tFinal");
                    System.Console.WriteLine("--------------------------------------------------------------------------------");
                    u.PrintCompactSalary();
                    System.Console.WriteLine("--------------------------------------------------------------------------------");
                    do
                    {
                        System.Console.WriteLine("Enter new Work Hours:");
                        inp = Console.ReadLine();
                    } while (!isInt(inp));
                    u.Workhours = Convert.ToInt32(inp);

                    do
                    {
                        System.Console.WriteLine("Enter new Overtime:");
                        inp = Console.ReadLine();
                    } while (!isInt(inp));
                    u.Overtime = Convert.ToInt32(inp);
                    //
                    break;
                }
            }
            System.Console.WriteLine("ID not found!");
        }
        public void SearchPayroll(List<User> users)
        {
            System.Console.WriteLine("Enter the employee's ID: ");
            string stringId = Console.ReadLine();
            bool isNumber = int.TryParse(stringId, out int numeric);
            if (!isNumber)
            {
                System.Console.WriteLine("Invalid number!");
                return;
            }
            else
            {
                int id = Convert.ToInt32(stringId);
                foreach (User u in users)
                {
                    if (u.ID == id)
                    {
                        System.Console.WriteLine("ID\tFName\tLName\tPos\tWHours\tTotal\tOver\tBonus\tTax\tFinal");
                        System.Console.WriteLine("--------------------------------------------------------------------------------");
                        u.PrintCompactSalary();
                        return;
                    }
                }
                System.Console.WriteLine("ID not found!");
            }
        }

        public void ChangeBaseRate()
        {
            Console.Clear();
            System.Console.WriteLine("--- --- --- --- --- --- --- ---- --- --- --- --- --- --- --- ---");
            System.Console.WriteLine();
            System.Console.WriteLine("       Which role's base rate that needs to be changed?");
            System.Console.WriteLine("                        1. Admin.");
            System.Console.WriteLine("                        2. Accountant.");
            System.Console.WriteLine("                        3. Manager.");
            System.Console.WriteLine("                        4. Employee.");
            System.Console.WriteLine("                        0. Return.");
            System.Console.WriteLine();
            System.Console.WriteLine("--- --- --- --- --- --- --- ---- --- --- --- --- --- --- --- ---");
            System.Console.Write("                        Your choice: ");
            EditRate(Console.ReadLine());
        }

        public void EditRate(string choice)
        {
            string Data = "";
            string Case = "";
            switch (choice)
            {
                case "1":
                    Data = File.ReadAllText("Data/Rates/Admin.txt", Encoding.UTF8);
                    Case = "Admin.txt";
                    break;
                case "2":
                    Data = File.ReadAllText("Data/Rates/Accountant.txt", Encoding.UTF8);
                    Case = "Accountant.txt";
                    break;
                case "3":
                    Data = File.ReadAllText("Data/Rates/Manager.txt", Encoding.UTF8);
                    Case = "Manager.txt";
                    break;
                case "4":
                    Data = File.ReadAllText("Data/Rates/Employee.txt", Encoding.UTF8);
                    Case = "Employee.txt";
                    break;
                case "0":
                    return;
                default:
                    System.Console.WriteLine("Invalid!");
                    break;
            }

            string[] rates = Data.Split(";");
            string final = "";
            System.Console.WriteLine("");
            Console.Clear();
            System.Console.WriteLine("--- --- --- --- --- --- -- Old Rates -- --- --- --- --- --- ---");
            System.Console.WriteLine("           Base\tTax\tOvertime");
            System.Console.Write("           ");
            foreach (string c in rates)
            {
                System.Console.Write(c + "\t");
            }
            System.Console.WriteLine();
            System.Console.WriteLine("--- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---");
            do
            {
                System.Console.WriteLine("Enter new Base rate: ");
                inp = Console.ReadLine();
            } while (!isDouble(inp));
            final = final + inp + ";";

            do
            {
                System.Console.WriteLine("Enter new Tax rate: ");
                inp = Console.ReadLine();
            } while (!isDouble(inp));
            final = final + inp + ";";

            do
            {
                System.Console.WriteLine("Enter new Overtime rate: ");
                inp = Console.ReadLine();
            } while (!isDouble(inp));
            final = final + inp + ";";

            File.WriteAllText("Data/Rates/" + Case, final);
        }
    }
}