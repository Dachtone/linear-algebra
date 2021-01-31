using System;
using Xunit;

namespace LinearAlgebra.Tests
{
    public class MatrixSquareTests
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(5, 5)]
        public void IsSquare_SquareMatrix_True(int rows, int columns)
        {
            Matrix<int> matrix = new Matrix<int>(rows, columns);

            Assert.True(matrix.IsSquare);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 4)]
        [InlineData(3, 2)]
        public void IsSquare_NonSquareMatrix_False(int rows, int columns)
        {
            Matrix<int> matrix = new Matrix<int>(rows, columns);

            Assert.False(matrix.IsSquare);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(5, 5)]
        public void Order_SquareMatrix_SameAsRowsOrColumns(int rows, int columns)
        {
            Matrix<int> matrix = new Matrix<int>(rows, columns);

            Assert.Equal(rows, matrix.Order);
        }

        [Theory]
        [InlineData(10, new[] { 1, 2, 4 }, new[] { 2, 4, 8 }, new[] { 50, 25, 5 })]
        [InlineData(5, new[] { 1, 2 }, new[] { 3, 4 })]
        [InlineData(55, new[] { 42, 15, 0 }, new[] { 1, 8, 8 }, new[] { 7, 3, 5 })]
        public void Trace_ArbitraryMatrix_SumOfElementsOnMainDiagonal(int trace, params int[][] data)
        {
            int[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);
            Matrix<int> matrix = array;

            Assert.Equal(trace, matrix.Trace);
        }

        [Theory]
        [InlineData(1, 0, -90.0, new[] { 1, 2, 4 }, new[] { 2, 4, 8 }, new[] { 50, 25, 5 })]
        [InlineData(1, 1, -195.0, new[] { 1, 2, 4 }, new[] { 2, 4, 8 }, new[] { 50, 25, 5 })]
        [InlineData(0, 0, 4.0, new[] { 1, 2 }, new[] { 3, 4 })]
        [InlineData(2, 1, 336.0, new[] { 42, 15, 0 }, new[] { 1, 8, 8 }, new[] { 7, 3, 5 })]
        [InlineData(0, 2, -53.0, new[] { 42, 15, 0 }, new[] { 1, 8, 8 }, new[] { 7, 3, 5 })]
        public void Minor_ArbitraryMatrix_DeterminantOfSubmatrix(int row, int column, double minor, params int[][] data)
        {
            int[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);
            Matrix<int> matrix = array;

            Assert.Equal(minor, matrix.Minor(row, column));
        }

        [Theory]
        [InlineData(1, 0, 90.0, new[] { 1, 2, 4 }, new[] { 2, 4, 8 }, new[] { 50, 25, 5 })]
        [InlineData(1, 1, -195.0, new[] { 1, 2, 4 }, new[] { 2, 4, 8 }, new[] { 50, 25, 5 })]
        [InlineData(0, 0, 4.0, new[] { 1, 2 }, new[] { 3, 4 })]
        [InlineData(2, 1, -336.0, new[] { 42, 15, 0 }, new[] { 1, 8, 8 }, new[] { 7, 3, 5 })]
        [InlineData(0, 2, -53.0, new[] { 42, 15, 0 }, new[] { 1, 8, 8 }, new[] { 7, 3, 5 })]
        public void AlgebraicComplement_ArbitraryMatrix_SignedDeterminantOfSubmatrix(int row, int column, double complement, params int[][] data)
        {
            int[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);
            Matrix<int> matrix = array;

            Assert.Equal(complement, matrix.AlgebraicComplement(row, column));
        }

        [Fact] // Due to xUnit limitations on InlineData parameters, this test will have to be a Fact
        public void Adjugate_ArbitraryMatrix_AlgebraicComplementsForElements()
        {
            Matrix<int> expected = new int[,]
            {
                { 16, 51, -53 },
                { -75, 210, -21 },
                { 120, -336, 321 }
            };

            Matrix<int> matrix = new int[,]
            {
                { 42, 15, 0 },
                { 1, 8, 8 },
                { 7, 3, 5 }
            };

            Assert.Equal(expected, matrix.Adjugate);
        }

        [Theory]
        [InlineData(-2.0, new[] { 1, 2 }, new[] { 3, 4 })]
        [InlineData(0.0, new[] { 1, 2, 4 }, new[] { 2, 4, 8 }, new[] { 50, 25, 5 })]
        [InlineData(1437.0, new[] { 42, 15, 0 }, new[] { 1, 8, 8 }, new[] { 7, 3, 5 })]
        [InlineData(1122.0, new[] { 5, 3, -1, 4 }, new[] { 7, 2, 4, 1 }, new[] { 8, 2, -3, 6 }, new[] { -5, 4, 5, 9 })]
        [InlineData(-59176.0, new[] { 6, 4, 2, 2, 8, 5 }, new[] { 1, -1, 7, 9, 3, 1 },
            new[] { 8, 4, 2, 5, -3, 3 }, new[] { 3, -2, 9, 1, 8, 1 }, new[] { 6, 5, 2, 6, 4, 2 }, new[] { 1, -4, 7, 9, 1, -5 })]
        public void Determinant_ArbitraryMatrix_CalculatedDeterminantValue(double determinant, params int[][] data)
        {
            int[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);
            Matrix<int> matrix = array;

            Assert.Equal(determinant, matrix.Determinant);
        }
    }
}
