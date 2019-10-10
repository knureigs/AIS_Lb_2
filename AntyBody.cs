using System;

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
            //result = x1 * x2 * Math.Sin(x1 * x1 + x2 * x2);
            //result = Math.Sin(x1*x1 + x2*x2);
            result = Math.Sin(x1);
        }

        internal AntyBody Clone()
        {
            return new AntyBody(this.x1, this.x2);
        }

        internal void Mutate(int seed, int probability)
        {
            Random rand = new Random(seed);
            bool probmutation = rand.Next(0, 100) < probability ? true : false;
            if (probmutation)
            {
                double x1n=0, x2n=0, modify=0;
                do
                {
                    modify = rand.NextDouble() * (rand.Next(0, 2) == 0 ? -1 : 1);
                    x1n = x1 + modify;
                }
                while (x1n > 1 || x1n < -1);
                x1 += modify;

                do
                {
                    modify = rand.NextDouble() * (rand.Next(0, 2) == 0 ? -1 : 1);
                    x2n = x2 + modify;
                }
                while (x2n > 1 || x2n < -1);
                x2 += modify;
            }
        }

        public override string ToString()
        {
            return "X1=" + x1.ToString("F2") + ", X2=" + x2.ToString("F2") + ", aff=" + result.ToString("F2");
        }
    }
}
