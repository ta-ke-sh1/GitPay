
using System.Security.Cryptography;
using System;
using System.Text;

namespace GitPay
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu m = new LoginMenu();
            m.Run();
        }
    }
}
