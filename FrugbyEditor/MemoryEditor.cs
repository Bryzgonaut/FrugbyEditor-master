using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace FrugbyEditor
{
    public static class MemoryEditor
    {
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        private const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        private static Process frugbyProcess = null;
        private static IntPtr frugbyProcessHandle;
        private static int baseAddress;

        /// <summary>
        /// Attaches to hockey.exe. Must be called before anything else. Make sure hockey is running
        /// </summary>
        /// <param name="isServer">Do you wish to attach to hockeydedicated.exe?</param>
        public static void Init(bool isServer)
        {
            try
            {
                frugbyProcess = Process.GetProcessesByName("frugby")[0];
            }
            catch (System.IndexOutOfRangeException e)  // CS0168
            {
                System.Console.WriteLine(e.Message);

                throw new System.ArgumentOutOfRangeException("no frugby.exe found", e);
            }
            frugbyProcessHandle = OpenProcess(PROCESS_ALL_ACCESS, false, frugbyProcess.Id);
        }

        // <summary>
        /// Attaches to frugby.exe. Must be called before anything else. Make sure frugby is running
        /// </summary>
        public static void Init()
        {
            try
            {
                frugbyProcess = Process.GetProcessesByName("frugby")[0];
                baseAddress = frugbyProcess.MainModule.BaseAddress.ToInt32();
            }
            catch (System.IndexOutOfRangeException e)  // CS0168
            {
                System.Console.WriteLine(e.Message);

                throw new System.ArgumentOutOfRangeException("no frugby.exe found", e);
            }
            frugbyProcessHandle = OpenProcess(PROCESS_ALL_ACCESS, false, frugbyProcess.Id);
        }

        /// <summary>
        /// Read a 32 bit integer from memory.
        /// </summary>
        /// <param name="address">the address to read from</param>
        public static int ReadInt(int address)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[4];
            ReadProcessMemory((int)frugbyProcessHandle, address + baseAddress, buffer, buffer.Length, ref bytesRead);

            return BitConverter.ToInt32(buffer, 0);
        }

        /// <summary>
        /// Write a 32 bit integer to memory.
        /// </summary>
        /// <param name="value">the integer to write</param>
        /// <param name="address">the address to write to</param>
        public static void WriteInt(int value, int address)
        {
            int bytesWritten = 0;
            byte[] buffer = BitConverter.GetBytes(value);

            WriteProcessMemory((int)frugbyProcessHandle, address + baseAddress, buffer, buffer.Length, ref bytesWritten);
        }

        /// <summary>
        /// Read a float from memory.
        /// </summary>
        /// <param name="address">the address to read from</param>
        public static float ReadFloat(int address)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[4];
            ReadProcessMemory((int)frugbyProcessHandle, address + baseAddress, buffer, buffer.Length, ref bytesRead);

            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        /// Write a float to memory.
        /// </summary>
        /// <param name="value">the float to write</param>
        /// <param name="address">the address to write to</param>
        public static void WriteFloat(float value, int address)
        {
            int bytesWritten = 0;
            byte[] buffer = BitConverter.GetBytes(value);

            WriteProcessMemory((int)frugbyProcessHandle, address + baseAddress, buffer, buffer.Length, ref bytesWritten);
        }

        /// <summary>
        /// Read a string from memory
        /// </summary>
        /// <param name="address">the address to read from</param>
        /// <param name="length">the length of the string to read</param>
        public static string ReadString(int address, int length)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[length];
            ReadProcessMemory((int)frugbyProcessHandle, address + baseAddress, buffer, buffer.Length, ref bytesRead);

            // Read up until a \0 is encounted
              return Encoding.ASCII.GetString(buffer).Split('\0')[0];
        }

        public static void WriteString(int address, string str)
        {
            int bytesWritten = 0;
            byte[] buffer = Encoding.ASCII.GetBytes(str + "\0");

            WriteProcessMemory((int)frugbyProcessHandle, address + baseAddress, buffer, buffer.Length, ref bytesWritten);
        }

        /// <summary>
        /// Writes a Vector3 to memory.
        /// </summary>
        /// <param name="v">float[] representing a Vector3.  x (width) = v[0]. y (height) = v[1], z (length) = v[2]</param>
        /// <param name="address"> The address of the vector to write. Addresses are contained in HQMClientAddresses or HQMServerAddresses</param>
        public static void WriteFRUGVector(FRUGVector v, int address)
        {         
            int bytesWritten = 0;
            byte[] buffer = new byte[12];
            float[] posArray = new float[] { v.X, v.Y, v.Z };
            Buffer.BlockCopy(posArray, 0, buffer, 0, buffer.Length);

            WriteProcessMemory((int)frugbyProcessHandle, address + baseAddress, buffer, buffer.Length, ref bytesWritten);
        }

        /// <summary>
        /// Reads a Vector3 from memory
        /// </summary>
        /// <param name="address">The address of the Vector3 to write. . Addresses are contained in HQMClientAddresses or HQMServerAddresses</param>
        /// <returns>float[] representing a Vector3. x (width) = v[0]. y (height) = v[1], z (length) = v[2]</returns>
        public static FRUGVector ReadFRUGVector(int address)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[12];

            ReadProcessMemory((int)frugbyProcessHandle, address + baseAddress, buffer, buffer.Length, ref bytesRead);

            float[] posArray = new float[3];
            Buffer.BlockCopy(buffer, 0, posArray, 0, buffer.Length);
            FRUGVector v = new FRUGVector(posArray[0], posArray[1], posArray[2]);

            return v;
        }

        /// <summary>
        /// Writes bytes to memory
        /// </summary>
        /// <param name="bytes">bytes to write</param>
        /// <param name="address">The address to write the bytes to</param>
        /// <returns>number of bytes written </returns>
        public static int WriteBytes(byte[] bytes, int address)
        {
            int bytesWritten = 0;
            WriteProcessMemory((int)frugbyProcessHandle, address + baseAddress, bytes, bytes.Length, ref bytesWritten);
            return bytesWritten;
        }

        /// <summary>
        /// Read bytes from memory
        /// </summary>
        /// <param name="address">The address to read the bytes from</param>
        /// <param name="numBytes">The number of bytes to read</param>
        /// <returns>bytes read from memory</returns>
        public static byte[] ReadBytes(int address, int numBytes)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[numBytes];
            ReadProcessMemory((int)frugbyProcessHandle, address + baseAddress, buffer, buffer.Length, ref bytesRead);
            return buffer;
        }
    }
}
