using System;
using System.Numerics;

namespace EvenText
{
    internal class Program
    {
        public static int JacobiSymbol(BigInteger a, BigInteger b)
        {
            if (BigInteger.GreatestCommonDivisor(a, b) != 1)
                return 0;

            int r = 1;
            if (a < 0)
            {
                a = -a;
                if (b % 4 == 3)
                    r = -r;
            }

            while (a != 0)
            {
                int t = 0;
                while (a % 2 == 0)
                {
                    t++;
                    a /= 2;
                }
                if (t % 2 == 1)
                {
                    BigInteger mod8 = b % 8;
                    if (mod8 == 3 || mod8 == 5)
                        r = -r;
                }
                if (a % 4 == 3 && b % 4 == 3)
                    r = -r;

                BigInteger c = a;
                a = b % c;
                b = c;
            }

            return (b == 1) ? r : 0;
        }

        public static string EvenTest(BigInteger n, int k)
        {
            if ((n < 5) || (n % 2 == 0))
                return "число должно быть нечет и >5";

            Random random = new Random();
            for (int i = 0; i < k; i++)
            {
                BigInteger a;
                do
                {
                    a = new BigInteger(random.Next(2, (int)BigInteger.Min(int.MaxValue - 1, n - 2) + 1));
                } while (BigInteger.GreatestCommonDivisor(a, n) != 1);

                BigInteger r = BigInteger.ModPow(a, (n - 1) / 2, n);
                if (r != 1 && r != n - 1)
                    return "число составное";

                int s = JacobiSymbol(a, n);
                BigInteger sMod = (s % n + n) % n;

                if (r != sMod)
                    return "число составное";
            }

            return "n вероятно простое";
        }

        static void Main(string[] args)//Факторизация 41⋅184829⋅460429974611
        {
            Console.WriteLine(EvenTest(3489133282872437279, 1));
            Console.ReadKey();
        }
    }
}