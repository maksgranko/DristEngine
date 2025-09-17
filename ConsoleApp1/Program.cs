namespace ConsoleApp1
{
    internal class Program
    {
        private static InputListener il = new InputListener();
        private static (bool VerboseInput, bool, bool, bool) Debug = (true, false, false, false);
        static void Main(string[] args)
        {
            OnInit();
            OnInitLazy();
            GameLoop();
            OnExit();
        }


        private static void GameLoop()
        {
            UpdateConsoleSize();
            while (true)
            {
                UpdateConsoleSize();
                Thread.Sleep(500);
                Thread.Yield();
            }
        }

        private static void UpdateConsoleSize()
        {
            if ((Console.WindowHeight != Console.BufferHeight) || (Console.WindowWidth != Console.BufferWidth))
            {
                try
                {
                    Console.SetCursorPosition(0, 0);
                    Console.BufferWidth = Console.WindowWidth;
                    Console.BufferHeight = Console.WindowHeight;
                }
                catch { }
            }
        }

        private static void Il_OnKeyEvent(KeyEventArgs e)
        {
            if (Debug.VerboseInput)
            {
                Console.WriteLine(Colorist.Red(true, false) + $"\t{e.KeyDown}\t{e.RepeatCount}\t{e.ScanCode}\t{e.VirtualKeyCode}");
            }
        }

        private static void Ml_OnMouseEvent(MouseEventArgs e)
        {
            if (Debug.VerboseInput)
            {
                Console.WriteLine(Colorist.Green(true, false) + $"\t{e.LeftButtonPressed}\t{e.MiddleButtonPressed}\t{e.RightButtonPressed}");
                Console.WriteLine(Colorist.Green(true, false) + $"\t{e.MouseMoved}\t{e.MouseWheelDown}\t{e.MouseWheelUp}\t{e.X}\t{e.Y}");
            }
        }

        private static void OnInit()
        {
            il.Start();
            il.OnKeyEvent += Il_OnKeyEvent;
            il.OnMouseEvent += Ml_OnMouseEvent;
        }

        private static void OnInitLazy()
        {
        }
        private static void OnExit()
        {
            il.Stop();
            il.Dispose();
        }
    }
}
