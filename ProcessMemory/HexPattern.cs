using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMemory
{
    public class HexPattern : IMemoryPattern
    {
        public string[] Pattern { get; private set; }
        public byte?[] PatternBytes { get; private set; }

        public long Length => PatternBytes.Length;

        public HexPattern(string pattern) {
            //Split pattern into an array for each space.
            Pattern = pattern.Split(' ');

            byte?[] bytes = new byte?[Pattern.Length];

            //Loop through each pattern string
            for (int i = 0; i < Pattern.Length; i++) {
                //If pattern at index is == ?? then we set that byte to null
                if (Pattern[i].Equals("??"))
                    bytes[i] = null;
                else
                    bytes[i] = Convert.ToByte(Pattern[i], 16);
                //If pattern is not == ?? then convert it to a byte
            }

            PatternBytes = bytes;
        }
        /// <summary>
        /// 不带空格的
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="none"></param>
        public HexPattern(string pattern,bool none=true)
        {
            byte[] hexByte = strToToHexByte(pattern);
            byte?[] bytes = new byte?[hexByte.Length];
            PatternBytes = bytes;
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
        public long FindMatch(byte[] source, long length) {
            bool allTheSame;
            for (int i = 0; i + PatternBytes.Length <= length; i++) {
                allTheSame = true;
                for (int jj = 0; jj < PatternBytes.Length; jj++) {
                    if (PatternBytes[jj] == null)
                        continue;

                    if (source[i + jj] != PatternBytes[jj]) {
                        allTheSame = false;
                        break;
                    }
                }
                if (allTheSame)
                    return i;
            }

            return -1;
        }
    }
}
