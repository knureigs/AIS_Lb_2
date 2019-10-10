using System;
using Gray;

namespace AIS
{
    class AntyBody
    {
        public double x1;
        public double x2;

        public double result = 0;

        public AntyBody(int seed)
        {
            Random rand = new Random(seed);
            int sign = rand.Next(0, 2) == 0 ? -1 : 1;
            x1 = rand.NextDouble() * sign;
            x2 = rand.NextDouble() * sign;
        }

        public AntyBody(double x1, double x2)
        {
            this.x1 = x1;
            this.x2 = x2;
        }

        public AntyBody(AntyBody antyBody)
        {
            this.x1 = antyBody.x1;
            this.x2 = antyBody.x2;
            this.result = antyBody.result;
        }

        public void SetAffinity()
        {
            result = x1 * x2 * Math.Sin(x1 * x1 + x2 * x2);
        }

        internal AntyBody Clone()
        {
            return new AntyBody(this.x1, this.x2);
        }

        internal void Mutate(int seed, int minProb)
        {
            string x1Gray = GrayCode.BinStringFormat(Convert.ToString(x1 ^ (x1 >> 1), 2));
        }

        //internal void Mutate(int seed, int minProb)
        //{
        //    Random rand = new Random();
        //    bool probmutation = rand.Next(0, 100) > minProb ? true : false;
        //    if (probmutation)
        //    {
        //        x1 += rand.NextDouble();
        //        x2 += rand.NextDouble();
        //    }
        //}

        public override string ToString()
        {
            return "X1=" + x1.ToString("F2") + ", X2=" + x2.ToString("F2") + ", aff=" + result.ToString("F2");
        }
    }
}
