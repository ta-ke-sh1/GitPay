using System.IO;
using System;

namespace GitPay
{
    public abstract class Rates
    {
        private string[] ratesData = new string[4];
        private string[] data;
        public double CoefficientSalary { get; set; }
        public double BaseRate { get; set; }
        public double Tax { get; set; }
        public double Overtime { get; set; }
        public Rates()
        {
            data = GetData();
            for (int i = 0; i < data.Length; i++)
            {
                ratesData = data[i].Split(";");
            }
            BaseRate = Convert.ToDouble(ratesData[0]);
            Tax = Convert.ToDouble(ratesData[1]);
            Overtime = Convert.ToDouble(ratesData[2]);
            CoefficientSalary = Convert.ToDouble(ratesData[3]);
        }
        public abstract string[] GetData();
    }

    public class AdminRate : Rates
    {
        public AdminRate() : base() { }

        public override string[] GetData()
        {
            return File.ReadAllLines("Data/Rates/Admin.txt");
        }
    }
    public class ManagerRate : Rates
    {
        public ManagerRate() : base() { }
        public override string[] GetData()
        {
            return File.ReadAllLines("Data/Rates/Manager.txt");
        }
    }
    public class EmployeeRate : Rates
    {
        public EmployeeRate() : base() { }

        public override string[] GetData()
        {
            return File.ReadAllLines("Data/Rates/Employee.txt");
        }
    }
    public class AccountantRate : Rates
    {
        public AccountantRate() : base() { }

        public override string[] GetData()
        {
            return File.ReadAllLines("Data/Rates/Accountant.txt");
        }
    }
}