# Linear Algebra

A collection of linear algebra concepts implemented programmatically.

## Features

###Matrix

Math:
- Is matrix a:
  - zero matrix
  - square matrix
  - column vector
  - row vector
- Transposition
- Submatrix
- Matrix addition
- Scalar multiplication
- Matrix multiplication
- For square matricies:
  - Order
  - Trace
  - Minor
  - Algebraic complement
  - Adjugate
  - Determinant

Technical:
- Elements of the matrix can be of any number type (template type)
- Construction from:
  - a number of rows and columns
  - a rank
  - a multidimensional array
  - another matrix
- Implicit conversion to/from a multidimensional array
- `FillWithRandomNumbers` method
- Matrix comparison by each element (including overloaded equality operators)
- Overloaded addition and multiplication operators
- Matrix is enumerable
- Indexer
- Hash code is the same as underlying storage
- Conversion to a string

###Ranged Random

- A random int/double generator that outputs numbers in a certain range