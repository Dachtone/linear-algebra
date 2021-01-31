using System;
using Xunit;

namespace LinearAlgebra.Tests
{
    public class MatrixConstructionTests
    {
        [Theory]
        [InlineData(2, 3)]
        [InlineData(5, 5)]
        [InlineData(1, 1)]
        [InlineData(2, 100)]
        public void Constructor_ValidDimensions_Construction(int rows, int columns)
        {
            Matrix<int> matrix = new Matrix<int>(rows, columns);

            Assert.Equal(rows, matrix.Rows);
            Assert.Equal(columns, matrix.Columns);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(2, 0)]
        [InlineData(-4, -4)]
        [InlineData(-2, 2)]
        public void Constructor_InvalidDimensions_Throw(int rows, int columns)
        {
            Assert.Throws<InvalidMatrixDimensionsException>(() =>
            {
                Matrix<int> matrix = new Matrix<int>(rows, columns);
            });
        }

        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(1)]
        [InlineData(100)]
        public void Constructor_ValidOrder_Construction(int order)
        {
            Matrix<int> matrix = new Matrix<int>(order);

            Assert.Equal(order, matrix.Order);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public void Constructor_InvalidOrder_Throw(int order)
        {
            Assert.Throws<InvalidMatrixDimensionsException>(() =>
            {
                Matrix<int> matrix = new Matrix<int>(order);
            });
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3 }, new[] { 0, 2, -1 }, new[] { 3, 2, 1 }, new[] { 3, 2, 1 })]
        [InlineData(new[] { 10 })]
        [InlineData(new[] { 2, 2 }, new[] { 3, -3 } )]
        public void Constructor_FromArray_Construction(params int[][] data)
        {
            int[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);

            Matrix<int> matrix = array;

            Assert.Equal(array, matrix.ToArray());
        }

        [Theory]
        [InlineData(new[] { 1.0, 2.5, 3.1 }, new[] { 0.2, 2.5, 1.0 }, new[] { 3.33, 2.9, 1.0 }, new[] { 3.1, 2.2, 1.3 })]
        [InlineData(new[] { 10.01 })]
        [InlineData(new[] { 2.2, 2.2 }, new[] { 3.03, 3.03 })]
        public void Constructor_FromArrayDouble_Construction(params double[][] data)
        {
            double[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);

            Matrix<double> matrix = array;

            Assert.Equal(array, matrix.ToArray());
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3 }, new[] { 0, 2, 1 }, new[] { 3, 2, 1 }, new[] { 3, 2, 1 })]
        [InlineData(new[] { 10 })]
        [InlineData(new[] { 2, 2 }, new[] { 3, 3 })]
        public void Constructor_FromMatrix_Construction(params int[][] data)
        {
            int[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);
            Matrix<int> other = array;

            Matrix<int> matrix = other;

            Assert.Equal(other, matrix);
        }
    }
}
