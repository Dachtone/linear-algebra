using System;

namespace LinearAlgebra
{
    public class InvalidMatrixDimensionsException : Exception
    {
        public InvalidMatrixDimensionsException()
        {
        }

        public InvalidMatrixDimensionsException(string message)
            : base(message)
        {
        }

        public InvalidMatrixDimensionsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
 

    public class MatriciesAreNotOfTheSameSizeException : Exception
    {
        public MatriciesAreNotOfTheSameSizeException()
        {
        }

        public MatriciesAreNotOfTheSameSizeException(string message)
            : base(message)
        {
        }

        public MatriciesAreNotOfTheSameSizeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class MatrixTooSmallException : Exception
    {
        public MatrixTooSmallException()
        {
        }

        public MatrixTooSmallException(string message)
            : base(message)
        {
        }

        public MatrixTooSmallException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class NumberOfColumnsDoesNotMatchNumberOfRowsException : Exception
    {
        public NumberOfColumnsDoesNotMatchNumberOfRowsException()
        {
        }

        public NumberOfColumnsDoesNotMatchNumberOfRowsException(string message)
            : base(message)
        {
        }

        public NumberOfColumnsDoesNotMatchNumberOfRowsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class NotASquareMatrixException : Exception
    {
        public NotASquareMatrixException()
        {
        }

        public NotASquareMatrixException(string message)
            : base(message)
        {
        }

        public NotASquareMatrixException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class FirstOrderMatrixException : Exception
    {
        public FirstOrderMatrixException()
        {
        }

        public FirstOrderMatrixException(string message)
            : base(message)
        {
        }

        public FirstOrderMatrixException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
