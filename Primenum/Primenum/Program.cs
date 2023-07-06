using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primenum
{
    class Program
    {
        static void Main(string[] args)
        {
            int numb;
            Console.WriteLine("Enter a number: ");

            numb = Convert.ToInt32(Console.ReadLine());

            if (numb < 2)
                Console.WriteLine(numb + " is not a prime");
            else
            {
                int i = 2;
                bool check = true;
                while (i <= Math.Sqrt(numb))
                {
                    if (numb % i == 0)
                    {
                        check = false;
                        break;
                    }
                    i++;
                }
                if (check)
                    Console.WriteLine(numb + " is a prime");
                else
                    Console.WriteLine(numb + " is not a prime");
            }
        }
    }
}
