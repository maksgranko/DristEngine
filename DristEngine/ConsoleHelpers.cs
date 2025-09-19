namespace ConsoleApp1
{

    internal class ConsoleHelpers
    {
        public static void SetConsoleTitle(string title) => Console.Title = title;
        public static void UpdateConsoleSize()
        {
#pragma warning disable CA1416 // Проверка совместимости платформы
            if ((Console.WindowHeight != Console.BufferHeight) || (Console.WindowWidth != Console.BufferWidth))
            {
                try
                {
                    Console.SetCursorPosition(0, 0);
                    switch (Environment.OSVersion.Platform)
                    {
                        case PlatformID.Win32NT:
                            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
                            break;
                        default:
                            break;
                    }
                }
                catch { }
            }
#pragma warning restore CA1416 // Проверка совместимости платформы
        }
    }
}
