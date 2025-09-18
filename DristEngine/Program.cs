using ConsoleApp1.Controllers;
using ConsoleApp1.Extensions;
using DristEngine.Controllers.Utils;

namespace ConsoleApp1
{
    internal class Program
    {
        private static (bool VerboseInput, bool, bool, bool) Debug = (true, false, false, false);
        private static ExtendedInput? EInput;
        static void Main(string[] args)
        {            
            OnInit();
            OnInitLazy();
            GameLoop();
            OnExit();
        }


        private static void GameLoop()
        {
            while (true)
            {
                ConsoleHelpers.UpdateConsoleSize();
                Thread.Sleep(500);
                Thread.Yield();
            }
        }

        private static void OnInit()
        {
            Console.CancelKeyPress += (s, e) => e.Cancel = true;
            Console.TreatControlCAsInput = false;
            StickyKeys.Disable();
        }
        
        private static void OnInitLazy()
        {
            if(EInput == null) EInput = new(Debug.VerboseInput);
            EInput.Start();
        }
        private static void OnExit()
        {
            EInput?.Stop();
            EInput?.Dispose();
        }
    }
}
