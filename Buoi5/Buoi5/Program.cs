using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buoi5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Linear Equation Resolver");
            Console.WriteLine("Give equation as 'a * x + b = 0', please type in constant:");

            Console.Write("a: ");

            double a = Convert.ToDouble(Console.ReadLine());

            Console.Write("b: ");

            double b = Convert.ToDouble(Console.ReadLine());

            if (a != 0)
            {
                //input the solution
                double solution = -b / a;
                Console.Write("The solution is: {0}!", solution);
            }
            //else
            else
            {
                if (b == 0)
                {
                    Console.Write("The solution is all x!");
                }
                else
                {
                    Console.Write("Nod solution!");
                }
            }
            Console.ReadLine();
        }
    }
}
