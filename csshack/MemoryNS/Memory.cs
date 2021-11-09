using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace csshack.MemoryNS
{
    internal class Memory
    {
        private static Process Process;
        public static IntPtr ProcessHandle { get; private set; }
        static Memory() // ctor
        {
            Process = Process.GetProcessesByName("hl2").FirstOrDefault();
            if (Process is null) { Environment.Exit(404); }

            ProcessHandle = Kernel32API.OpenProcess(Offsets.PROCESS_WM_READ, false, Process.Id);
        }
        public static T Read<T>(uint address)
        {
            uint length = (uint)Marshal.SizeOf(typeof(T));

            if (typeof(T) == typeof(bool))
                length = 1;

            byte[] buffer = new byte[length];
            uint nBytesRead = uint.MinValue;
            Kernel32API.ReadProcessMemory(ProcessHandle, (IntPtr)address, buffer, length, ref nBytesRead);
            return GetStructure<T>(buffer);
        }
        public static T Read<T>(IntPtr address)
        {
            return Read<T>((uint)address);
        }

        private static T GetStructure<T>(byte[] buffer)
        {
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            T structure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return structure;
        }
    }
}
