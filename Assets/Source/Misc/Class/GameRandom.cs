using System;

public static class GameRandom
{
    public class LinearConRng
    {
        private const long a = 25214903917;
        private const long c = 11;
        public        long seed;

        public LinearConRng(long seed)
        {
            if (seed < 0)
                throw new Exception("Bad seed");
            this.seed = seed;
        }

        private int next(int bits) // helper
        {
            seed = (seed * a + c) & ((1L << 48) - 1);
            return (int)(seed >> (48 - bits));
        }

        public double Next()
        {
            return (((long)next(26) << 27) + next(27)) / (double)(1L << 53);
        }
    }

    public static LinearConRng Random = new LinearConRng(0);

    public static float Range(float min, float max)
    {
        var range = max - min;
        return (float)Random.Next() * range + min;
    }
}