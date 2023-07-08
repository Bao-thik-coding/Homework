using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lonnhat
{
    class Program
    {
        static void Main(string[] args)
        {
            int vitri = lstInpt.IndexOf(find);
            if (vitri >= 0) Console.WriteLine("TIm thay tai thu {0}", vitri);
            else Console.WriteLine("Ko tim thay");

            int max_value = lstInpt.Max();
            int max_index = lstInpt.Max();
            Console.WriteLine("gia tri max={1}, tai thu {0}", max_index, max_value);
        }
    }
}
