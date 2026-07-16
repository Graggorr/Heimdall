using static System.Math;
using static System.MidpointRounding;

namespace Heimdall.Abstractions.Extensions;

public static class NumberExtensions
{
    public static decimal Decimalize(this long value, int scale = 2) => value / Scale(scale);

    public static decimal Decimalize(this int value, int scale = 2) => value / Scale(scale);

    extension(decimal value)
    {
        public long ToLong(int decimals = 2) => (long)(Round(value, decimals, ToZero) * Scale(decimals));

        public int ToInt(int decimals = 2) => (int)(Round(value, decimals, ToZero) * Scale(decimals));
    }

    private static decimal Scale(int decimals) => (decimal)Pow(10, decimals);
}
