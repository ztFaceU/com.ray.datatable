using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

//
// 摘要:
//     实用函数集。
public static class Utility
{
    //
    // 摘要:
    //     程序集相关的实用函数。
    public static class Assembly
    {
        private static readonly System.Reflection.Assembly[] s_Assemblies;

        private static readonly Dictionary<string, Type> s_CachedTypes;

        static Assembly()
        {
            s_Assemblies = null;
            s_CachedTypes = new Dictionary<string, Type>(StringComparer.Ordinal);
            s_Assemblies = AppDomain.CurrentDomain.GetAssemblies();
        }

        //
        // 摘要:
        //     获取已加载的程序集。
        //
        // 返回结果:
        //     已加载的程序集。
        public static System.Reflection.Assembly[] GetAssemblies()
        {
            return s_Assemblies;
        }

        //
        // 摘要:
        //     获取已加载的程序集中的所有类型。
        //
        // 返回结果:
        //     已加载的程序集中的所有类型。
        public static Type[] GetTypes()
        {
            List<Type> list = new List<Type>();
            System.Reflection.Assembly[] array = s_Assemblies;
            foreach (System.Reflection.Assembly assembly in array)
            {
                list.AddRange(assembly.GetTypes());
            }

            return list.ToArray();
        }

        //
        // 摘要:
        //     获取已加载的程序集中的所有类型。
        //
        // 参数:
        //   results:
        //     已加载的程序集中的所有类型。
        public static void GetTypes(List<Type> results)
        {
            if (results == null)
            {
                throw new Exception("Results is invalid.");
            }

            results.Clear();
            System.Reflection.Assembly[] array = s_Assemblies;
            foreach (System.Reflection.Assembly assembly in array)
            {
                results.AddRange(assembly.GetTypes());
            }
        }

        //
        // 摘要:
        //     获取已加载的程序集中的指定类型。
        //
        // 参数:
        //   typeName:
        //     要获取的类型名。
        //
        // 返回结果:
        //     已加载的程序集中的指定类型。
        public static Type GetType(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                throw new Exception("Type name is invalid.");
            }

            Type value = null;
            if (s_CachedTypes.TryGetValue(typeName, out value))
            {
                return value;
            }

            value = Type.GetType(typeName);
            if ((object)value != null)
            {
                s_CachedTypes.Add(typeName, value);
                return value;
            }

            System.Reflection.Assembly[] array = s_Assemblies;
            foreach (System.Reflection.Assembly assembly in array)
            {
                value = Type.GetType(Text.Format("{0}, {1}", typeName, assembly.FullName));
                if ((object)value != null)
                {
                    s_CachedTypes.Add(typeName, value);
                    return value;
                }
            }

            return null;
        }
    }

    //
    // 摘要:
    //     压缩解压缩相关的实用函数。
    public static class Compression
    {
        //
        // 摘要:
        //     压缩解压缩辅助器接口。
        public interface ICompressionHelper
        {
            //
            // 摘要:
            //     压缩数据。
            //
            // 参数:
            //   bytes:
            //     要压缩的数据的二进制流。
            //
            //   offset:
            //     要压缩的数据的二进制流的偏移。
            //
            //   length:
            //     要压缩的数据的二进制流的长度。
            //
            //   compressedStream:
            //     压缩后的数据的二进制流。
            //
            // 返回结果:
            //     是否压缩数据成功。
            bool Compress(byte[] bytes, int offset, int length, Stream compressedStream);

            //
            // 摘要:
            //     压缩数据。
            //
            // 参数:
            //   stream:
            //     要压缩的数据的二进制流。
            //
            //   compressedStream:
            //     压缩后的数据的二进制流。
            //
            // 返回结果:
            //     是否压缩数据成功。
            bool Compress(Stream stream, Stream compressedStream);

            //
            // 摘要:
            //     解压缩数据。
            //
            // 参数:
            //   bytes:
            //     要解压缩的数据的二进制流。
            //
            //   offset:
            //     要解压缩的数据的二进制流的偏移。
            //
            //   length:
            //     要解压缩的数据的二进制流的长度。
            //
            //   decompressedStream:
            //     解压缩后的数据的二进制流。
            //
            // 返回结果:
            //     是否解压缩数据成功。
            bool Decompress(byte[] bytes, int offset, int length, Stream decompressedStream);

            //
            // 摘要:
            //     解压缩数据。
            //
            // 参数:
            //   stream:
            //     要解压缩的数据的二进制流。
            //
            //   decompressedStream:
            //     解压缩后的数据的二进制流。
            //
            // 返回结果:
            //     是否解压缩数据成功。
            bool Decompress(Stream stream, Stream decompressedStream);
        }

        private static ICompressionHelper s_CompressionHelper;

        //
        // 摘要:
        //     设置压缩解压缩辅助器。
        //
        // 参数:
        //   compressionHelper:
        //     要设置的压缩解压缩辅助器。
        public static void SetCompressionHelper(ICompressionHelper compressionHelper)
        {
            s_CompressionHelper = compressionHelper;
        }

        //
        // 摘要:
        //     压缩数据。
        //
        // 参数:
        //   bytes:
        //     要压缩的数据的二进制流。
        //
        // 返回结果:
        //     压缩后的数据的二进制流。
        public static byte[] Compress(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new Exception("Bytes is invalid.");
            }

            return Compress(bytes, 0, bytes.Length);
        }

        //
        // 摘要:
        //     压缩数据。
        //
        // 参数:
        //   bytes:
        //     要压缩的数据的二进制流。
        //
        //   compressedStream:
        //     压缩后的数据的二进制流。
        //
        // 返回结果:
        //     是否压缩数据成功。
        public static bool Compress(byte[] bytes, Stream compressedStream)
        {
            if (bytes == null)
            {
                throw new Exception("Bytes is invalid.");
            }

            return Compress(bytes, 0, bytes.Length, compressedStream);
        }

        //
        // 摘要:
        //     压缩数据。
        //
        // 参数:
        //   bytes:
        //     要压缩的数据的二进制流。
        //
        //   offset:
        //     要压缩的数据的二进制流的偏移。
        //
        //   length:
        //     要压缩的数据的二进制流的长度。
        //
        // 返回结果:
        //     压缩后的数据的二进制流。
        public static byte[] Compress(byte[] bytes, int offset, int length)
        {
            using MemoryStream memoryStream = new MemoryStream();
            if (Compress(bytes, offset, length, memoryStream))
            {
                return memoryStream.ToArray();
            }

            return null;
        }

        //
        // 摘要:
        //     压缩数据。
        //
        // 参数:
        //   bytes:
        //     要压缩的数据的二进制流。
        //
        //   offset:
        //     要压缩的数据的二进制流的偏移。
        //
        //   length:
        //     要压缩的数据的二进制流的长度。
        //
        //   compressedStream:
        //     压缩后的数据的二进制流。
        //
        // 返回结果:
        //     是否压缩数据成功。
        public static bool Compress(byte[] bytes, int offset, int length, Stream compressedStream)
        {
            if (s_CompressionHelper == null)
            {
                throw new Exception("Compressed helper is invalid.");
            }

            if (bytes == null)
            {
                throw new Exception("Bytes is invalid.");
            }

            if (offset < 0 || length < 0 || offset + length > bytes.Length)
            {
                throw new Exception("Offset or length is invalid.");
            }

            if (compressedStream == null)
            {
                throw new Exception("Compressed stream is invalid.");
            }

            try
            {
                return s_CompressionHelper.Compress(bytes, offset, length, compressedStream);
            }
            catch (Exception ex)
            {
                if (ex is Exception)
                {
                    throw;
                }

                throw new Exception(Text.Format("Can not compress with exception '{0}'.", ex.ToString()), ex);
            }
        }

        //
        // 摘要:
        //     压缩数据。
        //
        // 参数:
        //   stream:
        //     要压缩的数据的二进制流。
        //
        // 返回结果:
        //     压缩后的数据的二进制流。
        public static byte[] Compress(Stream stream)
        {
            using MemoryStream memoryStream = new MemoryStream();
            if (Compress(stream, memoryStream))
            {
                return memoryStream.ToArray();
            }

            return null;
        }

        //
        // 摘要:
        //     压缩数据。
        //
        // 参数:
        //   stream:
        //     要压缩的数据的二进制流。
        //
        //   compressedStream:
        //     压缩后的数据的二进制流。
        //
        // 返回结果:
        //     是否压缩数据成功。
        public static bool Compress(Stream stream, Stream compressedStream)
        {
            if (s_CompressionHelper == null)
            {
                throw new Exception("Compressed helper is invalid.");
            }

            if (stream == null)
            {
                throw new Exception("Stream is invalid.");
            }

            if (compressedStream == null)
            {
                throw new Exception("Compressed stream is invalid.");
            }

            try
            {
                return s_CompressionHelper.Compress(stream, compressedStream);
            }
            catch (Exception ex)
            {
                if (ex is Exception)
                {
                    throw;
                }

                throw new Exception(Text.Format("Can not compress with exception '{0}'.", ex.ToString()), ex);
            }
        }

        //
        // 摘要:
        //     解压缩数据。
        //
        // 参数:
        //   bytes:
        //     要解压缩的数据的二进制流。
        //
        // 返回结果:
        //     解压缩后的数据的二进制流。
        public static byte[] Decompress(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new Exception("Bytes is invalid.");
            }

            return Decompress(bytes, 0, bytes.Length);
        }

        //
        // 摘要:
        //     解压缩数据。
        //
        // 参数:
        //   bytes:
        //     要解压缩的数据的二进制流。
        //
        //   decompressedStream:
        //     解压缩后的数据的二进制流。
        //
        // 返回结果:
        //     是否解压缩数据成功。
        public static bool Decompress(byte[] bytes, Stream decompressedStream)
        {
            if (bytes == null)
            {
                throw new Exception("Bytes is invalid.");
            }

            return Decompress(bytes, 0, bytes.Length, decompressedStream);
        }

        //
        // 摘要:
        //     解压缩数据。
        //
        // 参数:
        //   bytes:
        //     要解压缩的数据的二进制流。
        //
        //   offset:
        //     要解压缩的数据的二进制流的偏移。
        //
        //   length:
        //     要解压缩的数据的二进制流的长度。
        //
        // 返回结果:
        //     解压缩后的数据的二进制流。
        public static byte[] Decompress(byte[] bytes, int offset, int length)
        {
            using MemoryStream memoryStream = new MemoryStream();
            if (Decompress(bytes, offset, length, memoryStream))
            {
                return memoryStream.ToArray();
            }

            return null;
        }

        //
        // 摘要:
        //     解压缩数据。
        //
        // 参数:
        //   bytes:
        //     要解压缩的数据的二进制流。
        //
        //   offset:
        //     要解压缩的数据的二进制流的偏移。
        //
        //   length:
        //     要解压缩的数据的二进制流的长度。
        //
        //   decompressedStream:
        //     解压缩后的数据的二进制流。
        //
        // 返回结果:
        //     是否解压缩数据成功。
        public static bool Decompress(byte[] bytes, int offset, int length, Stream decompressedStream)
        {
            if (s_CompressionHelper == null)
            {
                throw new Exception("Compressed helper is invalid.");
            }

            if (bytes == null)
            {
                throw new Exception("Bytes is invalid.");
            }

            if (offset < 0 || length < 0 || offset + length > bytes.Length)
            {
                throw new Exception("Offset or length is invalid.");
            }

            if (decompressedStream == null)
            {
                throw new Exception("Decompressed stream is invalid.");
            }

            try
            {
                return s_CompressionHelper.Decompress(bytes, offset, length, decompressedStream);
            }
            catch (Exception ex)
            {
                if (ex is Exception)
                {
                    throw;
                }

                throw new Exception(Text.Format("Can not decompress with exception '{0}'.", ex.ToString()), ex);
            }
        }

        //
        // 摘要:
        //     解压缩数据。
        //
        // 参数:
        //   stream:
        //     要解压缩的数据的二进制流。
        //
        // 返回结果:
        //     是否解压缩数据成功。
        public static byte[] Decompress(Stream stream)
        {
            using MemoryStream memoryStream = new MemoryStream();
            if (Decompress(stream, memoryStream))
            {
                return memoryStream.ToArray();
            }

            return null;
        }

        //
        // 摘要:
        //     解压缩数据。
        //
        // 参数:
        //   stream:
        //     要解压缩的数据的二进制流。
        //
        //   decompressedStream:
        //     解压缩后的数据的二进制流。
        //
        // 返回结果:
        //     是否解压缩数据成功。
        public static bool Decompress(Stream stream, Stream decompressedStream)
        {
            if (s_CompressionHelper == null)
            {
                throw new Exception("Compressed helper is invalid.");
            }

            if (stream == null)
            {
                throw new Exception("Stream is invalid.");
            }

            if (decompressedStream == null)
            {
                throw new Exception("Decompressed stream is invalid.");
            }

            try
            {
                return s_CompressionHelper.Decompress(stream, decompressedStream);
            }
            catch (Exception ex)
            {
                if (ex is Exception)
                {
                    throw;
                }

                throw new Exception(Text.Format("Can not decompress with exception '{0}'.", ex.ToString()), ex);
            }
        }
    }

    //
    // 摘要:
    //     类型转换相关的实用函数。
    public static class Converter
    {
        private const float InchesToCentimeters = 2.54f;

        private const float CentimetersToInches = 0.393700778f;

        //
        // 摘要:
        //     获取数据在此计算机结构中存储时的字节顺序。
        public static bool IsLittleEndian => BitConverter.IsLittleEndian;

        //
        // 摘要:
        //     获取或设置屏幕每英寸点数。
        public static float ScreenDpi { get; set; }

        //
        // 摘要:
        //     将像素转换为厘米。
        //
        // 参数:
        //   pixels:
        //     像素。
        //
        // 返回结果:
        //     厘米。
        public static float GetCentimetersFromPixels(float pixels)
        {
            if (ScreenDpi <= 0f)
            {
                throw new Exception("You must set screen DPI first.");
            }

            return 2.54f * pixels / ScreenDpi;
        }

        //
        // 摘要:
        //     将厘米转换为像素。
        //
        // 参数:
        //   centimeters:
        //     厘米。
        //
        // 返回结果:
        //     像素。
        public static float GetPixelsFromCentimeters(float centimeters)
        {
            if (ScreenDpi <= 0f)
            {
                throw new Exception("You must set screen DPI first.");
            }

            return 0.393700778f * centimeters * ScreenDpi;
        }

        //
        // 摘要:
        //     将像素转换为英寸。
        //
        // 参数:
        //   pixels:
        //     像素。
        //
        // 返回结果:
        //     英寸。
        public static float GetInchesFromPixels(float pixels)
        {
            if (ScreenDpi <= 0f)
            {
                throw new Exception("You must set screen DPI first.");
            }

            return pixels / ScreenDpi;
        }

        //
        // 摘要:
        //     将英寸转换为像素。
        //
        // 参数:
        //   inches:
        //     英寸。
        //
        // 返回结果:
        //     像素。
        public static float GetPixelsFromInches(float inches)
        {
            if (ScreenDpi <= 0f)
            {
                throw new Exception("You must set screen DPI first.");
            }

            return inches * ScreenDpi;
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的布尔值。
        //
        // 参数:
        //   value:
        //     要转换的布尔值。
        //
        // 返回结果:
        //     用于存放结果的字节数组。
        public static byte[] GetBytes(bool value)
        {
            byte[] array = new byte[1];
            GetBytes(value, array, 0);
            return array;
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的布尔值。
        //
        // 参数:
        //   value:
        //     要转换的布尔值。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        public static void GetBytes(bool value, byte[] buffer)
        {
            GetBytes(value, buffer, 0);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的布尔值。
        //
        // 参数:
        //   value:
        //     要转换的布尔值。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        //
        //   startIndex:
        //     buffer 内的起始位置。
        public static void GetBytes(bool value, byte[] buffer, int startIndex)
        {
            if (buffer == null)
            {
                throw new Exception("Buffer is invalid.");
            }

            if (startIndex < 0 || startIndex + 1 > buffer.Length)
            {
                throw new Exception("Start index is invalid.");
            }

            buffer[startIndex] = (byte)(value ? 1 : 0);
        }

        //
        // 摘要:
        //     返回由字节数组中首字节转换来的布尔值。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        // 返回结果:
        //     如果 value 中的首字节非零，则为 true，否则为 false。
        public static bool GetBoolean(byte[] value)
        {
            return BitConverter.ToBoolean(value, 0);
        }

        //
        // 摘要:
        //     返回由字节数组中指定位置的一个字节转换来的布尔值。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        //   startIndex:
        //     value 内的起始位置。
        //
        // 返回结果:
        //     如果 value 中指定位置的字节非零，则为 true，否则为 false。
        public static bool GetBoolean(byte[] value, int startIndex)
        {
            return BitConverter.ToBoolean(value, startIndex);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 Unicode 字符值。
        //
        // 参数:
        //   value:
        //     要转换的字符。
        //
        // 返回结果:
        //     用于存放结果的字节数组。
        public static byte[] GetBytes(char value)
        {
            byte[] array = new byte[2];
            GetBytes((short)value, array, 0);
            return array;
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 Unicode 字符值。
        //
        // 参数:
        //   value:
        //     要转换的字符。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        public static void GetBytes(char value, byte[] buffer)
        {
            GetBytes((short)value, buffer, 0);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 Unicode 字符值。
        //
        // 参数:
        //   value:
        //     要转换的字符。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        //
        //   startIndex:
        //     buffer 内的起始位置。
        public static void GetBytes(char value, byte[] buffer, int startIndex)
        {
            GetBytes((short)value, buffer, startIndex);
        }

        //
        // 摘要:
        //     返回由字节数组中前两个字节转换来的 Unicode 字符。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        // 返回结果:
        //     由两个字节构成的字符。
        public static char GetChar(byte[] value)
        {
            return BitConverter.ToChar(value, 0);
        }

        //
        // 摘要:
        //     返回由字节数组中指定位置的两个字节转换来的 Unicode 字符。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        //   startIndex:
        //     value 内的起始位置。
        //
        // 返回结果:
        //     由两个字节构成的字符。
        public static char GetChar(byte[] value, int startIndex)
        {
            return BitConverter.ToChar(value, startIndex);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 16 位有符号整数值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        // 返回结果:
        //     用于存放结果的字节数组。
        public static byte[] GetBytes(short value)
        {
            byte[] array = new byte[2];
            GetBytes(value, array, 0);
            return array;
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 16 位有符号整数值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        public static void GetBytes(short value, byte[] buffer)
        {
            GetBytes(value, buffer, 0);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 16 位有符号整数值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        //
        //   startIndex:
        //     buffer 内的起始位置。
        public unsafe static void GetBytes(short value, byte[] buffer, int startIndex)
        {
            if (buffer == null)
            {
                throw new Exception("Buffer is invalid.");
            }

            if (startIndex < 0 || startIndex + 2 > buffer.Length)
            {
                throw new Exception("Start index is invalid.");
            }

            fixed (byte* ptr = buffer)
            {
                *(short*)(ptr + startIndex) = value;
            }
        }

        //
        // 摘要:
        //     返回由字节数组中前两个字节转换来的 16 位有符号整数。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        // 返回结果:
        //     由两个字节构成的 16 位有符号整数。
        public static short GetInt16(byte[] value)
        {
            return BitConverter.ToInt16(value, 0);
        }

        //
        // 摘要:
        //     返回由字节数组中指定位置的两个字节转换来的 16 位有符号整数。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        //   startIndex:
        //     value 内的起始位置。
        //
        // 返回结果:
        //     由两个字节构成的 16 位有符号整数。
        public static short GetInt16(byte[] value, int startIndex)
        {
            return BitConverter.ToInt16(value, startIndex);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 16 位无符号整数值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        // 返回结果:
        //     用于存放结果的字节数组。
        public static byte[] GetBytes(ushort value)
        {
            byte[] array = new byte[2];
            GetBytes((short)value, array, 0);
            return array;
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 16 位无符号整数值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        public static void GetBytes(ushort value, byte[] buffer)
        {
            GetBytes((short)value, buffer, 0);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 16 位无符号整数值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        //
        //   startIndex:
        //     buffer 内的起始位置。
        public static void GetBytes(ushort value, byte[] buffer, int startIndex)
        {
            GetBytes((short)value, buffer, startIndex);
        }

        //
        // 摘要:
        //     返回由字节数组中前两个字节转换来的 16 位无符号整数。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        // 返回结果:
        //     由两个字节构成的 16 位无符号整数。
        public static ushort GetUInt16(byte[] value)
        {
            return BitConverter.ToUInt16(value, 0);
        }

        //
        // 摘要:
        //     返回由字节数组中指定位置的两个字节转换来的 16 位无符号整数。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        //   startIndex:
        //     value 内的起始位置。
        //
        // 返回结果:
        //     由两个字节构成的 16 位无符号整数。
        public static ushort GetUInt16(byte[] value, int startIndex)
        {
            return BitConverter.ToUInt16(value, startIndex);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 32 位有符号整数值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        // 返回结果:
        //     用于存放结果的字节数组。
        public static byte[] GetBytes(int value)
        {
            byte[] array = new byte[4];
            GetBytes(value, array, 0);
            return array;
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 32 位有符号整数值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        public static void GetBytes(int value, byte[] buffer)
        {
            GetBytes(value, buffer, 0);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 32 位有符号整数值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        //
        //   startIndex:
        //     buffer 内的起始位置。
        public unsafe static void GetBytes(int value, byte[] buffer, int startIndex)
        {
            if (buffer == null)
            {
                throw new Exception("Buffer is invalid.");
            }

            if (startIndex < 0 || startIndex + 4 > buffer.Length)
            {
                throw new Exception("Start index is invalid.");
            }

            fixed (byte* ptr = buffer)
            {
                *(int*)(ptr + startIndex) = value;
            }
        }

        //
        // 摘要:
        //     返回由字节数组中前四个字节转换来的 32 位有符号整数。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        // 返回结果:
        //     由四个字节构成的 32 位有符号整数。
        public static int GetInt32(byte[] value)
        {
            return BitConverter.ToInt32(value, 0);
        }

        //
        // 摘要:
        //     返回由字节数组中指定位置的四个字节转换来的 32 位有符号整数。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        //   startIndex:
        //     value 内的起始位置。
        //
        // 返回结果:
        //     由四个字节构成的 32 位有符号整数。
        public static int GetInt32(byte[] value, int startIndex)
        {
            return BitConverter.ToInt32(value, startIndex);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 32 位无符号整数值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        // 返回结果:
        //     用于存放结果的字节数组。
        public static byte[] GetBytes(uint value)
        {
            byte[] array = new byte[4];
            GetBytes((int)value, array, 0);
            return array;
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 32 位无符号整数值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        public static void GetBytes(uint value, byte[] buffer)
        {
            GetBytes((int)value, buffer, 0);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 32 位无符号整数值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        //
        //   startIndex:
        //     buffer 内的起始位置。
        public static void GetBytes(uint value, byte[] buffer, int startIndex)
        {
            GetBytes((int)value, buffer, startIndex);
        }

        //
        // 摘要:
        //     返回由字节数组中前四个字节转换来的 32 位无符号整数。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        // 返回结果:
        //     由四个字节构成的 32 位无符号整数。
        public static uint GetUInt32(byte[] value)
        {
            return BitConverter.ToUInt32(value, 0);
        }

        //
        // 摘要:
        //     返回由字节数组中指定位置的四个字节转换来的 32 位无符号整数。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        //   startIndex:
        //     value 内的起始位置。
        //
        // 返回结果:
        //     由四个字节构成的 32 位无符号整数。
        public static uint GetUInt32(byte[] value, int startIndex)
        {
            return BitConverter.ToUInt32(value, startIndex);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 64 位有符号整数值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        // 返回结果:
        //     用于存放结果的字节数组。
        public static byte[] GetBytes(long value)
        {
            byte[] array = new byte[8];
            GetBytes(value, array, 0);
            return array;
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 64 位有符号整数值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        public static void GetBytes(long value, byte[] buffer)
        {
            GetBytes(value, buffer, 0);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 64 位有符号整数值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        //
        //   startIndex:
        //     buffer 内的起始位置。
        public unsafe static void GetBytes(long value, byte[] buffer, int startIndex)
        {
            if (buffer == null)
            {
                throw new Exception("Buffer is invalid.");
            }

            if (startIndex < 0 || startIndex + 8 > buffer.Length)
            {
                throw new Exception("Start index is invalid.");
            }

            fixed (byte* ptr = buffer)
            {
                *(long*)(ptr + startIndex) = value;
            }
        }

        //
        // 摘要:
        //     返回由字节数组中前八个字节转换来的 64 位有符号整数。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        // 返回结果:
        //     由八个字节构成的 64 位有符号整数。
        public static long GetInt64(byte[] value)
        {
            return BitConverter.ToInt64(value, 0);
        }

        //
        // 摘要:
        //     返回由字节数组中指定位置的八个字节转换来的 64 位有符号整数。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        //   startIndex:
        //     value 内的起始位置。
        //
        // 返回结果:
        //     由八个字节构成的 64 位有符号整数。
        public static long GetInt64(byte[] value, int startIndex)
        {
            return BitConverter.ToInt64(value, startIndex);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 64 位无符号整数值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        // 返回结果:
        //     用于存放结果的字节数组。
        public static byte[] GetBytes(ulong value)
        {
            byte[] array = new byte[8];
            GetBytes((long)value, array, 0);
            return array;
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 64 位无符号整数值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        public static void GetBytes(ulong value, byte[] buffer)
        {
            GetBytes((long)value, buffer, 0);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的 64 位无符号整数值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        //
        //   startIndex:
        //     buffer 内的起始位置。
        public static void GetBytes(ulong value, byte[] buffer, int startIndex)
        {
            GetBytes((long)value, buffer, startIndex);
        }

        //
        // 摘要:
        //     返回由字节数组中前八个字节转换来的 64 位无符号整数。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        // 返回结果:
        //     由八个字节构成的 64 位无符号整数。
        public static ulong GetUInt64(byte[] value)
        {
            return BitConverter.ToUInt64(value, 0);
        }

        //
        // 摘要:
        //     返回由字节数组中指定位置的八个字节转换来的 64 位无符号整数。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        //   startIndex:
        //     value 内的起始位置。
        //
        // 返回结果:
        //     由八个字节构成的 64 位无符号整数。
        public static ulong GetUInt64(byte[] value, int startIndex)
        {
            return BitConverter.ToUInt64(value, startIndex);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的单精度浮点值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        // 返回结果:
        //     用于存放结果的字节数组。
        public unsafe static byte[] GetBytes(float value)
        {
            byte[] array = new byte[4];
            GetBytes(*(int*)(&value), array, 0);
            return array;
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的单精度浮点值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        public unsafe static void GetBytes(float value, byte[] buffer)
        {
            GetBytes(*(int*)(&value), buffer, 0);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的单精度浮点值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        //
        //   startIndex:
        //     buffer 内的起始位置。
        public unsafe static void GetBytes(float value, byte[] buffer, int startIndex)
        {
            GetBytes(*(int*)(&value), buffer, startIndex);
        }

        //
        // 摘要:
        //     返回由字节数组中前四个字节转换来的单精度浮点数。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        // 返回结果:
        //     由四个字节构成的单精度浮点数。
        public static float GetSingle(byte[] value)
        {
            return BitConverter.ToSingle(value, 0);
        }

        //
        // 摘要:
        //     返回由字节数组中指定位置的四个字节转换来的单精度浮点数。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        //   startIndex:
        //     value 内的起始位置。
        //
        // 返回结果:
        //     由四个字节构成的单精度浮点数。
        public static float GetSingle(byte[] value, int startIndex)
        {
            return BitConverter.ToSingle(value, startIndex);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的双精度浮点值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        // 返回结果:
        //     用于存放结果的字节数组。
        public unsafe static byte[] GetBytes(double value)
        {
            byte[] array = new byte[8];
            GetBytes(*(long*)(&value), array, 0);
            return array;
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的双精度浮点值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        public unsafe static void GetBytes(double value, byte[] buffer)
        {
            GetBytes(*(long*)(&value), buffer, 0);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定的双精度浮点值。
        //
        // 参数:
        //   value:
        //     要转换的数字。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        //
        //   startIndex:
        //     buffer 内的起始位置。
        public unsafe static void GetBytes(double value, byte[] buffer, int startIndex)
        {
            GetBytes(*(long*)(&value), buffer, startIndex);
        }

        //
        // 摘要:
        //     返回由字节数组中前八个字节转换来的双精度浮点数。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        // 返回结果:
        //     由八个字节构成的双精度浮点数。
        public static double GetDouble(byte[] value)
        {
            return BitConverter.ToDouble(value, 0);
        }

        //
        // 摘要:
        //     返回由字节数组中指定位置的八个字节转换来的双精度浮点数。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        //   startIndex:
        //     value 内的起始位置。
        //
        // 返回结果:
        //     由八个字节构成的双精度浮点数。
        public static double GetDouble(byte[] value, int startIndex)
        {
            return BitConverter.ToDouble(value, startIndex);
        }

        //
        // 摘要:
        //     以字节数组的形式获取 UTF-8 编码的字符串。
        //
        // 参数:
        //   value:
        //     要转换的字符串。
        //
        // 返回结果:
        //     用于存放结果的字节数组。
        public static byte[] GetBytes(string value)
        {
            return GetBytes(value, Encoding.UTF8);
        }

        //
        // 摘要:
        //     以字节数组的形式获取 UTF-8 编码的字符串。
        //
        // 参数:
        //   value:
        //     要转换的字符串。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        //
        // 返回结果:
        //     buffer 内实际填充了多少字节。
        public static int GetBytes(string value, byte[] buffer)
        {
            return GetBytes(value, Encoding.UTF8, buffer, 0);
        }

        //
        // 摘要:
        //     以字节数组的形式获取 UTF-8 编码的字符串。
        //
        // 参数:
        //   value:
        //     要转换的字符串。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        //
        //   startIndex:
        //     buffer 内的起始位置。
        //
        // 返回结果:
        //     buffer 内实际填充了多少字节。
        public static int GetBytes(string value, byte[] buffer, int startIndex)
        {
            return GetBytes(value, Encoding.UTF8, buffer, startIndex);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定编码的字符串。
        //
        // 参数:
        //   value:
        //     要转换的字符串。
        //
        //   encoding:
        //     要使用的编码。
        //
        // 返回结果:
        //     用于存放结果的字节数组。
        public static byte[] GetBytes(string value, Encoding encoding)
        {
            if (value == null)
            {
                throw new Exception("Value is invalid.");
            }

            if (encoding == null)
            {
                throw new Exception("Encoding is invalid.");
            }

            return encoding.GetBytes(value);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定编码的字符串。
        //
        // 参数:
        //   value:
        //     要转换的字符串。
        //
        //   encoding:
        //     要使用的编码。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        //
        // 返回结果:
        //     buffer 内实际填充了多少字节。
        public static int GetBytes(string value, Encoding encoding, byte[] buffer)
        {
            return GetBytes(value, encoding, buffer, 0);
        }

        //
        // 摘要:
        //     以字节数组的形式获取指定编码的字符串。
        //
        // 参数:
        //   value:
        //     要转换的字符串。
        //
        //   encoding:
        //     要使用的编码。
        //
        //   buffer:
        //     用于存放结果的字节数组。
        //
        //   startIndex:
        //     buffer 内的起始位置。
        //
        // 返回结果:
        //     buffer 内实际填充了多少字节。
        public static int GetBytes(string value, Encoding encoding, byte[] buffer, int startIndex)
        {
            if (value == null)
            {
                throw new Exception("Value is invalid.");
            }

            if (encoding == null)
            {
                throw new Exception("Encoding is invalid.");
            }

            return encoding.GetBytes(value, 0, value.Length, buffer, startIndex);
        }

        //
        // 摘要:
        //     返回由字节数组使用 UTF-8 编码转换成的字符串。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        // 返回结果:
        //     转换后的字符串。
        public static string GetString(byte[] value)
        {
            return GetString(value, Encoding.UTF8);
        }

        //
        // 摘要:
        //     返回由字节数组使用指定编码转换成的字符串。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        //   encoding:
        //     要使用的编码。
        //
        // 返回结果:
        //     转换后的字符串。
        public static string GetString(byte[] value, Encoding encoding)
        {
            if (value == null)
            {
                throw new Exception("Value is invalid.");
            }

            if (encoding == null)
            {
                throw new Exception("Encoding is invalid.");
            }

            return encoding.GetString(value);
        }

        //
        // 摘要:
        //     返回由字节数组使用 UTF-8 编码转换成的字符串。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        //   startIndex:
        //     value 内的起始位置。
        //
        //   length:
        //     长度。
        //
        // 返回结果:
        //     转换后的字符串。
        public static string GetString(byte[] value, int startIndex, int length)
        {
            return GetString(value, startIndex, length, Encoding.UTF8);
        }

        //
        // 摘要:
        //     返回由字节数组使用指定编码转换成的字符串。
        //
        // 参数:
        //   value:
        //     字节数组。
        //
        //   startIndex:
        //     value 内的起始位置。
        //
        //   length:
        //     长度。
        //
        //   encoding:
        //     要使用的编码。
        //
        // 返回结果:
        //     转换后的字符串。
        public static string GetString(byte[] value, int startIndex, int length, Encoding encoding)
        {
            if (value == null)
            {
                throw new Exception("Value is invalid.");
            }

            if (encoding == null)
            {
                throw new Exception("Encoding is invalid.");
            }

            return encoding.GetString(value, startIndex, length);
        }
    }

    //
    // 摘要:
    //     加密解密相关的实用函数。
    public static class Encryption
    {
        internal const int QuickEncryptLength = 220;

        //
        // 摘要:
        //     将 bytes 使用 code 做异或运算的快速版本。
        //
        // 参数:
        //   bytes:
        //     原始二进制流。
        //
        //   code:
        //     异或二进制流。
        //
        // 返回结果:
        //     异或后的二进制流。
        public static byte[] GetQuickXorBytes(byte[] bytes, byte[] code)
        {
            return GetXorBytes(bytes, 0, 220, code);
        }

        //
        // 摘要:
        //     将 bytes 使用 code 做异或运算的快速版本。此方法将复用并改写传入的 bytes 作为返回值，而不额外分配内存空间。
        //
        // 参数:
        //   bytes:
        //     原始及异或后的二进制流。
        //
        //   code:
        //     异或二进制流。
        public static void GetQuickSelfXorBytes(byte[] bytes, byte[] code)
        {
            GetSelfXorBytes(bytes, 0, 220, code);
        }

        //
        // 摘要:
        //     将 bytes 使用 code 做异或运算。
        //
        // 参数:
        //   bytes:
        //     原始二进制流。
        //
        //   code:
        //     异或二进制流。
        //
        // 返回结果:
        //     异或后的二进制流。
        public static byte[] GetXorBytes(byte[] bytes, byte[] code)
        {
            if (bytes == null)
            {
                return null;
            }

            return GetXorBytes(bytes, 0, bytes.Length, code);
        }

        //
        // 摘要:
        //     将 bytes 使用 code 做异或运算。此方法将复用并改写传入的 bytes 作为返回值，而不额外分配内存空间。
        //
        // 参数:
        //   bytes:
        //     原始及异或后的二进制流。
        //
        //   code:
        //     异或二进制流。
        public static void GetSelfXorBytes(byte[] bytes, byte[] code)
        {
            if (bytes != null)
            {
                GetSelfXorBytes(bytes, 0, bytes.Length, code);
            }
        }

        //
        // 摘要:
        //     将 bytes 使用 code 做异或运算。
        //
        // 参数:
        //   bytes:
        //     原始二进制流。
        //
        //   startIndex:
        //     异或计算的开始位置。
        //
        //   length:
        //     异或计算长度，若小于 0，则计算整个二进制流。
        //
        //   code:
        //     异或二进制流。
        //
        // 返回结果:
        //     异或后的二进制流。
        public static byte[] GetXorBytes(byte[] bytes, int startIndex, int length, byte[] code)
        {
            if (bytes == null)
            {
                return null;
            }

            int num = bytes.Length;
            byte[] array = new byte[num];
            Array.Copy(bytes, 0, array, 0, num);
            GetSelfXorBytes(array, startIndex, length, code);
            return array;
        }

        //
        // 摘要:
        //     将 bytes 使用 code 做异或运算。此方法将复用并改写传入的 bytes 作为返回值，而不额外分配内存空间。
        //
        // 参数:
        //   bytes:
        //     原始及异或后的二进制流。
        //
        //   startIndex:
        //     异或计算的开始位置。
        //
        //   length:
        //     异或计算长度。
        //
        //   code:
        //     异或二进制流。
        public static void GetSelfXorBytes(byte[] bytes, int startIndex, int length, byte[] code)
        {
            if (bytes != null)
            {
                if (code == null)
                {
                    throw new Exception("Code is invalid.");
                }

                int num = code.Length;
                if (num <= 0)
                {
                    throw new Exception("Code length is invalid.");
                }

                if (startIndex < 0 || length < 0 || startIndex + length > bytes.Length)
                {
                    throw new Exception("Start index or length is invalid.");
                }

                int num2 = startIndex % num;
                for (int i = startIndex; i < length; i++)
                {
                    bytes[i] ^= code[num2++];
                    num2 %= num;
                }
            }
        }
    }

    //
    // 摘要:
    //     JSON 相关的实用函数。
    public static class Json
    {
        //
        // 摘要:
        //     JSON 辅助器接口。
        public interface IJsonHelper
        {
            //
            // 摘要:
            //     将对象序列化为 JSON 字符串。
            //
            // 参数:
            //   obj:
            //     要序列化的对象。
            //
            // 返回结果:
            //     序列化后的 JSON 字符串。
            string ToJson(object obj);

            //
            // 摘要:
            //     将 JSON 字符串反序列化为对象。
            //
            // 参数:
            //   json:
            //     要反序列化的 JSON 字符串。
            //
            // 类型参数:
            //   T:
            //     对象类型。
            //
            // 返回结果:
            //     反序列化后的对象。
            T ToObject<T>(string json);

            //
            // 摘要:
            //     将 JSON 字符串反序列化为对象。
            //
            // 参数:
            //   objectType:
            //     对象类型。
            //
            //   json:
            //     要反序列化的 JSON 字符串。
            //
            // 返回结果:
            //     反序列化后的对象。
            object ToObject(Type objectType, string json);
        }

        private static IJsonHelper s_JsonHelper;

        //
        // 摘要:
        //     设置 JSON 辅助器。
        //
        // 参数:
        //   jsonHelper:
        //     要设置的 JSON 辅助器。
        public static void SetJsonHelper(IJsonHelper jsonHelper)
        {
            s_JsonHelper = jsonHelper;
        }

        //
        // 摘要:
        //     将对象序列化为 JSON 字符串。
        //
        // 参数:
        //   obj:
        //     要序列化的对象。
        //
        // 返回结果:
        //     序列化后的 JSON 字符串。
        public static string ToJson(object obj)
        {
            if (s_JsonHelper == null)
            {
                throw new Exception("JSON helper is invalid.");
            }

            try
            {
                return s_JsonHelper.ToJson(obj);
            }
            catch (Exception ex)
            {
                if (ex is Exception)
                {
                    throw;
                }

                throw new Exception(Text.Format("Can not convert to JSON with exception '{0}'.", ex.ToString()), ex);
            }
        }

        //
        // 摘要:
        //     将 JSON 字符串反序列化为对象。
        //
        // 参数:
        //   json:
        //     要反序列化的 JSON 字符串。
        //
        // 类型参数:
        //   T:
        //     对象类型。
        //
        // 返回结果:
        //     反序列化后的对象。
        public static T ToObject<T>(string json)
        {
            if (s_JsonHelper == null)
            {
                throw new Exception("JSON helper is invalid.");
            }

            try
            {
                return s_JsonHelper.ToObject<T>(json);
            }
            catch (Exception ex)
            {
                if (ex is Exception)
                {
                    throw;
                }

                throw new Exception(Text.Format("Can not convert to object with exception '{0}'.", ex.ToString()), ex);
            }
        }

        //
        // 摘要:
        //     将 JSON 字符串反序列化为对象。
        //
        // 参数:
        //   objectType:
        //     对象类型。
        //
        //   json:
        //     要反序列化的 JSON 字符串。
        //
        // 返回结果:
        //     反序列化后的对象。
        public static object ToObject(Type objectType, string json)
        {
            if (s_JsonHelper == null)
            {
                throw new Exception("JSON helper is invalid.");
            }

            if ((object)objectType == null)
            {
                throw new Exception("Object type is invalid.");
            }

            try
            {
                return s_JsonHelper.ToObject(objectType, json);
            }
            catch (Exception ex)
            {
                if (ex is Exception)
                {
                    throw;
                }

                throw new Exception(Text.Format("Can not convert to object with exception '{0}'.", ex.ToString()), ex);
            }
        }
    }

    //
    // 摘要:
    //     Marshal 相关的实用函数。
    public static class Marshal
    {
        private const int BlockSize = 4096;

        private static IntPtr s_CachedHGlobalPtr = IntPtr.Zero;

        private static int s_CachedHGlobalSize = 0;

        //
        // 摘要:
        //     获取缓存的从进程的非托管内存中分配的内存的大小。
        public static int CachedHGlobalSize => s_CachedHGlobalSize;

        //
        // 摘要:
        //     确保从进程的非托管内存中分配足够大小的内存并缓存。
        //
        // 参数:
        //   ensureSize:
        //     要确保从进程的非托管内存中分配内存的大小。
        public static void EnsureCachedHGlobalSize(int ensureSize)
        {
            if (ensureSize < 0)
            {
                throw new Exception("Ensure size is invalid.");
            }

            if (s_CachedHGlobalPtr == IntPtr.Zero || s_CachedHGlobalSize < ensureSize)
            {
                FreeCachedHGlobal();
                int cb = (ensureSize - 1 + 4096) / 4096 * 4096;
                s_CachedHGlobalPtr = System.Runtime.InteropServices.Marshal.AllocHGlobal(cb);
                s_CachedHGlobalSize = cb;
            }
        }

        //
        // 摘要:
        //     释放缓存的从进程的非托管内存中分配的内存。
        public static void FreeCachedHGlobal()
        {
            if (s_CachedHGlobalPtr != IntPtr.Zero)
            {
                System.Runtime.InteropServices.Marshal.FreeHGlobal(s_CachedHGlobalPtr);
                s_CachedHGlobalPtr = IntPtr.Zero;
                s_CachedHGlobalSize = 0;
            }
        }

        //
        // 摘要:
        //     将数据从对象转换为二进制流。
        //
        // 参数:
        //   structure:
        //     要转换的对象。
        //
        // 类型参数:
        //   T:
        //     要转换的对象的类型。
        //
        // 返回结果:
        //     存储转换结果的二进制流。
        public static byte[] StructureToBytes<T>(T structure)
        {
            return StructureToBytes(structure, System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)));
        }

        //
        // 摘要:
        //     将数据从对象转换为二进制流。
        //
        // 参数:
        //   structure:
        //     要转换的对象。
        //
        //   structureSize:
        //     要转换的对象的大小。
        //
        // 类型参数:
        //   T:
        //     要转换的对象的类型。
        //
        // 返回结果:
        //     存储转换结果的二进制流。
        internal static byte[] StructureToBytes<T>(T structure, int structureSize)
        {
            if (structureSize < 0)
            {
                throw new Exception("Structure size is invalid.");
            }

            EnsureCachedHGlobalSize(structureSize);
            System.Runtime.InteropServices.Marshal.StructureToPtr((object)structure, s_CachedHGlobalPtr, fDeleteOld: true);
            byte[] array = new byte[structureSize];
            System.Runtime.InteropServices.Marshal.Copy(s_CachedHGlobalPtr, array, 0, structureSize);
            return array;
        }

        //
        // 摘要:
        //     将数据从对象转换为二进制流。
        //
        // 参数:
        //   structure:
        //     要转换的对象。
        //
        //   result:
        //     存储转换结果的二进制流。
        //
        // 类型参数:
        //   T:
        //     要转换的对象的类型。
        public static void StructureToBytes<T>(T structure, byte[] result)
        {
            StructureToBytes(structure, System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)), result, 0);
        }

        //
        // 摘要:
        //     将数据从对象转换为二进制流。
        //
        // 参数:
        //   structure:
        //     要转换的对象。
        //
        //   structureSize:
        //     要转换的对象的大小。
        //
        //   result:
        //     存储转换结果的二进制流。
        //
        // 类型参数:
        //   T:
        //     要转换的对象的类型。
        internal static void StructureToBytes<T>(T structure, int structureSize, byte[] result)
        {
            StructureToBytes(structure, structureSize, result, 0);
        }

        //
        // 摘要:
        //     将数据从对象转换为二进制流。
        //
        // 参数:
        //   structure:
        //     要转换的对象。
        //
        //   result:
        //     存储转换结果的二进制流。
        //
        //   startIndex:
        //     写入存储转换结果的二进制流的起始位置。
        //
        // 类型参数:
        //   T:
        //     要转换的对象的类型。
        public static void StructureToBytes<T>(T structure, byte[] result, int startIndex)
        {
            StructureToBytes(structure, System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)), result, startIndex);
        }

        //
        // 摘要:
        //     将数据从对象转换为二进制流。
        //
        // 参数:
        //   structure:
        //     要转换的对象。
        //
        //   structureSize:
        //     要转换的对象的大小。
        //
        //   result:
        //     存储转换结果的二进制流。
        //
        //   startIndex:
        //     写入存储转换结果的二进制流的起始位置。
        //
        // 类型参数:
        //   T:
        //     要转换的对象的类型。
        internal static void StructureToBytes<T>(T structure, int structureSize, byte[] result, int startIndex)
        {
            if (structureSize < 0)
            {
                throw new Exception("Structure size is invalid.");
            }

            if (result == null)
            {
                throw new Exception("Result is invalid.");
            }

            if (startIndex < 0)
            {
                throw new Exception("Start index is invalid.");
            }

            if (startIndex + structureSize > result.Length)
            {
                throw new Exception("Result length is not enough.");
            }

            EnsureCachedHGlobalSize(structureSize);
            System.Runtime.InteropServices.Marshal.StructureToPtr((object)structure, s_CachedHGlobalPtr, fDeleteOld: true);
            System.Runtime.InteropServices.Marshal.Copy(s_CachedHGlobalPtr, result, startIndex, structureSize);
        }

        //
        // 摘要:
        //     将数据从二进制流转换为对象。
        //
        // 参数:
        //   buffer:
        //     要转换的二进制流。
        //
        // 类型参数:
        //   T:
        //     要转换的对象的类型。
        //
        // 返回结果:
        //     存储转换结果的对象。
        public static T BytesToStructure<T>(byte[] buffer)
        {
            return BytesToStructure<T>(System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)), buffer, 0);
        }

        //
        // 摘要:
        //     将数据从二进制流转换为对象。
        //
        // 参数:
        //   buffer:
        //     要转换的二进制流。
        //
        //   startIndex:
        //     读取要转换的二进制流的起始位置。
        //
        // 类型参数:
        //   T:
        //     要转换的对象的类型。
        //
        // 返回结果:
        //     存储转换结果的对象。
        public static T BytesToStructure<T>(byte[] buffer, int startIndex)
        {
            return BytesToStructure<T>(System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)), buffer, startIndex);
        }

        //
        // 摘要:
        //     将数据从二进制流转换为对象。
        //
        // 参数:
        //   structureSize:
        //     要转换的对象的大小。
        //
        //   buffer:
        //     要转换的二进制流。
        //
        // 类型参数:
        //   T:
        //     要转换的对象的类型。
        //
        // 返回结果:
        //     存储转换结果的对象。
        internal static T BytesToStructure<T>(int structureSize, byte[] buffer)
        {
            return BytesToStructure<T>(structureSize, buffer, 0);
        }

        //
        // 摘要:
        //     将数据从二进制流转换为对象。
        //
        // 参数:
        //   structureSize:
        //     要转换的对象的大小。
        //
        //   buffer:
        //     要转换的二进制流。
        //
        //   startIndex:
        //     读取要转换的二进制流的起始位置。
        //
        // 类型参数:
        //   T:
        //     要转换的对象的类型。
        //
        // 返回结果:
        //     存储转换结果的对象。
        internal static T BytesToStructure<T>(int structureSize, byte[] buffer, int startIndex)
        {
            if (structureSize < 0)
            {
                throw new Exception("Structure size is invalid.");
            }

            if (buffer == null)
            {
                throw new Exception("Buffer is invalid.");
            }

            if (startIndex < 0)
            {
                throw new Exception("Start index is invalid.");
            }

            if (startIndex + structureSize > buffer.Length)
            {
                throw new Exception("Buffer length is not enough.");
            }

            EnsureCachedHGlobalSize(structureSize);
            System.Runtime.InteropServices.Marshal.Copy(buffer, startIndex, s_CachedHGlobalPtr, structureSize);
            return (T)System.Runtime.InteropServices.Marshal.PtrToStructure(s_CachedHGlobalPtr, typeof(T));
        }
    }

    //
    // 摘要:
    //     路径相关的实用函数。
    public static class Path
    {
        //
        // 摘要:
        //     获取规范的路径。
        //
        // 参数:
        //   path:
        //     要规范的路径。
        //
        // 返回结果:
        //     规范的路径。
        public static string GetRegularPath(string path)
        {
            return path?.Replace('\\', '/');
        }

        //
        // 摘要:
        //     获取远程格式的路径（带有file:// 或 http:// 前缀）。
        //
        // 参数:
        //   path:
        //     原始路径。
        //
        // 返回结果:
        //     远程格式路径。
        public static string GetRemotePath(string path)
        {
            string regularPath = GetRegularPath(path);
            if (regularPath == null)
            {
                return null;
            }

            if (!regularPath.Contains("://"))
            {
                return ("file:///" + regularPath).Replace("file:////", "file:///");
            }

            return regularPath;
        }

        //
        // 摘要:
        //     移除空文件夹。
        //
        // 参数:
        //   directoryName:
        //     要处理的文件夹名称。
        //
        // 返回结果:
        //     是否移除空文件夹成功。
        public static bool RemoveEmptyDirectory(string directoryName)
        {
            if (string.IsNullOrEmpty(directoryName))
            {
                throw new Exception("Directory name is invalid.");
            }

            try
            {
                if (!Directory.Exists(directoryName))
                {
                    return false;
                }

                string[] directories = Directory.GetDirectories(directoryName, "*");
                int num = directories.Length;
                string[] array = directories;
                for (int i = 0; i < array.Length; i++)
                {
                    if (RemoveEmptyDirectory(array[i]))
                    {
                        num--;
                    }
                }

                if (num > 0)
                {
                    return false;
                }

                if (Directory.GetFiles(directoryName, "*").Length != 0)
                {
                    return false;
                }

                Directory.Delete(directoryName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    //
    // 摘要:
    //     随机相关的实用函数。
    public static class Random
    {
        private static System.Random s_Random = new System.Random((int)DateTime.UtcNow.Ticks);

        //
        // 摘要:
        //     设置随机数种子。
        //
        // 参数:
        //   seed:
        //     随机数种子。
        public static void SetSeed(int seed)
        {
            s_Random = new System.Random(seed);
        }

        //
        // 摘要:
        //     返回非负随机数。
        //
        // 返回结果:
        //     大于等于零且小于 System.Int32.MaxValue 的 32 位带符号整数。
        public static int GetRandom()
        {
            return s_Random.Next();
        }

        //
        // 摘要:
        //     返回一个小于所指定最大值的非负随机数。
        //
        // 参数:
        //   maxValue:
        //     要生成的随机数的上界（随机数不能取该上界值）。maxValue 必须大于等于零。
        //
        // 返回结果:
        //     大于等于零且小于 maxValue 的 32 位带符号整数，即：返回值的范围通常包括零但不包括 maxValue。不过，如果 maxValue 等于零，则返回
        //     maxValue。
        public static int GetRandom(int maxValue)
        {
            return s_Random.Next(maxValue);
        }

        //
        // 摘要:
        //     返回一个指定范围内的随机数。
        //
        // 参数:
        //   minValue:
        //     返回的随机数的下界（随机数可取该下界值）。
        //
        //   maxValue:
        //     返回的随机数的上界（随机数不能取该上界值）。maxValue 必须大于等于 minValue。
        //
        // 返回结果:
        //     一个大于等于 minValue 且小于 maxValue 的 32 位带符号整数，即：返回的值范围包括 minValue 但不包括 maxValue。如果
        //     minValue 等于 maxValue，则返回 minValue。
        public static int GetRandom(int minValue, int maxValue)
        {
            return s_Random.Next(minValue, maxValue);
        }

        //
        // 摘要:
        //     返回一个介于 0.0 和 1.0 之间的随机数。
        //
        // 返回结果:
        //     大于等于 0.0 并且小于 1.0 的双精度浮点数。
        public static double GetRandomDouble()
        {
            return s_Random.NextDouble();
        }

        //
        // 摘要:
        //     用随机数填充指定字节数组的元素。
        //
        // 参数:
        //   buffer:
        //     包含随机数的字节数组。
        public static void GetRandomBytes(byte[] buffer)
        {
            s_Random.NextBytes(buffer);
        }
    }

    //
    // 摘要:
    //     字符相关的实用函数。
    public static class Text
    {
        private const int StringBuilderCapacity = 1024;

        [ThreadStatic]
        private static StringBuilder s_CachedStringBuilder;

        //
        // 摘要:
        //     获取格式化字符串。
        //
        // 参数:
        //   format:
        //     字符串格式。
        //
        //   arg0:
        //     字符串参数 0。
        //
        // 返回结果:
        //     格式化后的字符串。
        public static string Format(string format, object arg0)
        {
            if (format == null)
            {
                throw new Exception("Format is invalid.");
            }

            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(format, arg0);
            return s_CachedStringBuilder.ToString();
        }

        //
        // 摘要:
        //     获取格式化字符串。
        //
        // 参数:
        //   format:
        //     字符串格式。
        //
        //   arg0:
        //     字符串参数 0。
        //
        //   arg1:
        //     字符串参数 1。
        //
        // 返回结果:
        //     格式化后的字符串。
        public static string Format(string format, object arg0, object arg1)
        {
            if (format == null)
            {
                throw new Exception("Format is invalid.");
            }

            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(format, arg0, arg1);
            return s_CachedStringBuilder.ToString();
        }

        //
        // 摘要:
        //     获取格式化字符串。
        //
        // 参数:
        //   format:
        //     字符串格式。
        //
        //   arg0:
        //     字符串参数 0。
        //
        //   arg1:
        //     字符串参数 1。
        //
        //   arg2:
        //     字符串参数 2。
        //
        // 返回结果:
        //     格式化后的字符串。
        public static string Format(string format, object arg0, object arg1, object arg2)
        {
            if (format == null)
            {
                throw new Exception("Format is invalid.");
            }

            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(format, arg0, arg1, arg2);
            return s_CachedStringBuilder.ToString();
        }

        //
        // 摘要:
        //     获取格式化字符串。
        //
        // 参数:
        //   format:
        //     字符串格式。
        //
        //   args:
        //     字符串参数。
        //
        // 返回结果:
        //     格式化后的字符串。
        public static string Format(string format, params object[] args)
        {
            if (format == null)
            {
                throw new Exception("Format is invalid.");
            }

            if (args == null)
            {
                throw new Exception("Args is invalid.");
            }

            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(format, args);
            return s_CachedStringBuilder.ToString();
        }

        private static void CheckCachedStringBuilder()
        {
            if (s_CachedStringBuilder == null)
            {
                s_CachedStringBuilder = new StringBuilder(1024);
            }
        }
    }

    //
    // 摘要:
    //     校验相关的实用函数。
    public static class Verifier
    {
        //
        // 摘要:
        //     CRC32 算法。
        private sealed class Crc32
        {
            private const int TableLength = 256;

            private const uint DefaultPolynomial = 3988292384u;

            private const uint DefaultSeed = uint.MaxValue;

            private readonly uint m_Seed;

            private readonly uint[] m_Table;

            private uint m_Hash;

            public Crc32()
                : this(3988292384u, uint.MaxValue)
            {
            }

            public Crc32(uint polynomial, uint seed)
            {
                m_Seed = seed;
                m_Table = InitializeTable(polynomial);
                m_Hash = seed;
            }

            public void Initialize()
            {
                m_Hash = m_Seed;
            }

            public void HashCore(byte[] bytes, int offset, int length)
            {
                m_Hash = CalculateHash(m_Table, m_Hash, bytes, offset, length);
            }

            public uint HashFinal()
            {
                return ~m_Hash;
            }

            private static uint CalculateHash(uint[] table, uint value, byte[] bytes, int offset, int length)
            {
                int num = offset + length;
                for (int i = offset; i < num; i++)
                {
                    value = (value >> 8) ^ table[bytes[i] ^ (value & 0xFF)];
                }

                return value;
            }

            private static uint[] InitializeTable(uint polynomial)
            {
                uint[] array = new uint[256];
                for (int i = 0; i < 256; i++)
                {
                    uint num = (uint)i;
                    for (int j = 0; j < 8; j++)
                    {
                        num = (((num & 1) != 1) ? (num >> 1) : ((num >> 1) ^ polynomial));
                    }

                    array[i] = num;
                }

                return array;
            }
        }

        private const int CachedBytesLength = 4096;

        private static readonly byte[] s_CachedBytes = new byte[4096];

        private static readonly Crc32 s_Algorithm = new Crc32();

        //
        // 摘要:
        //     计算二进制流的 CRC32。
        //
        // 参数:
        //   bytes:
        //     指定的二进制流。
        //
        // 返回结果:
        //     计算后的 CRC32。
        public static int GetCrc32(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new Exception("Bytes is invalid.");
            }

            return GetCrc32(bytes, 0, bytes.Length);
        }

        //
        // 摘要:
        //     计算二进制流的 CRC32。
        //
        // 参数:
        //   bytes:
        //     指定的二进制流。
        //
        //   offset:
        //     二进制流的偏移。
        //
        //   length:
        //     二进制流的长度。
        //
        // 返回结果:
        //     计算后的 CRC32。
        public static int GetCrc32(byte[] bytes, int offset, int length)
        {
            if (bytes == null)
            {
                throw new Exception("Bytes is invalid.");
            }

            if (offset < 0 || length < 0 || offset + length > bytes.Length)
            {
                throw new Exception("Offset or length is invalid.");
            }

            s_Algorithm.HashCore(bytes, offset, length);
            uint result = s_Algorithm.HashFinal();
            s_Algorithm.Initialize();
            return (int)result;
        }

        //
        // 摘要:
        //     计算二进制流的 CRC32。
        //
        // 参数:
        //   stream:
        //     指定的二进制流。
        //
        // 返回结果:
        //     计算后的 CRC32。
        public static int GetCrc32(Stream stream)
        {
            if (stream == null)
            {
                throw new Exception("Stream is invalid.");
            }

            while (true)
            {
                int num = stream.Read(s_CachedBytes, 0, 4096);
                if (num <= 0)
                {
                    break;
                }

                s_Algorithm.HashCore(s_CachedBytes, 0, num);
            }

            uint result = s_Algorithm.HashFinal();
            s_Algorithm.Initialize();
            Array.Clear(s_CachedBytes, 0, 4096);
            return (int)result;
        }

        //
        // 摘要:
        //     获取 CRC32 数值的二进制数组。
        //
        // 参数:
        //   crc32:
        //     CRC32 数值。
        //
        // 返回结果:
        //     CRC32 数值的二进制数组。
        public static byte[] GetCrc32Bytes(int crc32)
        {
            return new byte[4]
            {
                    (byte)((uint)(crc32 >> 24) & 0xFFu),
                    (byte)((uint)(crc32 >> 16) & 0xFFu),
                    (byte)((uint)(crc32 >> 8) & 0xFFu),
                    (byte)((uint)crc32 & 0xFFu)
            };
        }

        //
        // 摘要:
        //     获取 CRC32 数值的二进制数组。
        //
        // 参数:
        //   crc32:
        //     CRC32 数值。
        //
        //   bytes:
        //     要存放结果的数组。
        public static void GetCrc32Bytes(int crc32, byte[] bytes)
        {
            GetCrc32Bytes(crc32, bytes, 0);
        }

        //
        // 摘要:
        //     获取 CRC32 数值的二进制数组。
        //
        // 参数:
        //   crc32:
        //     CRC32 数值。
        //
        //   bytes:
        //     要存放结果的数组。
        //
        //   offset:
        //     CRC32 数值的二进制数组在结果数组内的起始位置。
        public static void GetCrc32Bytes(int crc32, byte[] bytes, int offset)
        {
            if (bytes == null)
            {
                throw new Exception("Result is invalid.");
            }

            if (offset < 0 || offset + 4 > bytes.Length)
            {
                throw new Exception("Offset or length is invalid.");
            }

            bytes[offset] = (byte)((uint)(crc32 >> 24) & 0xFFu);
            bytes[offset + 1] = (byte)((uint)(crc32 >> 16) & 0xFFu);
            bytes[offset + 2] = (byte)((uint)(crc32 >> 8) & 0xFFu);
            bytes[offset + 3] = (byte)((uint)crc32 & 0xFFu);
        }

        internal static int GetCrc32(Stream stream, byte[] code, int length)
        {
            if (stream == null)
            {
                throw new Exception("Stream is invalid.");
            }

            if (code == null)
            {
                throw new Exception("Code is invalid.");
            }

            int num = code.Length;
            if (num <= 0)
            {
                throw new Exception("Code length is invalid.");
            }

            int num2 = (int)stream.Length;
            if (length < 0 || length > num2)
            {
                length = num2;
            }

            int num3 = 0;
            while (true)
            {
                int num4 = stream.Read(s_CachedBytes, 0, 4096);
                if (num4 <= 0)
                {
                    break;
                }

                if (length > 0)
                {
                    for (int i = 0; i < num4 && i < length; i++)
                    {
                        s_CachedBytes[i] ^= code[num3++];
                        num3 %= num;
                    }

                    length -= num4;
                }

                s_Algorithm.HashCore(s_CachedBytes, 0, num4);
            }

            uint result = s_Algorithm.HashFinal();
            s_Algorithm.Initialize();
            Array.Clear(s_CachedBytes, 0, 4096);
            return (int)result;
        }
    }
}