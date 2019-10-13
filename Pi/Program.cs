using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi
{

    class Program
    {
        static void pi_series()
        {
            int i;
            double pi = 0.0;

            Console.Write("Input number of series : ");

            i = int.Parse(Console.ReadLine());

            for (int j = 0; j < i; j++)
            {
                if ((j % 2) == 1)
                    pi -= 1.0 / (j * 2 + 1);
                else
                    pi += 1.0 / (j * 2 + 1);

                //Console.WriteLine("Pi = {0}, {1} series", pi * 4, j);
            }

            Console.WriteLine("Pi = {0}, {1} series", 4 * pi, i);
        }

        static void pi_Euler()
        {
            double i;
            double pi = 0.0;

            Console.Write("Input number of series : ");

            i = int.Parse(Console.ReadLine());

            for (double j = 1; j <= i; j++)
            {
                pi += 1.0 / (j * j);
                //Console.WriteLine("Pi = {0}, {1} series", pi * 4, j);
            }

            Console.WriteLine("Euler Pi = {0}, {1} series", Math.Sqrt(pi * 6), i);
        }

        static void Main(string[] args)
        {
            pi_Euler();

            pi_series();

            Console.ReadLine();
        }
    }
}
