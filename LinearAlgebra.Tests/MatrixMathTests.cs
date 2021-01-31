using System;
using Xunit;

namespace LinearAlgebra.Tests
{
    public class MatrixMathTests
    {
        [Theory]
        [InlineData(2, 3)]
        [InlineData(5, 5)]
        [InlineData(1, 1)]
        [InlineData(2, 100)]
        public void Count_DifferentDimensions_MultiplyDimensions(int rows, int columns)
        {
            Matrix<int> matrix = new Matrix<int>(rows, columns);

            Assert.Equal(rows * columns, matrix.Count);
        }

        [Theory]
        [InlineData(new[] { 0, 0, 0 }, new[] { 0, 0, 0 }, new[] { 0, 0, 0 }, new[] { 0, 0, 0 })]
        [InlineData(new[] { 0 })]
        [InlineData(new[] { 0, 0 }, new[] { 0, 0 })]
        public void IsZero_ZeroElements_True(params int[][] data)
        {
            int[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);
            Matrix<int> matrix = array;

            Assert.True(matrix.IsZero);
        }

        [Theory]
        [InlineData(new[] { 0, 0, 0 }, new[] { 0, 1, 0 }, new[] { 0, 0, 0 }, new[] { 0, 0, 0 })]
        [InlineData(new[] { -4 })]
        [InlineData(new[] { 1, 2 }, new[] { 3, 4 })]
        public void IsZero_NonZeroElements_False(params int[][] data)
        {
            int[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);
            Matrix<int> matrix = array;

            Assert.False(matrix.IsZero);
        }

        [Theory]
        [InlineData(new[] { 1, 2, 4 }, new[] { 2, 4, 8 }, new[] { 50, 25, 5 }, new[] { 33, 30, 33 })]
        [InlineData(new[] { 1, 2 }, new[] { 3, 4 })]
        public void Transpose_SwappedRowsAndColumns_TransposedElements(params int[][] data)
        {
            int[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);
            Matrix<int> matrix = array;

            matrix.Transpose();

            for (int row = 0; row < matrix.Rows; row++)
            {
                for (int column = 0; column < matrix.Columns; column++)
                {
                    Assert.Equal(array[column, row], matrix[row, column]);
                }
            }
        }

        [Theory]
        [InlineData(0, 1, new[] { 1, 2, 4 }, new[] { 2, 4, 8 }, new[] { 50, 25, 5 }, new[] { 33, 30, 33 })]
        [InlineData(1, 1, new[] { 1, 2 }, new[] { 3, 4 })]
        public void Submatrix_MoreThanOneRowAndColumn_SkipRowAndColumn(int skipRow, int skipColumn, params int[][] data)
        {
            int[,] array = Utilities.ConvertArrayFromJaggedToMultidimensional(data);
            Matrix<int> initial = array;
            Matrix<int> matrix = initial.Submatrix(skipRow, skipColumn);

            int newRow = 0, newColumn;
            for (int row = 0; row < initial.Rows; row++)
            {
                if (row == skipRow)
                {
                    continue;
                }

                newColumn = 0;
                for (int column = 0; column < initial.Columns; column++)
                {
                    if (column == skipColumn)
                    {
                        continue;
                    }

                    Assert.Equal(initial[row, column], matrix[newRow, newColumn]);

                    newColumn++;
                }

                newRow++;
            }
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 3)]
        [InlineData(5, 1)]
        public void Submatrix_OneRowOrColumn_Throw(int rows, int columns)
        {
            Matrix<int> initial = new Matrix<int>(rows, columns);
            Matrix<int> matrix;

            Assert.Throws<MatrixTooSmallException>(() =>
            {
                matrix = initial.Submatrix(0, 0); // Row and column indicies have no significance
            });
        }
    }
}
