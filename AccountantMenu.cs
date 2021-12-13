using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace Payroll
{
    public class AccountantMenu : Menu
    {
        private string inp;
        public AccountantMenu(User u, List<User> l)
        {
            session_user = u;
            users = l;
        }

        protected override void printMenu()
        {
            System.Console.WriteLine("--- --- --- --- --- -- Payroll Management -- --- --- --- --- ---");
            System.Console.WriteLine();
            System.Console.WriteLine("                     1. View all Payrolls");
            System.Console.WriteLine("                  2. Search Employee Payroll");
            System.Console.WriteLine("                   3. Edit Employee Payroll");
            System.Console.WriteLine("                      4. Change Base Rate");
            System.Console.WriteLine("                          0. Log Out");
            System.Console.WriteLine();
            System.Console.WriteLine("--- --- --- --- --- --- --- ---- --- --- --- --- --- --- --- ---");
        }

        protected override void doTask(string choice)
        {
            switch (choice)
            {
                case "1":
                    PrintAllPayrolls(); break;
                case "2":
                    SearchPayroll(); break;
                case "3":
                    EditPayroll(); break;
                case "4":
                    ChangeBaseRate(); break;
                case "0":
                    OverrideTxt(users);
                    Console.Clear(); break;
                default:
                    Console.Clear();
                    System.Console.WriteLine("--- --- --- --- --- --- --- ---- --- --- --- --- --- --- --- ---");
                    System.Console.WriteLine("                       Invalid choice!");
                    System.Console.WriteLine("--- --- --- --- --- --- --- ---- --- --- --- --- --- --- --- ---");
                    break;
            }
        }

        private void PrintAllPayrolls()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("ID\tFName\tLName\tPos\tWHours\tTotal\tOver\tBonus\tTax\tFinal");
            System.Console.WriteLine("--------------------------------------------------------------------------------");
            foreach (User u in users)
            {
                u.PrintCompactSalary();
            }
        }

        private void EditPayroll()
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
        private void SearchPayroll()
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
            GetRate(Console.ReadLine());
        }

        public void GetRate(string choice)
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
}