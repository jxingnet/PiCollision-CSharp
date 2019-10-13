using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace PiCollision
{
    public class Block
    {
        Microsoft.Extensions.Logging.ILogger _logger;
        public double x, w, m, v, y, xc, height, xConstraint;
        public Block(ILoggerFactory loggerFactory, string name, double x, double w, double m, double v, double xc)
        {
            _logger = loggerFactory.CreateLogger("Block " + name);

            this.x = x;
            this.y = height - w;
            this.w = w;
            this.v = v;
            this.m = m;
            this.xConstraint = xc;
        }

        public bool hitWall()
        {
            //_logger.LogTrace("Hit wall?");
            return (this.x <= 0);
        }

        public void reverse()
        {
            this.v *= -1;
        }

        public bool collide(Block other)
        {
            //_logger.LogTrace("Collide?");
            return !(this.x + this.w < other.x ||
              this.x > other.x + other.w);
        }

        public double bounce(Block other)
        {
            //_logger.LogTrace("Bounce?");
            var sumM = this.m + other.m;
            var newV = (this.m - other.m) / sumM * this.v;
            newV += (2 * other.m / sumM) * other.v;
            return newV;
        }

        public void update()
        {
            //_logger.LogTrace("Update");
            this.x += this.v;
        }

        public void show()
        {
            //var x = constrain(this.x, this.xConstraint, width);
            //image(blockImg, x, this.y, this.w, this.w);
        }
    }
}
