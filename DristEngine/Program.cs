using ConsoleApp1.Controllers;
using ConsoleApp1.Extensions;

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
