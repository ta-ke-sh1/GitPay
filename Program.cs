using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace Payroll
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
