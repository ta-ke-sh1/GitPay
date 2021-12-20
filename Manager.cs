using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace GitPay
{
    public class Manager : User
    {
        private string intCheck;
        public Manager(User u) : base(u) { }
        public Manager(int id, string user, string pass, string first, string last, string occ, int work, int over)
        : base(id, user, pass, first, last, occ, work, over) { }
        public override Rates GetRates()
        {
            return new ManagerRate();
        }

        public void EditPayroll(List<User> users)
        {
            do
            {
                System.Console.WriteLine("Enter the employee's ID: ");
                intCheck = Console.ReadLine();
            } while (!isInt(intCheck));

            int id = Convert.ToInt32(intCheck);
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
                        intCheck = Console.ReadLine();
                    } while (!isInt(intCheck));
                    u.Workhours = Convert.ToInt32(intCheck);

                    do
                    {
                        System.Console.WriteLine("Enter new Overtime:");
                        intCheck = Console.ReadLine();
                    } while (!isInt(intCheck));
                    u.Overtime = Convert.ToInt32(intCheck);
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
    }
}