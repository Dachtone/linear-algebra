using System;
using System.Collections;
using System.Text;

namespace LinearAlgebra
{
    public class Matrix<T> : IEnumerable, IEquatable<Matrix<T>>
        where T : IConvertible
    {
        #region Fields and Properties

        // Dimensions of the matrix
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        // Internal storage
        T[,] storage;

        #endregion

        #region Constructors

        // Constructor
        public Matrix(int rows, int columns)
        {
            if (rows < 1 || columns < 1)
                throw new InvalidMatrixDimensionsException("A matrix must have at least one element.");

            Rows = rows;
            Columns = columns;

            // Allocate storage
            storage = new T[Rows, Columns];
        }

        public Matrix(int order)
        {
            if (order < 1)
                throw new InvalidMatrixDimensionsException("A matrix must have at least one element.");

            Rows = order;
            Columns = order;

            // Allocate storage
            storage = new T[Rows, Columns];
        }

        // Constructor from an array
        public Matrix(T[,] array)
        {
            InitializeFromArray(array);
        }

        public Matrix(Matrix<T> matrix)
        {
            InitializeFromArray(matrix.storage);
        }

        // Private constructor for internal use
        Matrix() { }

        #endregion

        #region Math

        /// <summary>
        /// Total number of elements in the matrix.
        /// </summary>
        public int Count
        {
            get { return Rows * Columns; }
        }

        /// <summary>
        /// Is this a zero matrix.
        /// </summary>
        public bool IsZero
        {
            get
            {
                foreach (T element in this)
                {
                    if (element != (0 as dynamic))
                        return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Returns whether the matrix is square.
        /// </summary>
        public bool IsSquare
        {
            get { return (Rows == Columns); }
        }

        /// <summary>
        /// Returns whether the matrix is a vector.
        /// </summary>
        public bool IsVector
        {
            get { return IsColumnVector || IsRowVector; }
        }

        /// <summary>
        /// Returns whether the matrix is a column vector.
        /// </summary>
        public bool IsColumnVector
        {
            get { return (Columns == 1); }
        }

        /// <summary>
        /// Returns whether the matrix is row vector.
        /// </summary>
        public bool IsRowVector
        {
            get { return (Rows == 1); }
        }

        /// <summary>
        /// Transposes the matrix.
        /// </summary>
        /// <returns>The matrix with the transposition applied.</returns>
        public Matrix<T> Transpose()
        {
            T[,] newStorage = new T[Columns, Rows];

            int position = 0;
            foreach (T element in this)
            {
                // Switch the row and the column
                newStorage[position % Columns, position / Columns] = element;
                position++;
            }

            int numberOfRows = Rows;
            Rows = Columns;
            Columns = numberOfRows;

            storage = newStorage;

            return this;
        }

        /// <summary>
        /// Returns the transposition of the matrix without affecting the original state.
        /// </summary>
        public Matrix<T> Transposition
        {
            get { return new Matrix<T>(this).Transpose(); }
        }

        /// <summary>
        /// Creates a new matrix by removing the specified row and column.
        /// </summary>
        /// <param name="row">A row to remove.</param>
        /// <param name="column">A column to remove.</param>
        /// <returns>The submatrix.</returns>
        public Matrix<T> Submatrix(int row, int column)
        {
            // Submatrix with an order of one less
            Matrix<T> subMatrix = new Matrix<T>(Rows - 1, Columns - 1);

            int position = 0;
            for (int traversingRow = 0; traversingRow < Rows; traversingRow++)
            {
                if (traversingRow == row)
                    continue;

                for (int traversingColumn = 0; traversingColumn < Columns; traversingColumn++)
                {
                    if (traversingColumn == column)
                        continue;

                    subMatrix[position / subMatrix.Columns, position % subMatrix.Columns]
                        = this[traversingRow, traversingColumn];
                    position++;
                }
            }

            return subMatrix;
        }

        /// <summary>
        /// Matrix addition.
        /// </summary>
        /// <param name="matrix">A matrix to add to this matrix.</param>
        /// <returns>The matrix with the addition applied.</returns>
        public Matrix<T> Add(Matrix<T> matrix)
        {
            if (matrix.Rows != Rows || matrix.Columns != Columns)
                throw new MatriciesAreNotOfTheSameSizeException("Addition can only be applied to matricies of the same size.");

            for (int i = 0; i < Count; i++)
            {
                int row = i / Columns;
                int column = i % Columns;

                // Refer to the element as dynamic to perform a mathematical operation with generic operands
                var referenceToElement = this[row, column] as dynamic;
                this[row, column] = referenceToElement + matrix[row, column];
            }

            return this;
        }

        /// <summary>
        /// Scalar multiplication of the matrix by a specified factor.
        /// </summary>
        /// <param name="factor">A factor to scale the matrix by.</param>
        /// <returns>The matrix with the multiplication applied.</returns>
        public Matrix<T> Multiply(T factor)
        {
            foreach (ref T element in this)
            {
                // Refer to the element as dynamic to perform a mathematical operation with generic operands
                var referenceToElement = element as dynamic;
                element = referenceToElement * factor;
            }

            return this;
        }

        /// <summary>
        /// Matrix multiplication.
        /// </summary>
        /// <param name="matrix">A matrix to multiply with this matrix.</param>
        /// <returns>The product matrix.</returns>
        public Matrix<T> Multiply(Matrix<T> matrix)
        {
            if (Columns != matrix.Rows)
            {
                throw new NumberOfColumnsDoesNotMatchNumberOfRowsException(
                    "Multiplication can only be applied if the number of columns of the first matrix is equal to the number of rows of the second matrix."
                );
            }

            Matrix<T> product = new Matrix<T>(Rows, matrix.Columns);
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < matrix.Columns; column++)
                {
                    for (int element = 0; element < Columns; element++)
                    {
                        // Refer to the element as dynamic to perform a mathematical operation with generic operands
                        var referenceToElement = this[row, element] as dynamic;
                        product[row, column] += referenceToElement * matrix[element, column];
                    }
                }
                
            }
               
            // This matrix now represents the product
            ShallowCopy(product);

            return this;
        }

        #region Square Matrix

        /// <summary>
        /// The order of the matrix. Applies only to square matricies.
        /// </summary>
        public int Order
        {
            get
            {
                if (!IsSquare)
                    throw new NotASquareMatrixException("Only square matricies have an order.");

                return Rows;
            }
        }

        /// <summary>
        /// The trace (sum of elements on the main diagonal) of the matrix.
        /// </summary>
        public T Trace
        {
            get
            {
                if (!IsSquare)
                    throw new NotASquareMatrixException("Only square matricies have a trace.");

                // Initialize with zero
                T sum = (T)Convert.ChangeType(0, typeof(T));

                for (int diagonal = 0; diagonal < Order; diagonal++)
                {
                    sum += (this[diagonal, diagonal] as dynamic);
                }

                return sum;
            }
        }

        /// <summary>
        /// Calculate the minor of a specified element in the matrix.
        /// </summary>
        /// <param name="row">The row of the element.</param>
        /// <param name="column">The column of the element.</param>
        /// <returns>The minor of the element.</returns>
        public double Minor(int row, int column)
        {
            if (!IsSquare)
                throw new NotASquareMatrixException("Only elements of square matricies have minors.");

            if (Order <= 1)
                throw new FirstOrderMatrixException("Elements of the first order matricies don't have a minor.");

            return Submatrix(row, column).Determinant;
        }

        /// <summary>
        /// Calculate the algebraic complement of a specified element in the matrix.
        /// </summary>
        /// <param name="row">The row of the element.</param>
        /// <param name="column">The column of the element.</param>
        /// <returns>The algebraic complement of the element.</returns>
        public double AlgebraicComplement(int row, int column)
        {
            if (!IsSquare)
                throw new NotASquareMatrixException("Only elements of square matricies have algebraic complements.");

            // +Minor if (row + column) is even, -Minor otherwise
            return ((row + column) % 2 == 0 ? 1 : -1) * Minor(row, column);
        }

        public Matrix<T> Adjugate
        {
            get
            {
                Matrix<T> adjugate = new Matrix<T>(this.Order);
                int row = 0, column = 0;
                foreach (T value in this)
                {
                    adjugate[row, column] = AlgebraicComplement(row, column) as dynamic;

                    if (column == Columns - 1)
                    {
                        column = 0;
                        row++;
                    }
                    else
                    {
                        column++;
                    }
                }

                return adjugate;
            }
        }

        /// <summary>
        /// Calculates the determinant.
        /// </summary>
        public double Determinant
        {
            get
            {
                if (!IsSquare)
                    throw new NotASquareMatrixException("Only square matricies have a determinant.");

                if (Order == 1)
                    return (double)Convert.ChangeType(this[0, 0], typeof(double));

                if (Order == 2)
                {
                    // "\ minus /"
                    // Casting as dynamic to allow the multiplication operator
                    return (this[0, 0] as dynamic) * this[1, 1] - (this[0, 1] as dynamic) * this[1, 0];
                }

                // Calculate the determinant with algebraic complements
                double determinant = 0.0;

                // Go through the first row
                for (int position = 0; position < Columns; position++)
                {
                    determinant += (this[0, position] as dynamic) * AlgebraicComplement(0, position);
                }

                return determinant;
            }
        }

        #endregion

        #endregion

        #region Technical Implementation

        /// <summary>
        /// Total number of elements in the matrix.
        /// </summary>
        public int Length
        {
            get { return Rows * Columns; }
        }

        #region Addition Operator

        public static Matrix<T> operator +(Matrix<T> firstMatrix, Matrix<T> secondMatrix)
        {
            return new Matrix<T>(firstMatrix).Add(secondMatrix);
        }

        #endregion

        #region Scalar Multiplication Operator
        
        public static Matrix<T> operator *(T factor, Matrix<T> matrix)
        {
            return new Matrix<T>(matrix).Multiply(factor);
        }

        public static Matrix<T> operator *(Matrix<T> matrix, T factor)
        {
            return new Matrix<T>(matrix).Multiply(factor);
        }

        #endregion

        #region Multiplication Operator

        public static Matrix<T> operator *(Matrix<T> firstMatrix, Matrix<T> secondMatrix)
        {
            return new Matrix<T>(firstMatrix).Multiply(secondMatrix);
        }

        #endregion

        /// <summary>
        /// Checks whether an object is identical to the matrix.
        /// </summary>
        /// <param name="other">The object to compare to.</param>
        /// <returns>True if the object is identical to the matrix, false otherwise.</returns>
        public override bool Equals(object other)
        {
            if (other == null || other.GetType() != GetType())
                return false;

            // Cast to matrix and compare
            return Equals((Matrix<T>)other);
        }

        /// <summary>
        /// Checks whether two matricies are identical.
        /// </summary>
        /// <param name="matrix">The matrix to compare to.</param>
        /// <returns>True if matricies are identical, false otherwise.</returns>
        public bool Equals(Matrix<T> matrix)
        {
            if (matrix.Rows != Rows || matrix.Columns != Columns)
                return false;

            // Compare every element
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    if ((matrix[row, column] as dynamic) != this[row, column])
                        return false;
                }
            }

            return true;
        }

        #region Equality Operators

        public static bool operator==(Matrix<T> firstMatrix, Matrix<T> secondMatrix)
        {
            return firstMatrix.Equals(secondMatrix);
        }
        
        public static bool operator!=(Matrix<T> firstMatrix, Matrix<T> secondMatrix)
        {
            return !firstMatrix.Equals(secondMatrix);
        }

        #endregion

        /// <summary>
        /// Retrieves a hash code for this object.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return storage.GetHashCode();
        }

        // Conversion from an array
        public static implicit operator Matrix<T>(T[,] array)
        {
            Matrix<T> matrix = new Matrix<T>();
            matrix.InitializeFromArray(array);

            return matrix;
        }

        void InitializeFromArray(T[,] array)
        {
            // Get information about the dimensions
            Rows = array.GetLength(0);
            Columns = array.GetLength(1);

            // Allocate storage
            storage = new T[Rows, Columns];

            // Copy to internal storage
            Array.Copy(array, storage, array.Length);
        }

        void ShallowCopy(Matrix<T> matrix)
        {
            Rows = matrix.Rows;
            Columns = matrix.Columns;

            storage = matrix.storage;
        }

        // Indexer
        public T this[int row, int column]
        {
            get { return storage[row, column]; }
            set { storage[row, column] = value; }
        }

        // Random filler
        // The Random object should be instantiated only once
        static readonly RangedRandom random = new RangedRandom();

        /// <summary>
        /// Fills the matrix with random values from a specified range.
        /// </summary>
        /// <param name="lowerBound">Minimum value.</param>
        /// <param name="upperBound">Maximum value.</param>
        public void FillWithRandomNumbers(double lowerBound, double upperBound)
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    // Assign a random number
                    // The generated number is of type double to have full support for all numerics,
                    // since double is easily converted to other types with some obvious data losses
                    storage[row, column] =
                        (T)Convert.ChangeType(random.GenerateDouble(lowerBound, upperBound), typeof(T));
                }
            }
        }

        // Conversion to an array
        public static implicit operator T[,](Matrix<T> matrix)
        {
            return matrix.storage;
        }

        // Conversion into a string
        public override string ToString()
        {
            var builder = new StringBuilder();

            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    builder.Append(storage[row, column]);

                    if (column < Columns - 1)
                        builder.Append(", ");
                }

                if (row < Rows - 1)
                    builder.Append(Environment.NewLine);
            }

            return builder.ToString();
        }

        // Get the enumerator
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public MatrixEnumerator GetEnumerator()
        {
            return new MatrixEnumerator(this);
        }

        // Enumerator class
        public class MatrixEnumerator : IEnumerator
        {
            int rows;
            int columns;

            T[,] storage;

            // Current position index,
            // starts before the first element
            int position = -1;

            // Constructor from a matrix instance
            public MatrixEnumerator(Matrix<T> matrix)
            {
                rows = matrix.Rows;
                columns = matrix.Columns;

                // Not a copy
                storage = matrix.storage;
            }

            public bool MoveNext()
            {
                position++;
                return (position < storage.Length);
            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public ref T Current
            {
                get
                {
                    try
                    {
                        return ref storage[position / columns, position % columns];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }

        #endregion
    }
}
