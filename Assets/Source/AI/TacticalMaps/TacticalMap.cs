using System;
using System.Text;

public readonly struct TacticalMap
{
    private readonly int[,] _map;
    public readonly  int    width;
    public readonly  int    height;

    public int this[int i, int j]
    {
        get => _map[i, j];
        set => _map[i, j] = value;
    }

    public TacticalMap(int[,] map)
    {
        _map   = map;
        height = map.GetLength(0);
        width  = map.GetLength(1);
    }

    public TacticalMap(int width, int height)
    {
        _map        = new int[width, height];
        this.width  = width;
        this.height = height;
    }

    public TacticalMap ForEach(Func<int, int> func)
    {
        //int[,] result = new int[_height, _width];
        //result = (int[,])_map.Clone();
        //Array.Copy(_map, result, _map.Length);

        var result = new TacticalMap(width, height);

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                result[i, j] = func(this[i, j]);
            }
        }

        return result;
    }

    public TacticalMap ForEachPair(TacticalMap second, Func<int, int, int> func)
    {
        var result = new TacticalMap(width, height);

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                result[i, j] = func(this[i, j], second[i, j]);
            }
        }

        return result;
    }

    public (int x, int y, int value) Min(Func<int, bool> filter)
    {
        var x   = 0;
        var y   = 0;
        var min = int.MaxValue;

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                var elem = _map[i, j];
                if (elem < min && filter(elem))
                {
                    x   = i;
                    y   = j;
                    min = _map[i, j];
                }
            }
        }

        return (x, y, min);
    }

    public (int x, int y, int value) Max(Func<int, bool> filter)
    {
        var x   = 0;
        var y   = 0;
        var max = int.MinValue;

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                var elem = _map[i, j];
                if (elem > max && filter(elem))
                {
                    x   = i;
                    y   = j;
                    max = _map[i, j];
                }
            }
        }

        return (x, y, max);
    }

    public int[,] ToIntArray() => (int[,])_map.Clone();

    public TacticalMap Clone() => new TacticalMap((int[,])_map.Clone());

    public static TacticalMap operator !(TacticalMap tacticalMap) => tacticalMap.ForEach(x => x > 0 ? 0 : 1);


    public static TacticalMap operator ++(TacticalMap first) => first.ForEach(x => x + 1);

    public static TacticalMap operator --(TacticalMap first) => first.ForEach(x => x - 1);


    public static TacticalMap operator +(TacticalMap first, int scalar) => first.ForEach(x => x + scalar);

    public static TacticalMap operator +(int scalar, TacticalMap first) => first.ForEach(x => x + scalar);

    public static TacticalMap operator +(TacticalMap first, TacticalMap second) => first.ForEachPair(second, (x1, x2) => x1 + x2);


    public static TacticalMap operator -(TacticalMap first) => first.ForEach(x => -x);

    public static TacticalMap operator -(TacticalMap first, int scalar) => first.ForEach(x => x - scalar);

    public static TacticalMap operator -(int scalar, TacticalMap first) => first.ForEach(x => x - scalar);

    public static TacticalMap operator -(TacticalMap first, TacticalMap second) => first.ForEachPair(second, (x1, x2) => x1 - x2);


    public static TacticalMap operator *(TacticalMap first, TacticalMap second) => first.ForEachPair(second, (x1, x2) => x1 * x2);

    public static TacticalMap operator *(TacticalMap tacticalMap, double scalar) => tacticalMap.ForEach(x => (int)(x * scalar));

    public static TacticalMap operator *(double scalar, TacticalMap tacticalMap) => tacticalMap.ForEach(x => (int)(x * scalar));


    public static TacticalMap operator ==(TacticalMap first, TacticalMap second) => first.ForEachPair(second, (x1, x2) => x1 == x2 ? 1 : 0);

    public static TacticalMap operator !=(TacticalMap first, TacticalMap second) => first.ForEachPair(second, (x1, x2) => x1 != x2 ? 1 : 0);


    public static TacticalMap operator >(TacticalMap first, int scalar) => first.ForEach(x => x > scalar ? 1 : 0);

    public static TacticalMap operator <(TacticalMap first, int scalar) => first.ForEach(x => x < scalar ? 1 : 0);

    public static TacticalMap operator >(TacticalMap first, TacticalMap second) => first.ForEachPair(second, (x1, x2) => x1 > x2 ? 1 : 0);

    public static TacticalMap operator <(TacticalMap first, TacticalMap second) => first.ForEachPair(second, (x1, x2) => x1 < x2 ? 1 : 0);


    public static TacticalMap operator >=(TacticalMap first, int scalar) => first.ForEach(x => x >= scalar ? 1 : 0);

    public static TacticalMap operator <=(TacticalMap first, int scalar) => first.ForEach(x => x <= scalar ? 1 : 0);


    public static TacticalMap operator >=(TacticalMap first, TacticalMap second) => first.ForEachPair(second, (x1, x2) => x1 >= x2 ? 1 : 0);

    public static TacticalMap operator <=(TacticalMap first, TacticalMap second) => first.ForEachPair(second, (x1, x2) => x1 <= x2 ? 1 : 0);


    public override int GetHashCode()
    {
        var tacticalMapString = new StringBuilder();
        tacticalMapString.Append(height).Append("x").Append(width).Append("=");
        for (var row = 0; row < height; row++)
        {
            for (var col = 0; col < width; col++)
            {
                tacticalMapString.Append(this[row, col]).Append(";");
            }
        }

        return tacticalMapString.ToString().GetHashCode();
    }

    public bool Equals(TacticalMap other) => Equals(_map, other._map) && width == other.width && height == other.height;

    public override bool Equals(object obj) => obj is TacticalMap other && Equals(other);
}