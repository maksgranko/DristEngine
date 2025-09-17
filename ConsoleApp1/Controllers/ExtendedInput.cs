using ConsoleApp1.Extensions;
using System.Linq;

namespace ConsoleApp1.Controllers
{
    internal class ExtendedInput : InputListener
    {
        private static (bool VerboseInput, bool) Debug = (true, false);
        private static Key[] KeysBuf = new Key[3];
        private static byte ModCodeBuf = new byte();

        //public static ModCode[] GetCurrentMods(byte ModFlags)
        //{
            //ModCode[] ModCodeBuf = new ModCode[8];
        //}

        public ExtendedInput(bool VerboseInput)
        {
            Debug.VerboseInput = VerboseInput;
            base.OnKeyEvent += Il_OnKeyEvent;
            base.OnMouseEvent += Ml_OnMouseEvent;
        }


        private static void Il_OnKeyEvent(KeyEventArgs e)
        {
            int index = Array.FindIndex(KeysBuf, k => (byte)k.KeyCode == e.VirtualKeyCode);
            if (index != -1)
            {
                KeysBuf[index].LongPressed = true;
            }
            else {
                Array.ForEach(KeysBuf, k =>
                {
                    if (k.Equals(Key.Default)) k.KeyCode = (KeyCode)e.VirtualKeyCode;
                    return;
                });
            }


            if (Debug.VerboseInput)
            {
                Console.WriteLine(Colorist.Red(true, false) + $"\t{e.KeyDown}\t{e.ScanCode}\t{e.VirtualKeyCode}\t{e.UnicodeChar}\t{e.ControlKeyState}");
                Console.WriteLine(Colorist.Red(true, false) + $"\t{e.ControlKeyState.ToBin()}");
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
    }

    public struct Key
    {
        public KeyCode KeyCode;
        public bool LongPressed;

        public Key(KeyCode keyCode, bool longPressed)
        {
            KeyCode = keyCode;
            LongPressed = longPressed;
        }
        public bool Equals(Key other)
        {
            return KeyCode == other.KeyCode && LongPressed == other.LongPressed;
        }
        public bool Equals(KeyCode other)
        {
            return KeyCode == other;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (!(obj is Key other)) return false;
            return Equals(other);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)KeyCode * 397) ^ LongPressed.GetHashCode();
            }
        }
        public static bool operator ==(Key key, KeyCode keyCode)
        {
            return key.KeyCode == keyCode;
        }
        public static bool operator !=(Key key, KeyCode keyCode)
        {
            return key.KeyCode != keyCode;
        }

        public static Key Default => new Key(KeyCode.None, false);
    }

    public enum KeyCode
    {
        None = 0,
        Backspace = 8,
        Tab = 9,
        Clear = 12,
        Enter = 13,
        Shift = 16,
        Ctrl = 17,
        Alt = 18,
        PauseBreak = 19,
        CapsLock = 20,
        Escape = 27,
        Space = 32,
        PageUp = 33,
        PageDown = 34,
        End = 35,
        Home = 36,
        LeftArrow = 37,
        UpArrow = 38,
        RightArrow = 39,
        DownArrow = 40,
        PrintScreen = 44,
        Insert = 45,
        Delete = 46,
        Digit0 = 48,
        Digit1 = 49,
        Digit2 = 50,
        Digit3 = 51,
        Digit4 = 52,
        Digit5 = 53,
        Digit6 = 54,
        Digit7 = 55,
        Digit8 = 56,
        Digit9 = 57,
        KeyA = 65,
        KeyB = 66,
        KeyC = 67,
        KeyD = 68,
        KeyE = 69,
        KeyF = 70,
        KeyG = 71,
        KeyH = 72,
        KeyI = 73,
        KeyJ = 74,
        KeyK = 75,
        KeyL = 76,
        KeyM = 77,
        KeyN = 78,
        KeyO = 79,
        KeyP = 80,
        KeyQ = 81,
        KeyR = 82,
        KeyS = 83,
        KeyT = 84,
        KeyU = 85,
        KeyV = 86,
        KeyW = 87,
        KeyX = 88,
        KeyY = 89,
        KeyZ = 90,
        Meta = 91,
        R_Meta = 92,
        Menu = 93,
        Numpad0 = 96,
        Numpad1 = 97,
        Numpad2 = 98,
        Numpad3 = 99,
        Numpad4 = 100,
        Numpad5 = 101,
        Numpad6 = 102,
        Numpad7 = 103,
        Numpad8 = 104,
        Numpad9 = 105,
        Multiply = 106,
        Add = 107,
        Subtract = 109,
        DecimalPoint = 110,
        Divide = 111,
        F1 = 112,
        F2 = 113,
        F3 = 114,
        F4 = 115,
        F5 = 116,
        F6 = 117,
        F7 = 118,
        F8 = 119,
        F9 = 120,
        F10 = 121,
        F11 = 122,
        F12 = 123,
        NumLock = 144,
        ScrollLock = 145,
        Semicolon = 186,
        EqualSign = 187,
        Comma = 188,
        Dash = 189,
        Period = 190,
        ForwardSlash = 191,
        GraveAccent = 192,
        OpenBracket = 219,
        BackSlash = 220,
        CloseBracket = 221,
        SingleQuote = 222
    }
    enum ModCode
    {
        // Locks
        NumLock = 0x20,
        ScrollLock = 0x40,
        CapsLock = 0x80,

        // Right Side Keyboard
        Right = 0x100,

        RALT = 0x1,
        RCTRL = 0x4,

        // Left Side Keyboard
        ALT = 0x2,
        CTRL = 0x8,
        Shift = 0x10,


    }
}
