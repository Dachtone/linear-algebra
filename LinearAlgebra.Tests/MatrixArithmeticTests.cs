using System;
using Xunit;

namespace LinearAlgebra.Tests
{
    public class MatrixArithmeticTests
    {
        [Theory]
        [InlineData(15, new[] { 1, 2, 4 }, new[] { 2, 4, 8 }, new[] { 50, 25, 5 }, new[] { 33, 30, 33 })]
        [InlineData(-7, new[] { 1, 2 }, new[] { 3, 4 })]
        public void Add_EqualDimensions_ElementsAreSummed(int sum, params int[][] data)
        {
            int[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);
            Matrix<int> a = array;

            Matrix<int> b = new Matrix<int>(a.Rows, a.Columns);
            for (int row = 0; row < a.Rows; row++)
            {
                for (int column = 0; column < a.Columns; column++)
                {
                    b[row, column] = a[row, column] + sum;
                }
            }

            Matrix<int> matrix = a + b;

            for (int row = 0; row < a.Rows; row++)
            {
                for (int column = 0; column < a.Columns; column++)
                {
                    Assert.Equal(a[row, column] + b[row, column], matrix[row, column]);
                }
            }
        }

        [Theory]
        [InlineData(2, 3, 2, 2)]
        [InlineData(5, 5, 4, 4)]
        public void Add_DifferentDimensions_Throw(int rowsA, int columnsA, int rowsB, int columnsB)
        {
            Matrix<int> a = new Matrix<int>(rowsA, columnsA);
            Matrix<int> b = new Matrix<int>(rowsB, columnsB);
            Matrix<int> matrix;

            Assert.Throws<MatriciesAreNotOfTheSameSizeException>(() =>
            {
                matrix = a + b;
            });
        }

        [Fact] // Due to xUnit limitations on InlineData parameters, this test will have to be a Fact
        public void Multiply_MatriciesWithCorrectDimensions_MatrixProduct()
        {
            Matrix<int> expected = new int[,]
            {
                { 24, 15 },
                { 13, -2 },
                { -93, 12 },
                { 27, 24 }
            };

            Matrix<int> a = new int[,]
            {
                { 2, 3, -1 },
                { 1, 2, -4 },
                { -1, -12, 14 },
                { 2, 3, 2 }
            };

            Matrix<int> b = new int[,]
            {
                { -1, 6 },
                { 9, 2 },
                { 1, 3 }
            };

            Matrix<int> actual = a * b;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(2, 3, 2, 2)]
        [InlineData(1, 3, 2, 2)]
        [InlineData(4, 4, 3, 4)]
        public void Multiply_MatriciesWithIncorrectDimensions_Throw(int rowsA, int columnsA, int rowsB, int columnsB)
        {
            Matrix<int> a = new Matrix<int>(rowsA, columnsA);
            Matrix<int> b = new Matrix<int>(rowsB, columnsB);
            Matrix<int> matrix;

            Assert.Throws<NumberOfColumnsDoesNotMatchNumberOfRowsException>(() =>
            {
                matrix = a * b;
            });
        }
    }
}
