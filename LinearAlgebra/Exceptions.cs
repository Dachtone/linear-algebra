using System;

namespace LinearAlgebra
{
    class InvalidMatrixDimensionsException : Exception
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
 

    class MatriciesAreNotOfTheSameSizeException : Exception
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

    class NumberOfColumnsDoesNotMatchNumberOfRowsException : Exception
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

    class NotASquareMatrixException : Exception
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

    class FirstOrderMatrixException : Exception
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
