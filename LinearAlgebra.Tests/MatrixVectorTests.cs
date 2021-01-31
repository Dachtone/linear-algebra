using System;
using Xunit;

namespace LinearAlgebra.Tests
{
    public class MatrixVectorTests
    {
        [Theory]
        [InlineData(new[] { 0, 3, 0 })]
        [InlineData(new[] { 1 })]
        [InlineData(new[] { 1 }, new[] { 2 }, new[] { 3 })]
        public void IsVector_OneRowOrColumn_True(params int[][] data)
        {
            int[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);
            Matrix<int> matrix = array;

            Assert.True(matrix.IsVector);
        }

        [Theory]
        [InlineData(new[] { 0, 3, 0 }, new int[] { 1, 1, 1 })]
        [InlineData(new[] { 1, 1 }, new[] { -1, -1 })]
        public void IsVector_MoreThanOneRowAndColumn_False(params int[][] data)
        {
            int[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);
            Matrix<int> matrix = array;

            Assert.False(matrix.IsVector);
        }

        [Theory]
        [InlineData(new[] { 0, 3, 0 })]
        [InlineData(new[] { 1 })]
        public void IsRowVector_OneRow_True(params int[][] data)
        {
            int[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);
            Matrix<int> matrix = array;

            Assert.True(matrix.IsRowVector);
        }

        [Theory]
        [InlineData(new[] { 0, 3, 0 }, new[] { 3, 0, 3 })]
        [InlineData(new[] { 1 }, new[] { 2 }, new[] { 3 })]
        public void IsRowVector_MoreThanOneRow_False(params int[][] data)
        {
            int[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);
            Matrix<int> matrix = array;

            Assert.False(matrix.IsRowVector);
        }

        [Theory]
        [InlineData(new[] { 1 }, new[] { 2 }, new[] { 3 })]
        [InlineData(new[] { 0 })]
        public void IsColumnVector_OneColumn_True(params int[][] data)
        {
            int[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);
            Matrix<int> matrix = array;

            Assert.True(matrix.IsColumnVector);
        }

        [Theory]
        [InlineData(new[] { 0, 3, 0 }, new[] { 3, 0, 3 })]
        [InlineData(new[] { 0, 3, 0 })]
        public void IsColumnVector_MoreThanOneColumn_False(params int[][] data)
        {
            int[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);
            Matrix<int> matrix = array;

            Assert.False(matrix.IsColumnVector);
        }
    }
}
