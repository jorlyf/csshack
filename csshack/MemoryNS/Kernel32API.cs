using System;
using System.Runtime.InteropServices;

namespace csshack.MemoryNS
{
    internal static class Kernel32API
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr processHandle,
          IntPtr lpBaseAddress, byte[] lpBuffer, uint dwSize, ref uint lpNumberOfBytesRead);
    }
}
