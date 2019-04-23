using System.Numerics;

namespace RSAKillerByEiler
{
    public class EuclidExtended
    {
        private BigInteger a, b;

        public EuclidExtended(BigInteger a, BigInteger b)
        {
            this.a = a;
            this.b = b;
        }

        public void Solve()
        {
            BigInteger x0 = 1, xn = 1, y0 = 0, yn = 0, x1 = 0, y1 = 1, f, r = a % b;

            while (r > 0)
            {
                f = a / b;
                xn = x0 - f * x1;
                yn = y0 - f * y1;

                x0 = x1;
                y0 = y1;
                x1 = xn;
                y1 = yn;
                a = b;
                b = r;
                r = a % b;
            }

            XN = xn;
            YN = yn;
            B = b;
        }

        public BigInteger B { get; set; }

        public BigInteger YN { get; set; }

        public BigInteger XN { get; set; }
    }
}