using System.Numerics;

namespace ConsoleApp1.Extensions
{
    public enum NumeralSystem
    {
        // Base
        NONE = 10,

        // Extended shortcuts
        BINARY = 2,
        OCTAL = 8,
        DECIMAL = 10,
        HEXADECIMAL = 16,

        // Shortcuts
        BIN = 2,
        OCT = 8,
        DEC = 10,
        HEX = 16,

    }
    public static class StringExtension
    {

        public static string ToNumberBase<T>(this T val, NumeralSystem numBase) where T : struct, IConvertible
        {
            return val switch
            {
                int intVal => Convert.ToString(intVal, (int)numBase),
                long longVal => Convert.ToString(longVal, (int)numBase),
                uint uintVal => Convert.ToString(uintVal, (int)numBase),
                ulong ulongVal when ulongVal <= long.MaxValue => Convert.ToString((long)ulongVal, (int)numBase),
                ulong ulongVal => ulongVal.ToString("X"),
                short shortVal => Convert.ToString(shortVal, (int)numBase),
                byte byteVal => Convert.ToString(byteVal, (int)numBase),
                _ => Convert.ToString(val.ToInt64(null), (int)numBase)
            };
        }

        public static string ToBin<T>(this T val) where T : struct, IConvertible
        {
            return val.ToNumberBase(NumeralSystem.BIN);
        }
        public static string ToOct<T>(this T val) where T : struct, IConvertible
        {
            return val.ToNumberBase(NumeralSystem.OCT);
        }
        public static string ToDec<T>(this T val) where T : struct, IConvertible
        {
            return val.ToNumberBase(NumeralSystem.DEC);
        }
        public static string ToHex<T>(this T val) where T : struct, IConvertible
        {
            return val.ToNumberBase(NumeralSystem.HEX);
        }

    }
}
