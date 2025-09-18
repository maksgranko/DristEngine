using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DristEngine.Controllers.Utils
{
    internal class StickyKeys
    {
            [DllImport("user32.dll", SetLastError = true)]
            private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref STICKYKEYS pvParam, uint fWinIni);

            private const uint SPI_GETSTICKYKEYS = 0x003A;
            private const uint SPI_SETSTICKYKEYS = 0x003B;
            private const uint SKF_STICKYKEYSON = 0x00000001;

            [StructLayout(LayoutKind.Sequential)]
            public struct STICKYKEYS
            {
                public uint cbSize;
                public uint dwFlags;
            }

            public static void Disable()
            {
                STICKYKEYS sk = new STICKYKEYS();
                sk.cbSize = (uint)Marshal.SizeOf(sk);
                SystemParametersInfo(SPI_GETSTICKYKEYS, sk.cbSize, ref sk, 0);
                sk.dwFlags &= ~SKF_STICKYKEYSON; // Выключаем
                SystemParametersInfo(SPI_SETSTICKYKEYS, sk.cbSize, ref sk, 0);
                Console.WriteLine("StickyKeys отключены");
            }
        }
    }