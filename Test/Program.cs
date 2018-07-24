using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessMemory;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Test
{
    class Program
    {

        static void Main(string[] args)
        {
            /*
            ProcessStream m = new ProcessStream(Process.GetProcessesByName("mouserate").First());
            long offset = 0;
            MEMORY_BASIC_INFORMATION i = new MEMORY_BASIC_INFORMATION();
            long maxAddress = 0x7fffffff;
            while ((long)m.Process.MainModule.BaseAddress + offset <= maxAddress) {
                int dw = VirtualQueryEx(m.Handle, (IntPtr)((long)m.Process.MainModule.BaseAddress + offset), out i, (uint)Marshal.SizeOf(i));
                offset += (long)i.RegionSize;
                AllocationProtect p = (AllocationProtect)i.AllocationProtect;
                Console.WriteLine(i.RegionSize + " : " + p);
            }
            */

            ProcessStream s = new ProcessStream(Process.GetProcessesByName("MEmuHeadless").First());
            HexPattern p = new HexPattern("14 ?? ?? ED");
            long pointerMatrix = s.PatternScan(p, 0x40000000, 4096 * 3, 0x400000000);
            string ms = "11 11 11 11 11";

            Console.WriteLine(System.Convert.ToString(pointerMatrix,16));
            s.WriteMemory(pointerMatrix, strToToHexByte(ms), 139);
            Console.WriteLine("Done");
            Console.ReadLine();
        }
        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

    }
}
