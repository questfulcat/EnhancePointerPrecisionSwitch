using System;
using System.Runtime.InteropServices;

namespace EnhancePointerPrecisionSwitch
{
    internal static class Program
    {
        enum SPIF { NONE = 0x00, SPIF_UPDATEINIFILE = 0x01, SPIF_SENDCHANGE = 0x02 }
        const UInt32 SPI_GETMOUSE = 0x0003, SPI_SETMOUSE = 0x0004;

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", SetLastError = true)]
        static extern bool SystemParametersInfo(uint action, uint param, IntPtr vparam, SPIF fWinIni);

        [STAThread]
        static void Main(string[] args)
        {
            int[] mouseParams = new int[3];
            GCHandle gch = GCHandle.Alloc(mouseParams, GCHandleType.Pinned);
            SystemParametersInfo(SPI_GETMOUSE, 0, gch.AddrOfPinnedObject(), SPIF.NONE);
            if (Array.IndexOf(args, "on") >= 0) mouseParams[2] = 1;
            if (Array.IndexOf(args, "off") >= 0) mouseParams[2] = 0;
            if (Array.IndexOf(args, "toggle") >= 0) mouseParams[2] = mouseParams[2] == 0 ? 1 : 0;
            SystemParametersInfo(SPI_SETMOUSE, 0, gch.AddrOfPinnedObject(), (Array.IndexOf(args, "permanent") >= 0) ? SPIF.SPIF_UPDATEINIFILE : SPIF.SPIF_SENDCHANGE);
            gch.Free();
        }
    }
}
