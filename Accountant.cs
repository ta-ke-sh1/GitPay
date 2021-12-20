using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace GitPay
{
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
            switch (choice)
            {
                case "1":
                    Edit(File.ReadAllText("Data/Rates/Admin.txt", Encoding.UTF8), "Admin.txt");
                    break;
                case "2":
                    Edit(File.ReadAllText("Data/Rates/Accountant.txt", Encoding.UTF8), "Accountant.txt");
                    break;
                case "3":
                    Edit(File.ReadAllText("Data/Rates/Manager.txt", Encoding.UTF8), "Manager.txt");
                    break;
                case "4":
                    Edit(File.ReadAllText("Data/Rates/Employee.txt", Encoding.UTF8), "Employee.txt");
                    break;
                case "0":
                    return;
                default:
                    System.Console.WriteLine("Invalid!");
                    break;
            }
        }

        public void Edit(string Data, string Case)
        {
            string[] rates = Data.Split(";");
            string final = "";
            System.Console.WriteLine("");
            Console.Clear();
            System.Console.WriteLine("--- --- --- --- --- --- -- Old Rates -- --- --- --- --- --- ---");
            System.Console.WriteLine("            Base\tTax\tBonus\tCoefficients");
            System.Console.Write("            " + rates[0] + "   \t" + rates[1] + "\t" + rates[2] + "\t" + rates[3]);
            System.Console.WriteLine();
            System.Console.WriteLine("--- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---");
            do
            {
                System.Console.WriteLine("Enter new Base Rate: ");
                inp = Console.ReadLine();
            } while (!isDouble(inp));
            final = final + inp + ";";

            do
            {
                System.Console.WriteLine("Enter new Tax Rate: ");
                inp = Console.ReadLine();
            } while (!isDouble(inp));
            final = final + inp + ";";

            do
            {
                System.Console.WriteLine("Enter new Bonus Rate: ");
                inp = Console.ReadLine();
            } while (!isDouble(inp));
            final = final + inp + ";";

            do
            {
                System.Console.WriteLine("Enter new Coefficient Rate: ");
                inp = Console.ReadLine();
            } while (!isDouble(inp));
            final = final + inp + ";";

            File.WriteAllText("Data/Rates/" + Case, final);
        }

        public void ViewBaseRate()
        {
            string[] AdminData = File.ReadAllText("Data/Rates/Admin.txt", Encoding.UTF8).Split(";");
            string[] AccountantData = File.ReadAllText("Data/Rates/Accountant.txt", Encoding.UTF8).Split(";");
            string[] EmployeeData = File.ReadAllText("Data/Rates/Employee.txt", Encoding.UTF8).Split(";");
            string[] ManagerData = File.ReadAllText("Data/Rates/Manager.txt", Encoding.UTF8).Split(";");

            System.Console.WriteLine("--- --- --- --- --- --- --- ---- --- --- --- --- --- --- --- ---");
            System.Console.WriteLine();
            System.Console.WriteLine("Role      \tBase\tTax\tBonus\tCoefficient");
            System.Console.WriteLine("Admin     \t" + AdminData[0] + "\t" + AdminData[1] + "\t" + AdminData[2] + "\t" + AdminData[3]);
            System.Console.WriteLine("Accountant\t" + AccountantData[0] + "\t" + AccountantData[1] + "\t" + AccountantData[2] + "\t" + AccountantData[3]);
            System.Console.WriteLine("Manager   \t" + ManagerData[0] + "\t" + ManagerData[1] + "\t" + ManagerData[2] + "\t" + ManagerData[3]);
            System.Console.WriteLine("Employee  \t" + EmployeeData[0] + "\t" + EmployeeData[1] + "\t" + EmployeeData[2] + "\t" + EmployeeData[3]);
            System.Console.WriteLine();
        }
    }
}