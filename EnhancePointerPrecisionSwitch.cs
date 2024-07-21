using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EnhancePointerPrecisionSwitch
{
    internal static class Program
    {
        enum Action { None, On, Off, Toggle }

        enum SPIF { NONE = 0x00, SPIF_UPDATEINIFILE = 0x01, SPIF_SENDCHANGE = 0x02 }

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", SetLastError = true)]
        static extern bool SystemParametersInfoGet(uint action, uint param, IntPtr vparam, SPIF fWinIni);
        const UInt32 SPI_GETMOUSE = 0x0003;

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", SetLastError = true)]
        static extern bool SystemParametersInfoSet(uint action, uint param, IntPtr vparam, SPIF fWinIni);
        const UInt32 SPI_SETMOUSE = 0x0004;

        static bool SetEnhancePointerPrecision(Action act, bool permanent)
        {
            int[] mouseParams = new int[3];
            SystemParametersInfoGet(SPI_GETMOUSE, 0, GCHandle.Alloc(mouseParams, GCHandleType.Pinned).AddrOfPinnedObject(), SPIF.NONE);
            if (act == Action.On) mouseParams[2] = 1;
            if (act == Action.Off) mouseParams[2] = 0;
            if (act == Action.Toggle) mouseParams[2] = mouseParams[2] == 0 ? 1 : 0;
            return SystemParametersInfoSet(SPI_SETMOUSE, 0, GCHandle.Alloc(mouseParams, GCHandleType.Pinned).AddrOfPinnedObject(), permanent ? SPIF.SPIF_UPDATEINIFILE : SPIF.SPIF_SENDCHANGE);
        }

        [STAThread]
        static void Main(string[] args)
        {
            Action act = Action.None;
            bool p = false;
            foreach (var arg in args)
            {
                string a = args[0].Trim().ToLower();
                if (a == "on") act = Action.On;
                if (a == "off") act = Action.Off;
                if (a == "toggle") act = Action.Toggle;
                if (a == "permanent") p = true; 
            }
            if (act != Action.None) SetEnhancePointerPrecision(act, p);
        }
    }
}
