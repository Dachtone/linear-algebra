using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebra.Tests
{
    internal static class Utilities
    {
        /// <summary>
        /// Converts from a jagged array to a multidimensional array.
        /// </summary>
        /// <typeparam name="T">Array type.</typeparam>
        /// <param name="jagged">Jagged array to convert.</param>
        /// <returns>Multidimensional array.</returns>
        public static T[,] ConvertArrayFromJaggedToMultidimensional<T>(T[][] jagged)
        {
            int rows = 0, columns = 0, columnsNext; // columnsNext is used for counting current number
            foreach (T[] row in jagged)
            {
                rows++;

                columnsNext = 0;
                foreach (T value in row)
                {
                    columnsNext++;
                }

                if (columns == 0)
                {
                    columns = columnsNext;
                }
                else
                {
                    if (columnsNext != columns)
                    {
                        throw new ArgumentException("The number of columns should be the same across all rows.");
                    }
                }
            }

            T[,] multidimensional = new T[rows, columns];
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < columns; columnIndex++)
                {
                    multidimensional[rowIndex, columnIndex] = jagged[rowIndex][columnIndex];
                }
            }

            return multidimensional;
        }
    }
}
