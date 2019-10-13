using System;
using System.Diagnostics;

namespace PiCollisionPure
{
    class Program
    {
        static void Main(string[] args)
        {
            int digits = 7; // how many digits

            var sketch = new Sketch(digits);

            var sw = new Stopwatch();
            sw.Start();
            while (!sketch.draw())
            {
            }
            sw.Stop();

            Console.WriteLine($"Elapsed : {sw.Elapsed}");

        }
    }
}
