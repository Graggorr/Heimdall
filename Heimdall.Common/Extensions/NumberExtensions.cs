using static System.Convert;
using static System.Globalization.CultureInfo;
using static System.Math;
using static System.MidpointRounding;
using static System.String;

namespace Heimdall.Common.Extensions;

public static class NumberExtensions
{
    public static decimal Decimalize(this long value, int scale = 2)
    {
        scale = (int)Pow(10, scale);

        return ToDecimal(value) / scale;
    }
    
    public static decimal Decimalize(this int value, int scale = 2)
    {
        scale = (int)Pow(10, scale);

        return ToDecimal(value) / scale;
    }

    extension(decimal value)
    {
        public long ToLong(int decimals = 2)
        {
            value = Round(value, decimals, ToZero);
            var stringValue = value.ToString($"F{decimals}", CurrentCulture).Replace(".", "").TrimStart('0');

            return IsNullOrWhiteSpace(stringValue) || !long.TryParse(stringValue, out var result) 
                ? 0 
                : result;
        }

        public int ToInt(int decimals = 2)
        {
            value = Round(value, decimals, ToZero);
            var stringValue = value.ToString($"F{decimals}", CurrentCulture).Replace(".", "").TrimStart('0');

            return IsNullOrWhiteSpace(stringValue) || !int.TryParse(stringValue, out var result) 
                ? 0 
                : result;
        }
    }
}
