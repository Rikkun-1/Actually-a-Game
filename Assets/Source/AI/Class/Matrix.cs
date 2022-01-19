using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Matrix
{
    private readonly int[,] _matrix;
    public readonly  int    height;
    public readonly  int    width;

    public Matrix(int[,] matrix)
    {
        _matrix = matrix;
        width   = matrix.GetLength(0);
        height  = matrix.GetLength(1);
    }

    public Matrix(int width, int height)
    {
        _matrix     = new int[width, height];
        this.width  = width;
        this.height = height;
    }

    public Matrix(int width, int height, int initialValue) : this(width, height)
    {
        _matrix = _matrix.Fill(initialValue);
    }

    public int this[int x, int y]
    {
        get => _matrix[x, y];
        set => _matrix[x, y] = value;
    }

    public void Loop(Action<int, int> act)
    {
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                act(x, y);
            }
        }
    }

    public Matrix ForEach(Func<int, int> func)
    {
        return new Matrix(_matrix.Select(func));
    }

    public Matrix ForEach(Func<int, int, int, int> func)
    {
        return new Matrix(_matrix.Select(func));
    }

    public Matrix ForEachPair(Matrix second, Func<int, int, int> func)
    {
        if (width != second.width || height != second.height)
        {
            throw new ArgumentException("Matrices should have same size");
        }

        var result = new Matrix(width, height);

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                result[x, y] = func(this[x, y], second[x, y]);
            }
        }

        return result;
    }

    public (int x, int y, int value) Min()
    {
        return Min(value => true);
    }

    public (int x, int y, int value) Max()
    {
        return Max(value => true);
    }

    public (int x, int y, int value) Min(Func<int, bool> filter)
    {
        var minX = 0;
        var minY = 0;
        var min  = int.MaxValue;

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var elem = _matrix[x, y];
                if (elem < min && filter(elem))
                {
                    minX = x;
                    minY = y;
                    min  = elem;
                }
            }
        }

        return (minX, minY, min);
    }

    public (int x, int y, int value) Max(Func<int, bool> filter)
    {
        var minX = 0;
        var minY = 0;
        var max  = int.MinValue;

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var elem = _matrix[x, y];
                if (elem > max && filter(elem))
                {
                    minX = x;
                    minY = y;
                    max  = elem;
                }
            }
        }

        return (minX, minY, max);
    }

    public static int GetMaximumOfAll(IEnumerable<Matrix> matrices)
    {
        var maxes = matrices.Where(matrix => (object)matrix != null)
                            .Select(matrix => matrix.Max().value);
        return maxes.Max();
    }

    public int[,] ToIntArray()
    {
        return _matrix.DeepCopy2DArray();
    }

    public override string ToString()
    {
        var result = new StringBuilder("\n");

        for (var x = 0; x < width; x++)
        {
            result.Append("[");
            for (var y = 0; y < height; y++)
            {
                result.AppendFormat(_matrix[x, y].ToString().PadLeft(4));
            }

            result.Append("]\n");
        }

        return result.ToString();
    }

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    public bool Equals(Matrix other)
    {
        return Equals(_matrix, other._matrix) && width == other.width && height == other.height;
    }

    public override bool Equals(object obj)
    {
        return obj is Matrix other && Equals(other);
    }


    #region Operators

    public static Matrix operator !(Matrix matrix)
    {
        return matrix.ForEach(x => x > 0 ? 0 : 1);
    }

    public static Matrix operator ++(Matrix first)
    {
        return first.ForEach(x => x + 1);
    }

    public static Matrix operator --(Matrix first)
    {
        return first.ForEach(x => x - 1);
    }


    public static Matrix operator +(Matrix first, int scalar)
    {
        return first.ForEach(x => x + scalar);
    }

    public static Matrix operator +(int scalar, Matrix first)
    {
        return first.ForEach(x => x + scalar);
    }

    public static Matrix operator +(Matrix first, Matrix second)
    {
        return first.ForEachPair(second, (x1, x2) => x1 + x2);
    }


    public static Matrix operator -(Matrix first)
    {
        return first.ForEach(x => -x);
    }

    public static Matrix operator -(Matrix first, int scalar)
    {
        return first.ForEach(x => x - scalar);
    }

    public static Matrix operator -(int scalar, Matrix first)
    {
        return first.ForEach(x => x - scalar);
    }

    public static Matrix operator -(Matrix first, Matrix second)
    {
        return first.ForEachPair(second, (x1, x2) => x1 - x2);
    }


    public static Matrix operator *(Matrix first, Matrix second)
    {
        return first.ForEachPair(second, (x1, x2) => x1 * x2);
    }

    public static Matrix operator *(Matrix matrix, float scalar)
    {
        return matrix.ForEach(x => (int)(x * scalar));
    }

    public static Matrix operator *(float scalar, Matrix matrix)
    {
        return matrix.ForEach(x => (int)(x * scalar));
    }

    public static Matrix operator /(Matrix first, Matrix second)
    {
        return first.ForEachPair(second, (x1, x2) => x1 / x2);
    }

    public static Matrix operator /(Matrix matrix, float scalar)
    {
        return matrix.ForEach(x => (int)(x / scalar));
    }

    public static Matrix operator /(float scalar, Matrix matrix)
    {
        return matrix.ForEach(x => (int)(scalar / x));
    }

    public static Matrix operator ==(Matrix first, Matrix second)
    {
        return first?.ForEachPair(second, (x1, x2) => x1 == x2 ? 1 : 0);
    }

    public static Matrix operator !=(Matrix first, Matrix second)
    {
        return first?.ForEachPair(second, (x1, x2) => x1 != x2 ? 1 : 0);
    }


    public static Matrix operator >(Matrix first, int scalar)
    {
        return first.ForEach(x => x > scalar ? 1 : 0);
    }

    public static Matrix operator <(Matrix first, int scalar)
    {
        return first.ForEach(x => x < scalar ? 1 : 0);
    }

    public static Matrix operator >(Matrix first, Matrix second)
    {
        return first.ForEachPair(second, (x1, x2) => x1 > x2 ? 1 : 0);
    }

    public static Matrix operator <(Matrix first, Matrix second)
    {
        return first.ForEachPair(second, (x1, x2) => x1 < x2 ? 1 : 0);
    }


    public static Matrix operator >=(Matrix first, int scalar)
    {
        return first.ForEach(x => x >= scalar ? 1 : 0);
    }

    public static Matrix operator <=(Matrix first, int scalar)
    {
        return first.ForEach(x => x <= scalar ? 1 : 0);
    }


    public static Matrix operator >=(Matrix first, Matrix second)
    {
        return first.ForEachPair(second, (x1, x2) => x1 >= x2 ? 1 : 0);
    }

    public static Matrix operator <=(Matrix first, Matrix second)
    {
        return first.ForEachPair(second, (x1, x2) => x1 <= x2 ? 1 : 0);
    }

    #endregion
}