using System;

namespace LinearAlgebra
{
    public class RangedRandom
    {
        Random random;

        public RangedRandom()
        {
            random = new Random();
        }

        /// <summary>
        /// Generates a random integer from the specified range.
        /// </summary>
        /// <param name="lowerBound">Minimum value</param>
        /// <param name="upperBound">Maximum value</param>
        /// <returns></returns>
        public int Generate(int lowerBound, int upperBound)
        {
            return random.Next(lowerBound, upperBound);
        }

        /// <summary>
        /// Generates a random double from the specified range.
        /// </summary>
        /// <param name="lowerBound">Minimum value</param>
        /// <param name="upperBound">Maximum value</param>
        /// <returns></returns>
        public double GenerateDouble(double lowerBound, double upperBound)
        {
            return (lowerBound + random.NextDouble() * (upperBound - lowerBound));
        }
    }
}
