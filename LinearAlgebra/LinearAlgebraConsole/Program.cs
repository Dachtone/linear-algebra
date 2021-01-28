using System;
using LinearAlgebra;

namespace LinearAlgebraConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix<double> a = new double[,]
            {
                { 2, 3, -1 },
                { 1, 2, -4 },
                { -1, -12, 14 }
            };

            Matrix<double> b = new double[,]
            {
                { -1 },
                { 9 },
                { 1 }
            };

            // Console.WriteLine(a.Adjugate);
            Console.WriteLine(((1.0 / a.Determinant) * a.Adjugate.Transposition) * b);

            Console.ReadKey();
        }
    }
}
