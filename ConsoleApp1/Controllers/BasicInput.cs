using System.Runtime.InteropServices;

public class InputListener : IDisposable
{
    private Thread _inputThread;
    private bool _running;
    private IntPtr _inputHandle;

    private const int STD_INPUT_HANDLE = -10;
    private const uint ENABLE_MOUSE_INPUT = 0x0010;
    private const uint ENABLE_EXTENDED_FLAGS = 0x0080;
    private const uint ENABLE_PROCESSED_INPUT = 0x0001;
    private const uint ENABLE_QUICK_EDIT_MODE = 0x0040;

    private const ushort KEY_EVENT = 0x0001;
    private const ushort MOUSE_EVENT = 0x0002;

    [StructLayout(LayoutKind.Sequential)]
    private struct INPUT_RECORD
    {
        public ushort EventType;
        public EventUnion Event;

        [StructLayout(LayoutKind.Explicit)]
        public struct EventUnion
        {
            [FieldOffset(0)]
            public KEY_EVENT_RECORD KeyEvent;
            [FieldOffset(0)]
            public MOUSE_EVENT_RECORD MouseEvent;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct KEY_EVENT_RECORD
    {
        [MarshalAs(UnmanagedType.Bool)]
        public bool bKeyDown;
        public ushort wRepeatCount;
        public ushort wVirtualKeyCode;
        public ushort wVirtualScanCode;
        public char UnicodeChar;
        public uint dwControlKeyState;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MOUSE_EVENT_RECORD
    {
        public COORD dwMousePosition;
        public uint dwButtonState;
        public uint dwControlKeyState;
        public uint dwEventFlags;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct COORD
    {
        public short X;
        public short Y;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll")]
    private static extern bool GetConsoleMode(IntPtr hConsoleInput, out uint lpMode);

    [DllImport("kernel32.dll")]
    private static extern bool SetConsoleMode(IntPtr hConsoleInput, uint dwMode);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool ReadConsoleInput(
        IntPtr hConsoleInput,
        [Out] INPUT_RECORD[] lpBuffer,
        uint nLength,
        out uint lpNumberOfEventsRead);

    public delegate void KeyEventHandler(KeyEventArgs e);
    public delegate void MouseEventHandler(MouseEventArgs e);

    public event KeyEventHandler? OnKeyEvent;
    public event MouseEventHandler? OnMouseEvent;

    public InputListener()
    {
        _inputHandle = GetStdHandle(STD_INPUT_HANDLE);
        if (_inputHandle == IntPtr.Zero)
            throw new InvalidOperationException("Не удалось получить дескриптор ввода консоли.");

        if (!GetConsoleMode(_inputHandle, out uint mode))
            throw new InvalidOperationException("Не удалось получить режим консоли.");

        // Отключаем Quick Edit Mode, включаем обработку мыши и расширенные флаги
        mode &= ~ENABLE_QUICK_EDIT_MODE;
        mode |= ENABLE_MOUSE_INPUT | ENABLE_EXTENDED_FLAGS | ENABLE_PROCESSED_INPUT;

        if (!SetConsoleMode(_inputHandle, mode))
            throw new InvalidOperationException("Не удалось установить режим консоли.");
    }

    public void Start()
    {
        if (_running)
            return;

        _running = true;
        _inputThread = new Thread(InputLoop)
        {
            IsBackground = true
        };
        _inputThread.Start();
    }

    public void Stop()
    {
        _running = false;
        _inputThread?.Join();
    }

    private void InputLoop()
    {
        while (_running)
        {
            INPUT_RECORD[] records = new INPUT_RECORD[1];
            if (!ReadConsoleInput(_inputHandle, records, 1, out uint read) || read == 0)
            {
                Thread.Sleep(10);
                continue;
            }

            var record = records[0];

            if (record.EventType == KEY_EVENT)
            {
                var ke = record.Event.KeyEvent;
                KeyEventArgs args = new()
                {
                    KeyDown =            ke.bKeyDown,
                    RepeatCount =        ke.wRepeatCount,
                    VirtualKeyCode =     ke.wVirtualKeyCode,
                    ScanCode =           ke.wVirtualScanCode,
                    UnicodeChar =        ke.UnicodeChar,
                    ControlKeyState =    ke.dwControlKeyState
                };
                OnKeyEvent?.Invoke(args);
            }
            else if (record.EventType == MOUSE_EVENT)
            {
                var me = record.Event.MouseEvent;
                MouseEventArgs args = new()
                {
                    X = me.dwMousePosition.X,
                    Y = me.dwMousePosition.Y,
                    LeftButtonPressed = (me.dwButtonState & 0x1) != 0,
                    RightButtonPressed = (me.dwButtonState & 0x2) != 0,
                    MiddleButtonPressed = (me.dwButtonState & 0x4) != 0,
                    MouseMoved = (me.dwEventFlags & 0x1) != 0,
                    MouseWheelUp = (me.dwEventFlags == 0x4 && (me.dwButtonState & 0x700000) != 0),
                    MouseWheelDown = (me.dwEventFlags == 0x4 && (me.dwButtonState & 0xFF800000) != 0)
                };
                OnMouseEvent?.Invoke(args);
            }
        }
    }
    public void Dispose() => Stop();
}

public class KeyEventArgs : EventArgs
{
    public bool KeyDown { get; set; }
    public ushort RepeatCount { get; set; }
    public ushort VirtualKeyCode { get; set; }
    public ushort ScanCode { get; set; }
    public char UnicodeChar { get; set; }
    public uint ControlKeyState { get; set; }
}

public class MouseEventArgs : EventArgs
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool LeftButtonPressed { get; set; }
    public bool RightButtonPressed { get; set; }
    public bool MiddleButtonPressed { get; set; }
    public bool MouseMoved { get; set; }
    public bool MouseWheelUp { get; set; }
    public bool MouseWheelDown { get; set; }
}
