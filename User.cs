using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

using System;

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
            System.Console.WriteLine(ID + "\t" + Username + "\t" + limitChar(FirstName, 6) + "\t" + limitChar(LastName, 6) + "\t" + limitChar(Occupation,3) + "\t" + Workhours + "\t" + Overtime);
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
    }
    public class Admin : User
    {
        public Admin(int id, string user, string pass, string first, string last, string occ, int work, int over)
        : base(id, user, pass, first, last, occ, work, over) { }
        public override Rates GetRates()
        {
            return new AdminRate();
        }
    }

    public class Manager : User
    {
        public Manager(int id, string user, string pass, string first, string last, string occ, int work, int over)
        : base(id, user, pass, first, last, occ, work, over) { }
        public override Rates GetRates()
        {
            return new ManagerRate();
        }
    }

    public class Employee : User
    {
        public Employee(int id, string user, string pass, string first, string last, string occ, int work, int over)
        : base(id, user, pass, first, last, occ, work, over) { }
        public override Rates GetRates()
        {
            return new EmployeeRate();
        }
    }

    public class Accountant : User
    {
        public Accountant(int id, string user, string pass, string first, string last, string occ, int work, int over)
        : base(id, user, pass, first, last, occ, work, over) { }
        public override Rates GetRates()
        {
            return new AccountantRate();
        }
    }
}