namespace ConsoleApp1
{
    internal class Colorist
    {
        private static readonly string[] ForegroundColors = new string[]
    {
        "\x1b[30m",                          // Black
        "\x1b[31m",                          // Red
        "\x1b[32m",                          // Green
        "\x1b[33m",                          // Yellow
        "\x1b[34m",                          // Blue
        "\x1b[35m",                          // Magenta
        "\x1b[36m",                          // Cyan
        "\x1b[37m",                          // White
        "\x1b[90m",                          // Bright Black (DarkGray)
        "\x1b[91m",                          // Bright Red
        "\x1b[92m",                          // Bright Green
        "\x1b[93m",                          // Bright Yellow
        "\x1b[94m",                          // Bright Blue
        "\x1b[95m",                          // Bright Magenta
        "\x1b[96m",                          // Bright Cyan
        "\x1b[97m"                           // Bright White
    };
        // ANSI коды цветов (background)
        private static readonly string[] BackgroundColors = new string[]
        {
        "\x1b[40m",                         // Black
        "\x1b[41m",                         // Red
        "\x1b[42m",                         // Green
        "\x1b[43m",                         // Yellow
        "\x1b[44m",                         // Blue
        "\x1b[45m",                         // Magenta
        "\x1b[46m",                         // Cyan
        "\x1b[47m",                         // White
        "\x1b[100m",                         // Bright Black (DarkGray)
        "\x1b[101m",                         // Bright Red
        "\x1b[102m",                         // Bright Green
        "\x1b[103m",                         // Bright Yellow
        "\x1b[104m",                         // Bright Blue
        "\x1b[105m",                         // Bright Magenta
        "\x1b[106m",                         // Bright Cyan
        "\x1b[107m"                          // Bright White
        };
        public enum Color
        {
            Black = 0,
            Red = 1,
            Green = 2,
            Yellow = 3,
            Blue = 4,
            Magenta = 5,
            Cyan = 6,
            White = 7,
            DarkGray = 8,
            BrightRed = 9,
            BrightGreen = 10,
            BrightYellow = 11,
            BrightBlue = 12,
            BrightMagenta = 13,
            BrightCyan = 14,
            BrightWhite = 15
        }
        // Метод для генерации ANSI кода цвета с флагами изменения fg/bg
        public static string SetColor(Color color, bool fgChange, bool bgChange)
        {
            string fgCode = fgChange ? ForegroundColors[(int)color] : "";
            string bgCode = bgChange ? BackgroundColors[(int)color] : "";
            return fgCode + bgCode;
        }


        public static string Black(bool fgChange = true, bool bgChange = false) => SetColor(Color.Black, fgChange, bgChange);
        public static string Red(bool fgChange = true, bool bgChange = false) => SetColor(Color.Red, fgChange, bgChange);
        public static string Green(bool fgChange = true, bool bgChange = false) => SetColor(Color.Green, fgChange, bgChange);
        public static string Yellow(bool fgChange = true, bool bgChange = false) => SetColor(Color.Yellow, fgChange, bgChange);
        public static string Blue(bool fgChange = true, bool bgChange = false) => SetColor(Color.Blue, fgChange, bgChange);
        public static string Magenta(bool fgChange = true, bool bgChange = false) => SetColor(Color.Magenta, fgChange, bgChange);
        public static string Cyan(bool fgChange = true, bool bgChange = false) => SetColor(Color.Cyan, fgChange, bgChange);
        public static string White(bool fgChange = true, bool bgChange = false) => SetColor(Color.White, fgChange, bgChange);
        public static string DarkGray(bool fgChange = true, bool bgChange = false) => SetColor(Color.DarkGray, fgChange, bgChange);
        public static string BrightRed(bool fgChange = true, bool bgChange = false) => SetColor(Color.BrightRed, fgChange, bgChange);
        public static string BrightGreen(bool fgChange = true, bool bgChange = false) => SetColor(Color.BrightGreen, fgChange, bgChange);
        public static string BrightYellow(bool fgChange = true, bool bgChange = false) => SetColor(Color.BrightYellow, fgChange, bgChange);
        public static string BrightBlue(bool fgChange = true, bool bgChange = false) => SetColor(Color.BrightBlue, fgChange, bgChange);
        public static string BrightMagenta(bool fgChange = true, bool bgChange = false) => SetColor(Color.BrightMagenta, fgChange, bgChange);
        public static string BrightCyan(bool fgChange = true, bool bgChange = false) => SetColor(Color.BrightCyan, fgChange, bgChange);
        public static string BrightWhite(bool fgChange = true, bool bgChange = false) => SetColor(Color.BrightWhite, fgChange, bgChange);
        public static string Reset => "\x1b[0m";
    }
}
