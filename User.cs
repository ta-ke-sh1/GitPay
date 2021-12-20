
using System.Collections.Generic;
using System;

namespace GitPay
{
    public abstract class User
    {
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
                if (value.Length > 12)
                {
                    System.Console.WriteLine("Password cannot be longer than 12 characters, set as the first 12 characters instead!");
                    password = limitChar(value, 12);
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
        private int workHours;
        public int Workhours
        {
            get { return workHours; }
            set { workHours = value; }
        }
        public int Overtime
        {
            get { return overtime; }
            set { overtime = value; }
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

            total = (Workhours / 160.0 * rates.BaseRate * rates.CoefficientSalary);
            double bonus = Overtime * rates.Overtime * rates.CoefficientSalary / 100;
            System.Console.WriteLine("--- --- --- --- --- --- --- ---- --- --- --- --- --- --- --- ---");
            System.Console.WriteLine("Employee ID   : " + ID);
            System.Console.WriteLine("Full Name     : " + LastName + " " + FirstName);
            System.Console.WriteLine("Occupation    : " + Occupation);
            System.Console.WriteLine("Worked Hours  : " + Workhours + "/160\t" + "  Overtime Hours : " + Overtime);
            System.Console.WriteLine("Salary Rate   : " + rates.BaseRate + "\t" + "  Overtime Rate  : " + rates.Overtime);
            System.Console.WriteLine("Base Rate     : " + rates.CoefficientSalary + "\t  Overtime Base  : " + rates.CoefficientSalary / 100);
            System.Console.WriteLine("Pre-tax       : " + total.ToString("0.0") + "\t  Bonus          : " + bonus);

            total += bonus;
            double tax_amount = rates.Tax * total / 10;
            System.Console.WriteLine("Tax Amount    : " + tax_amount.ToString("0.0") + "\t  Tax Rate       : " + rates.Tax * 10 + "%");

            System.Console.WriteLine("--- --- --- --- --- --- --- ---- --- --- --- --- --- --- --- ---");
            total -= tax_amount;
            System.Console.WriteLine("Total        $: " + total.ToString("0.0"));
            System.Console.WriteLine("--- --- --- --- --- --- --- ---- --- --- --- --- --- --- --- ---");
        }

        public void PrintCompactSalary()
        {
            double total = (Workhours / 160.0 * rates.BaseRate * rates.CoefficientSalary);
            double tax_amount = rates.Tax * total / 10;
            double bonus = Overtime * rates.Overtime * rates.CoefficientSalary / 100;
            double final = total + bonus - tax_amount;
            System.Console.WriteLine(ID
                                    + "\t" + limitChar(FirstName, 6)
                                    + "\t" + limitChar(LastName, 6)
                                    + "\t" + limitChar(Occupation, 3)
                                    + "\t" + Workhours
                                    + "\t" + String.Format("{0:0.##}", total)
                                    + "\t" + Overtime
                                    + "\t" + String.Format("{0:0.##}", bonus)
                                    + "\t" + String.Format("{0:0.##}", tax_amount)
                                    + "\t" + String.Format("{0:0.##}", final));
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

        public void EditPassword(List<User> users, User user)
        {
            foreach (User u in users)
            {
                if (u.Username == this.Username)
                {
                    System.Console.WriteLine("Enter old password: ");
                    string pass = Console.ReadLine();
                    if (Login(u.Username, pass))
                    {
                        string newPassword = "";
                        do
                        {
                            System.Console.WriteLine("Enter new password: ");
                            newPassword = Console.ReadLine();
                        } while (pwdCheck(newPassword));

                        u.Password = newPassword;
                        System.Console.WriteLine("Password changed!");
                    }
                    else
                    {
                        System.Console.WriteLine("Wrong password!");
                    }
                }
            }
        }

        public bool pwdCheck(string newPassword)
        {
            int upper = 0;
            int number = 0;
            int lower = 0;

            if (newPassword.Length > 12)
            {
                System.Console.WriteLine("Password cannot be longer than 12 characters!");
                return false;
            }

            foreach (char c in newPassword)
            {
                if (Char.IsUpper(c)) { upper++; }
                if (Char.IsLower(c)) { lower++; }
                if (Char.IsNumber(c)) { number++; }
            }

            if (number == 0)
            {
                System.Console.WriteLine("Password must contains at least 1 number!");
                return false;
            }

            if (upper == 0)
            {
                System.Console.WriteLine("Password must contains at least 1 uppercase letter!");
                return false;
            }

            if (lower == 0)
            {
                System.Console.WriteLine("Password must contains at least 1 lowercase letter!");
                return false;
            }

            return true;
        }
        public void EditInformation(List<User> users, User user)
        {
            foreach (User u in users)
            {
                if (u.Username == this.Username)
                {
                    System.Console.WriteLine("Enter old password: ");
                    string pass = Console.ReadLine();
                    if (Login(Username, pass))
                    {
                        System.Console.WriteLine("Enter your First Name: ");
                        u.FirstName = Console.ReadLine();
                        System.Console.WriteLine("Enter your Last Name: ");
                        u.LastName = Console.ReadLine();
                        System.Console.WriteLine("User information changed!");
                    }
                    else
                    {
                        System.Console.WriteLine("Wrong password!");
                    }
                }
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

}