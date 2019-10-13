using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace PiCollision
{
    public class Sketch
    {
        Microsoft.Extensions.Logging.ILogger _logger;
        const string piMax = "3141592653589793238";

        Block block1;
        Block block2;

        int count = 0;
        int digits = 7; // how many digits
        long timeSteps;
        long pi;

        public Sketch(ILoggerFactory loggerFactory)
        {
            if (digits > piMax.Length)
                throw new Exception("Digit is too large");

            _logger = loggerFactory.CreateLogger("Sketch");

            _logger.LogInformation("Setup...");
            timeSteps = (long)Math.Pow(10, digits - 1);

            var pistr = piMax.Substring(0, digits);
            pi = long.Parse(pistr);

            // setup
            block1 = new Block(loggerFactory, "small", 100, 20, 1, 0, 0);
            var m2 = Math.Pow(100, digits - 1);
            block2 = new Block(loggerFactory, "Big", 300, 100, m2, -1.0 / timeSteps, 20);
            //countDiv = createDiv(count);
            //countDiv.style('font-size', '72pt');
        }

        public bool draw()
        {
            var clackSound = false;

            for (long i = 0; i < timeSteps; i++)
            {
                if (block1.collide(block2))
                {
                    var v1 = block1.bounce(block2);
                    var v2 = block2.bounce(block1);
                    block1.v = v1;
                    block2.v = v2;
                    clackSound = true;
                    count++;
                }

                if (block1.hitWall())
                {
                    block1.reverse();
                    clackSound = true;
                    count++;
                }

                block1.update();
                block2.update();
            }

            if (clackSound)
            {
                //clack.play();
                _logger.LogWarning("Clack...");
            }

            //block1.show();
            //block2.show();

            //countDiv.html(nf(count, digits));
            _logger.LogInformation($"{count}");

            return pi == count;
            //return count.ToString();
        }
    }
}
