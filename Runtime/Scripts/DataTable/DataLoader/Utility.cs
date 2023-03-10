using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

//
// ժҪ:
//     ʵ�ú�������
public static class Utility
{
    //
    // ժҪ:
    //     ������ص�ʵ�ú�����
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
        // ժҪ:
        //     ��ȡ�Ѽ��صĳ��򼯡�
        //
        // ���ؽ��:
        //     �Ѽ��صĳ��򼯡�
        public static System.Reflection.Assembly[] GetAssemblies()
        {
            return s_Assemblies;
        }

        //
        // ժҪ:
        //     ��ȡ�Ѽ��صĳ����е��������͡�
        //
        // ���ؽ��:
        //     �Ѽ��صĳ����е��������͡�
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
        // ժҪ:
        //     ��ȡ�Ѽ��صĳ����е��������͡�
        //
        // ����:
        //   results:
        //     �Ѽ��صĳ����е��������͡�
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
        // ժҪ:
        //     ��ȡ�Ѽ��صĳ����е�ָ�����͡�
        //
        // ����:
        //   typeName:
        //     Ҫ��ȡ����������
        //
        // ���ؽ��:
        //     �Ѽ��صĳ����е�ָ�����͡�
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
    // ժҪ:
    //     ѹ����ѹ����ص�ʵ�ú�����
    public static class Compression
    {
        //
        // ժҪ:
        //     ѹ����ѹ���������ӿڡ�
        public interface ICompressionHelper
        {
            //
            // ժҪ:
            //     ѹ�����ݡ�
            //
            // ����:
            //   bytes:
            //     Ҫѹ�������ݵĶ���������
            //
            //   offset:
            //     Ҫѹ�������ݵĶ���������ƫ�ơ�
            //
            //   length:
            //     Ҫѹ�������ݵĶ��������ĳ��ȡ�
            //
            //   compressedStream:
            //     ѹ��������ݵĶ���������
            //
            // ���ؽ��:
            //     �Ƿ�ѹ�����ݳɹ���
            bool Compress(byte[] bytes, int offset, int length, Stream compressedStream);

            //
            // ժҪ:
            //     ѹ�����ݡ�
            //
            // ����:
            //   stream:
            //     Ҫѹ�������ݵĶ���������
            //
            //   compressedStream:
            //     ѹ��������ݵĶ���������
            //
            // ���ؽ��:
            //     �Ƿ�ѹ�����ݳɹ���
            bool Compress(Stream stream, Stream compressedStream);

            //
            // ժҪ:
            //     ��ѹ�����ݡ�
            //
            // ����:
            //   bytes:
            //     Ҫ��ѹ�������ݵĶ���������
            //
            //   offset:
            //     Ҫ��ѹ�������ݵĶ���������ƫ�ơ�
            //
            //   length:
            //     Ҫ��ѹ�������ݵĶ��������ĳ��ȡ�
            //
            //   decompressedStream:
            //     ��ѹ��������ݵĶ���������
            //
            // ���ؽ��:
            //     �Ƿ��ѹ�����ݳɹ���
            bool Decompress(byte[] bytes, int offset, int length, Stream decompressedStream);

            //
            // ժҪ:
            //     ��ѹ�����ݡ�
            //
            // ����:
            //   stream:
            //     Ҫ��ѹ�������ݵĶ���������
            //
            //   decompressedStream:
            //     ��ѹ��������ݵĶ���������
            //
            // ���ؽ��:
            //     �Ƿ��ѹ�����ݳɹ���
            bool Decompress(Stream stream, Stream decompressedStream);
        }

        private static ICompressionHelper s_CompressionHelper;

        //
        // ժҪ:
        //     ����ѹ����ѹ����������
        //
        // ����:
        //   compressionHelper:
        //     Ҫ���õ�ѹ����ѹ����������
        public static void SetCompressionHelper(ICompressionHelper compressionHelper)
        {
            s_CompressionHelper = compressionHelper;
        }

        //
        // ժҪ:
        //     ѹ�����ݡ�
        //
        // ����:
        //   bytes:
        //     Ҫѹ�������ݵĶ���������
        //
        // ���ؽ��:
        //     ѹ��������ݵĶ���������
        public static byte[] Compress(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new Exception("Bytes is invalid.");
            }

            return Compress(bytes, 0, bytes.Length);
        }

        //
        // ժҪ:
        //     ѹ�����ݡ�
        //
        // ����:
        //   bytes:
        //     Ҫѹ�������ݵĶ���������
        //
        //   compressedStream:
        //     ѹ��������ݵĶ���������
        //
        // ���ؽ��:
        //     �Ƿ�ѹ�����ݳɹ���
        public static bool Compress(byte[] bytes, Stream compressedStream)
        {
            if (bytes == null)
            {
                throw new Exception("Bytes is invalid.");
            }

            return Compress(bytes, 0, bytes.Length, compressedStream);
        }

        //
        // ժҪ:
        //     ѹ�����ݡ�
        //
        // ����:
        //   bytes:
        //     Ҫѹ�������ݵĶ���������
        //
        //   offset:
        //     Ҫѹ�������ݵĶ���������ƫ�ơ�
        //
        //   length:
        //     Ҫѹ�������ݵĶ��������ĳ��ȡ�
        //
        // ���ؽ��:
        //     ѹ��������ݵĶ���������
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
        // ժҪ:
        //     ѹ�����ݡ�
        //
        // ����:
        //   bytes:
        //     Ҫѹ�������ݵĶ���������
        //
        //   offset:
        //     Ҫѹ�������ݵĶ���������ƫ�ơ�
        //
        //   length:
        //     Ҫѹ�������ݵĶ��������ĳ��ȡ�
        //
        //   compressedStream:
        //     ѹ��������ݵĶ���������
        //
        // ���ؽ��:
        //     �Ƿ�ѹ�����ݳɹ���
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
        // ժҪ:
        //     ѹ�����ݡ�
        //
        // ����:
        //   stream:
        //     Ҫѹ�������ݵĶ���������
        //
        // ���ؽ��:
        //     ѹ��������ݵĶ���������
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
        // ժҪ:
        //     ѹ�����ݡ�
        //
        // ����:
        //   stream:
        //     Ҫѹ�������ݵĶ���������
        //
        //   compressedStream:
        //     ѹ��������ݵĶ���������
        //
        // ���ؽ��:
        //     �Ƿ�ѹ�����ݳɹ���
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
        // ժҪ:
        //     ��ѹ�����ݡ�
        //
        // ����:
        //   bytes:
        //     Ҫ��ѹ�������ݵĶ���������
        //
        // ���ؽ��:
        //     ��ѹ��������ݵĶ���������
        public static byte[] Decompress(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new Exception("Bytes is invalid.");
            }

            return Decompress(bytes, 0, bytes.Length);
        }

        //
        // ժҪ:
        //     ��ѹ�����ݡ�
        //
        // ����:
        //   bytes:
        //     Ҫ��ѹ�������ݵĶ���������
        //
        //   decompressedStream:
        //     ��ѹ��������ݵĶ���������
        //
        // ���ؽ��:
        //     �Ƿ��ѹ�����ݳɹ���
        public static bool Decompress(byte[] bytes, Stream decompressedStream)
        {
            if (bytes == null)
            {
                throw new Exception("Bytes is invalid.");
            }

            return Decompress(bytes, 0, bytes.Length, decompressedStream);
        }

        //
        // ժҪ:
        //     ��ѹ�����ݡ�
        //
        // ����:
        //   bytes:
        //     Ҫ��ѹ�������ݵĶ���������
        //
        //   offset:
        //     Ҫ��ѹ�������ݵĶ���������ƫ�ơ�
        //
        //   length:
        //     Ҫ��ѹ�������ݵĶ��������ĳ��ȡ�
        //
        // ���ؽ��:
        //     ��ѹ��������ݵĶ���������
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
        // ժҪ:
        //     ��ѹ�����ݡ�
        //
        // ����:
        //   bytes:
        //     Ҫ��ѹ�������ݵĶ���������
        //
        //   offset:
        //     Ҫ��ѹ�������ݵĶ���������ƫ�ơ�
        //
        //   length:
        //     Ҫ��ѹ�������ݵĶ��������ĳ��ȡ�
        //
        //   decompressedStream:
        //     ��ѹ��������ݵĶ���������
        //
        // ���ؽ��:
        //     �Ƿ��ѹ�����ݳɹ���
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
        // ժҪ:
        //     ��ѹ�����ݡ�
        //
        // ����:
        //   stream:
        //     Ҫ��ѹ�������ݵĶ���������
        //
        // ���ؽ��:
        //     �Ƿ��ѹ�����ݳɹ���
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
        // ժҪ:
        //     ��ѹ�����ݡ�
        //
        // ����:
        //   stream:
        //     Ҫ��ѹ�������ݵĶ���������
        //
        //   decompressedStream:
        //     ��ѹ��������ݵĶ���������
        //
        // ���ؽ��:
        //     �Ƿ��ѹ�����ݳɹ���
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
    // ժҪ:
    //     ����ת����ص�ʵ�ú�����
    public static class Converter
    {
        private const float InchesToCentimeters = 2.54f;

        private const float CentimetersToInches = 0.393700778f;

        //
        // ժҪ:
        //     ��ȡ�����ڴ˼�����ṹ�д洢ʱ���ֽ�˳��
        public static bool IsLittleEndian => BitConverter.IsLittleEndian;

        //
        // ժҪ:
        //     ��ȡ��������ĻÿӢ�������
        public static float ScreenDpi { get; set; }

        //
        // ժҪ:
        //     ������ת��Ϊ���ס�
        //
        // ����:
        //   pixels:
        //     ���ء�
        //
        // ���ؽ��:
        //     ���ס�
        public static float GetCentimetersFromPixels(float pixels)
        {
            if (ScreenDpi <= 0f)
            {
                throw new Exception("You must set screen DPI first.");
            }

            return 2.54f * pixels / ScreenDpi;
        }

        //
        // ժҪ:
        //     ������ת��Ϊ���ء�
        //
        // ����:
        //   centimeters:
        //     ���ס�
        //
        // ���ؽ��:
        //     ���ء�
        public static float GetPixelsFromCentimeters(float centimeters)
        {
            if (ScreenDpi <= 0f)
            {
                throw new Exception("You must set screen DPI first.");
            }

            return 0.393700778f * centimeters * ScreenDpi;
        }

        //
        // ժҪ:
        //     ������ת��ΪӢ�硣
        //
        // ����:
        //   pixels:
        //     ���ء�
        //
        // ���ؽ��:
        //     Ӣ�硣
        public static float GetInchesFromPixels(float pixels)
        {
            if (ScreenDpi <= 0f)
            {
                throw new Exception("You must set screen DPI first.");
            }

            return pixels / ScreenDpi;
        }

        //
        // ժҪ:
        //     ��Ӣ��ת��Ϊ���ء�
        //
        // ����:
        //   inches:
        //     Ӣ�硣
        //
        // ���ؽ��:
        //     ���ء�
        public static float GetPixelsFromInches(float inches)
        {
            if (ScreenDpi <= 0f)
            {
                throw new Exception("You must set screen DPI first.");
            }

            return inches * ScreenDpi;
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���Ĳ���ֵ��
        //
        // ����:
        //   value:
        //     Ҫת���Ĳ���ֵ��
        //
        // ���ؽ��:
        //     ���ڴ�Ž�����ֽ����顣
        public static byte[] GetBytes(bool value)
        {
            byte[] array = new byte[1];
            GetBytes(value, array, 0);
            return array;
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���Ĳ���ֵ��
        //
        // ����:
        //   value:
        //     Ҫת���Ĳ���ֵ��
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        public static void GetBytes(bool value, byte[] buffer)
        {
            GetBytes(value, buffer, 0);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���Ĳ���ֵ��
        //
        // ����:
        //   value:
        //     Ҫת���Ĳ���ֵ��
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        //
        //   startIndex:
        //     buffer �ڵ���ʼλ�á�
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
        // ժҪ:
        //     �������ֽ����������ֽ�ת�����Ĳ���ֵ��
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        // ���ؽ��:
        //     ��� value �е����ֽڷ��㣬��Ϊ true������Ϊ false��
        public static bool GetBoolean(byte[] value)
        {
            return BitConverter.ToBoolean(value, 0);
        }

        //
        // ժҪ:
        //     �������ֽ�������ָ��λ�õ�һ���ֽ�ת�����Ĳ���ֵ��
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        //   startIndex:
        //     value �ڵ���ʼλ�á�
        //
        // ���ؽ��:
        //     ��� value ��ָ��λ�õ��ֽڷ��㣬��Ϊ true������Ϊ false��
        public static bool GetBoolean(byte[] value, int startIndex)
        {
            return BitConverter.ToBoolean(value, startIndex);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� Unicode �ַ�ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�����ַ���
        //
        // ���ؽ��:
        //     ���ڴ�Ž�����ֽ����顣
        public static byte[] GetBytes(char value)
        {
            byte[] array = new byte[2];
            GetBytes((short)value, array, 0);
            return array;
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� Unicode �ַ�ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�����ַ���
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        public static void GetBytes(char value, byte[] buffer)
        {
            GetBytes((short)value, buffer, 0);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� Unicode �ַ�ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�����ַ���
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        //
        //   startIndex:
        //     buffer �ڵ���ʼλ�á�
        public static void GetBytes(char value, byte[] buffer, int startIndex)
        {
            GetBytes((short)value, buffer, startIndex);
        }

        //
        // ժҪ:
        //     �������ֽ�������ǰ�����ֽ�ת������ Unicode �ַ���
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        // ���ؽ��:
        //     �������ֽڹ��ɵ��ַ���
        public static char GetChar(byte[] value)
        {
            return BitConverter.ToChar(value, 0);
        }

        //
        // ժҪ:
        //     �������ֽ�������ָ��λ�õ������ֽ�ת������ Unicode �ַ���
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        //   startIndex:
        //     value �ڵ���ʼλ�á�
        //
        // ���ؽ��:
        //     �������ֽڹ��ɵ��ַ���
        public static char GetChar(byte[] value, int startIndex)
        {
            return BitConverter.ToChar(value, startIndex);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� 16 λ�з�������ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        // ���ؽ��:
        //     ���ڴ�Ž�����ֽ����顣
        public static byte[] GetBytes(short value)
        {
            byte[] array = new byte[2];
            GetBytes(value, array, 0);
            return array;
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� 16 λ�з�������ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        public static void GetBytes(short value, byte[] buffer)
        {
            GetBytes(value, buffer, 0);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� 16 λ�з�������ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        //
        //   startIndex:
        //     buffer �ڵ���ʼλ�á�
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
        // ժҪ:
        //     �������ֽ�������ǰ�����ֽ�ת������ 16 λ�з���������
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        // ���ؽ��:
        //     �������ֽڹ��ɵ� 16 λ�з���������
        public static short GetInt16(byte[] value)
        {
            return BitConverter.ToInt16(value, 0);
        }

        //
        // ժҪ:
        //     �������ֽ�������ָ��λ�õ������ֽ�ת������ 16 λ�з���������
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        //   startIndex:
        //     value �ڵ���ʼλ�á�
        //
        // ���ؽ��:
        //     �������ֽڹ��ɵ� 16 λ�з���������
        public static short GetInt16(byte[] value, int startIndex)
        {
            return BitConverter.ToInt16(value, startIndex);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� 16 λ�޷�������ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        // ���ؽ��:
        //     ���ڴ�Ž�����ֽ����顣
        public static byte[] GetBytes(ushort value)
        {
            byte[] array = new byte[2];
            GetBytes((short)value, array, 0);
            return array;
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� 16 λ�޷�������ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        public static void GetBytes(ushort value, byte[] buffer)
        {
            GetBytes((short)value, buffer, 0);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� 16 λ�޷�������ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        //
        //   startIndex:
        //     buffer �ڵ���ʼλ�á�
        public static void GetBytes(ushort value, byte[] buffer, int startIndex)
        {
            GetBytes((short)value, buffer, startIndex);
        }

        //
        // ժҪ:
        //     �������ֽ�������ǰ�����ֽ�ת������ 16 λ�޷���������
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        // ���ؽ��:
        //     �������ֽڹ��ɵ� 16 λ�޷���������
        public static ushort GetUInt16(byte[] value)
        {
            return BitConverter.ToUInt16(value, 0);
        }

        //
        // ժҪ:
        //     �������ֽ�������ָ��λ�õ������ֽ�ת������ 16 λ�޷���������
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        //   startIndex:
        //     value �ڵ���ʼλ�á�
        //
        // ���ؽ��:
        //     �������ֽڹ��ɵ� 16 λ�޷���������
        public static ushort GetUInt16(byte[] value, int startIndex)
        {
            return BitConverter.ToUInt16(value, startIndex);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� 32 λ�з�������ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        // ���ؽ��:
        //     ���ڴ�Ž�����ֽ����顣
        public static byte[] GetBytes(int value)
        {
            byte[] array = new byte[4];
            GetBytes(value, array, 0);
            return array;
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� 32 λ�з�������ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        public static void GetBytes(int value, byte[] buffer)
        {
            GetBytes(value, buffer, 0);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� 32 λ�з�������ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        //
        //   startIndex:
        //     buffer �ڵ���ʼλ�á�
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
        // ժҪ:
        //     �������ֽ�������ǰ�ĸ��ֽ�ת������ 32 λ�з���������
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        // ���ؽ��:
        //     ���ĸ��ֽڹ��ɵ� 32 λ�з���������
        public static int GetInt32(byte[] value)
        {
            return BitConverter.ToInt32(value, 0);
        }

        //
        // ժҪ:
        //     �������ֽ�������ָ��λ�õ��ĸ��ֽ�ת������ 32 λ�з���������
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        //   startIndex:
        //     value �ڵ���ʼλ�á�
        //
        // ���ؽ��:
        //     ���ĸ��ֽڹ��ɵ� 32 λ�з���������
        public static int GetInt32(byte[] value, int startIndex)
        {
            return BitConverter.ToInt32(value, startIndex);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� 32 λ�޷�������ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        // ���ؽ��:
        //     ���ڴ�Ž�����ֽ����顣
        public static byte[] GetBytes(uint value)
        {
            byte[] array = new byte[4];
            GetBytes((int)value, array, 0);
            return array;
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� 32 λ�޷�������ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        public static void GetBytes(uint value, byte[] buffer)
        {
            GetBytes((int)value, buffer, 0);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� 32 λ�޷�������ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        //
        //   startIndex:
        //     buffer �ڵ���ʼλ�á�
        public static void GetBytes(uint value, byte[] buffer, int startIndex)
        {
            GetBytes((int)value, buffer, startIndex);
        }

        //
        // ժҪ:
        //     �������ֽ�������ǰ�ĸ��ֽ�ת������ 32 λ�޷���������
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        // ���ؽ��:
        //     ���ĸ��ֽڹ��ɵ� 32 λ�޷���������
        public static uint GetUInt32(byte[] value)
        {
            return BitConverter.ToUInt32(value, 0);
        }

        //
        // ժҪ:
        //     �������ֽ�������ָ��λ�õ��ĸ��ֽ�ת������ 32 λ�޷���������
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        //   startIndex:
        //     value �ڵ���ʼλ�á�
        //
        // ���ؽ��:
        //     ���ĸ��ֽڹ��ɵ� 32 λ�޷���������
        public static uint GetUInt32(byte[] value, int startIndex)
        {
            return BitConverter.ToUInt32(value, startIndex);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� 64 λ�з�������ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        // ���ؽ��:
        //     ���ڴ�Ž�����ֽ����顣
        public static byte[] GetBytes(long value)
        {
            byte[] array = new byte[8];
            GetBytes(value, array, 0);
            return array;
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� 64 λ�з�������ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        public static void GetBytes(long value, byte[] buffer)
        {
            GetBytes(value, buffer, 0);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� 64 λ�з�������ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        //
        //   startIndex:
        //     buffer �ڵ���ʼλ�á�
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
        // ժҪ:
        //     �������ֽ�������ǰ�˸��ֽ�ת������ 64 λ�з���������
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        // ���ؽ��:
        //     �ɰ˸��ֽڹ��ɵ� 64 λ�з���������
        public static long GetInt64(byte[] value)
        {
            return BitConverter.ToInt64(value, 0);
        }

        //
        // ժҪ:
        //     �������ֽ�������ָ��λ�õİ˸��ֽ�ת������ 64 λ�з���������
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        //   startIndex:
        //     value �ڵ���ʼλ�á�
        //
        // ���ؽ��:
        //     �ɰ˸��ֽڹ��ɵ� 64 λ�з���������
        public static long GetInt64(byte[] value, int startIndex)
        {
            return BitConverter.ToInt64(value, startIndex);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� 64 λ�޷�������ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        // ���ؽ��:
        //     ���ڴ�Ž�����ֽ����顣
        public static byte[] GetBytes(ulong value)
        {
            byte[] array = new byte[8];
            GetBytes((long)value, array, 0);
            return array;
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� 64 λ�޷�������ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        public static void GetBytes(ulong value, byte[] buffer)
        {
            GetBytes((long)value, buffer, 0);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���� 64 λ�޷�������ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        //
        //   startIndex:
        //     buffer �ڵ���ʼλ�á�
        public static void GetBytes(ulong value, byte[] buffer, int startIndex)
        {
            GetBytes((long)value, buffer, startIndex);
        }

        //
        // ժҪ:
        //     �������ֽ�������ǰ�˸��ֽ�ת������ 64 λ�޷���������
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        // ���ؽ��:
        //     �ɰ˸��ֽڹ��ɵ� 64 λ�޷���������
        public static ulong GetUInt64(byte[] value)
        {
            return BitConverter.ToUInt64(value, 0);
        }

        //
        // ժҪ:
        //     �������ֽ�������ָ��λ�õİ˸��ֽ�ת������ 64 λ�޷���������
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        //   startIndex:
        //     value �ڵ���ʼλ�á�
        //
        // ���ؽ��:
        //     �ɰ˸��ֽڹ��ɵ� 64 λ�޷���������
        public static ulong GetUInt64(byte[] value, int startIndex)
        {
            return BitConverter.ToUInt64(value, startIndex);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���ĵ����ȸ���ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        // ���ؽ��:
        //     ���ڴ�Ž�����ֽ����顣
        public unsafe static byte[] GetBytes(float value)
        {
            byte[] array = new byte[4];
            GetBytes(*(int*)(&value), array, 0);
            return array;
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���ĵ����ȸ���ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        public unsafe static void GetBytes(float value, byte[] buffer)
        {
            GetBytes(*(int*)(&value), buffer, 0);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ���ĵ����ȸ���ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        //
        //   startIndex:
        //     buffer �ڵ���ʼλ�á�
        public unsafe static void GetBytes(float value, byte[] buffer, int startIndex)
        {
            GetBytes(*(int*)(&value), buffer, startIndex);
        }

        //
        // ժҪ:
        //     �������ֽ�������ǰ�ĸ��ֽ�ת�����ĵ����ȸ�������
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        // ���ؽ��:
        //     ���ĸ��ֽڹ��ɵĵ����ȸ�������
        public static float GetSingle(byte[] value)
        {
            return BitConverter.ToSingle(value, 0);
        }

        //
        // ժҪ:
        //     �������ֽ�������ָ��λ�õ��ĸ��ֽ�ת�����ĵ����ȸ�������
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        //   startIndex:
        //     value �ڵ���ʼλ�á�
        //
        // ���ؽ��:
        //     ���ĸ��ֽڹ��ɵĵ����ȸ�������
        public static float GetSingle(byte[] value, int startIndex)
        {
            return BitConverter.ToSingle(value, startIndex);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ����˫���ȸ���ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        // ���ؽ��:
        //     ���ڴ�Ž�����ֽ����顣
        public unsafe static byte[] GetBytes(double value)
        {
            byte[] array = new byte[8];
            GetBytes(*(long*)(&value), array, 0);
            return array;
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ����˫���ȸ���ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        public unsafe static void GetBytes(double value, byte[] buffer)
        {
            GetBytes(*(long*)(&value), buffer, 0);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ����˫���ȸ���ֵ��
        //
        // ����:
        //   value:
        //     Ҫת�������֡�
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        //
        //   startIndex:
        //     buffer �ڵ���ʼλ�á�
        public unsafe static void GetBytes(double value, byte[] buffer, int startIndex)
        {
            GetBytes(*(long*)(&value), buffer, startIndex);
        }

        //
        // ժҪ:
        //     �������ֽ�������ǰ�˸��ֽ�ת������˫���ȸ�������
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        // ���ؽ��:
        //     �ɰ˸��ֽڹ��ɵ�˫���ȸ�������
        public static double GetDouble(byte[] value)
        {
            return BitConverter.ToDouble(value, 0);
        }

        //
        // ժҪ:
        //     �������ֽ�������ָ��λ�õİ˸��ֽ�ת������˫���ȸ�������
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        //   startIndex:
        //     value �ڵ���ʼλ�á�
        //
        // ���ؽ��:
        //     �ɰ˸��ֽڹ��ɵ�˫���ȸ�������
        public static double GetDouble(byte[] value, int startIndex)
        {
            return BitConverter.ToDouble(value, startIndex);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡ UTF-8 ������ַ�����
        //
        // ����:
        //   value:
        //     Ҫת�����ַ�����
        //
        // ���ؽ��:
        //     ���ڴ�Ž�����ֽ����顣
        public static byte[] GetBytes(string value)
        {
            return GetBytes(value, Encoding.UTF8);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡ UTF-8 ������ַ�����
        //
        // ����:
        //   value:
        //     Ҫת�����ַ�����
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        //
        // ���ؽ��:
        //     buffer ��ʵ������˶����ֽڡ�
        public static int GetBytes(string value, byte[] buffer)
        {
            return GetBytes(value, Encoding.UTF8, buffer, 0);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡ UTF-8 ������ַ�����
        //
        // ����:
        //   value:
        //     Ҫת�����ַ�����
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        //
        //   startIndex:
        //     buffer �ڵ���ʼλ�á�
        //
        // ���ؽ��:
        //     buffer ��ʵ������˶����ֽڡ�
        public static int GetBytes(string value, byte[] buffer, int startIndex)
        {
            return GetBytes(value, Encoding.UTF8, buffer, startIndex);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ��������ַ�����
        //
        // ����:
        //   value:
        //     Ҫת�����ַ�����
        //
        //   encoding:
        //     Ҫʹ�õı��롣
        //
        // ���ؽ��:
        //     ���ڴ�Ž�����ֽ����顣
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
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ��������ַ�����
        //
        // ����:
        //   value:
        //     Ҫת�����ַ�����
        //
        //   encoding:
        //     Ҫʹ�õı��롣
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        //
        // ���ؽ��:
        //     buffer ��ʵ������˶����ֽڡ�
        public static int GetBytes(string value, Encoding encoding, byte[] buffer)
        {
            return GetBytes(value, encoding, buffer, 0);
        }

        //
        // ժҪ:
        //     ���ֽ��������ʽ��ȡָ��������ַ�����
        //
        // ����:
        //   value:
        //     Ҫת�����ַ�����
        //
        //   encoding:
        //     Ҫʹ�õı��롣
        //
        //   buffer:
        //     ���ڴ�Ž�����ֽ����顣
        //
        //   startIndex:
        //     buffer �ڵ���ʼλ�á�
        //
        // ���ؽ��:
        //     buffer ��ʵ������˶����ֽڡ�
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
        // ժҪ:
        //     �������ֽ�����ʹ�� UTF-8 ����ת���ɵ��ַ�����
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        // ���ؽ��:
        //     ת������ַ�����
        public static string GetString(byte[] value)
        {
            return GetString(value, Encoding.UTF8);
        }

        //
        // ժҪ:
        //     �������ֽ�����ʹ��ָ������ת���ɵ��ַ�����
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        //   encoding:
        //     Ҫʹ�õı��롣
        //
        // ���ؽ��:
        //     ת������ַ�����
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
        // ժҪ:
        //     �������ֽ�����ʹ�� UTF-8 ����ת���ɵ��ַ�����
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        //   startIndex:
        //     value �ڵ���ʼλ�á�
        //
        //   length:
        //     ���ȡ�
        //
        // ���ؽ��:
        //     ת������ַ�����
        public static string GetString(byte[] value, int startIndex, int length)
        {
            return GetString(value, startIndex, length, Encoding.UTF8);
        }

        //
        // ժҪ:
        //     �������ֽ�����ʹ��ָ������ת���ɵ��ַ�����
        //
        // ����:
        //   value:
        //     �ֽ����顣
        //
        //   startIndex:
        //     value �ڵ���ʼλ�á�
        //
        //   length:
        //     ���ȡ�
        //
        //   encoding:
        //     Ҫʹ�õı��롣
        //
        // ���ؽ��:
        //     ת������ַ�����
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
    // ժҪ:
    //     ���ܽ�����ص�ʵ�ú�����
    public static class Encryption
    {
        internal const int QuickEncryptLength = 220;

        //
        // ժҪ:
        //     �� bytes ʹ�� code ���������Ŀ��ٰ汾��
        //
        // ����:
        //   bytes:
        //     ԭʼ����������
        //
        //   code:
        //     ������������
        //
        // ���ؽ��:
        //     ����Ķ���������
        public static byte[] GetQuickXorBytes(byte[] bytes, byte[] code)
        {
            return GetXorBytes(bytes, 0, 220, code);
        }

        //
        // ժҪ:
        //     �� bytes ʹ�� code ���������Ŀ��ٰ汾���˷��������ò���д����� bytes ��Ϊ����ֵ��������������ڴ�ռ䡣
        //
        // ����:
        //   bytes:
        //     ԭʼ������Ķ���������
        //
        //   code:
        //     ������������
        public static void GetQuickSelfXorBytes(byte[] bytes, byte[] code)
        {
            GetSelfXorBytes(bytes, 0, 220, code);
        }

        //
        // ժҪ:
        //     �� bytes ʹ�� code ��������㡣
        //
        // ����:
        //   bytes:
        //     ԭʼ����������
        //
        //   code:
        //     ������������
        //
        // ���ؽ��:
        //     ����Ķ���������
        public static byte[] GetXorBytes(byte[] bytes, byte[] code)
        {
            if (bytes == null)
            {
                return null;
            }

            return GetXorBytes(bytes, 0, bytes.Length, code);
        }

        //
        // ժҪ:
        //     �� bytes ʹ�� code ��������㡣�˷��������ò���д����� bytes ��Ϊ����ֵ��������������ڴ�ռ䡣
        //
        // ����:
        //   bytes:
        //     ԭʼ������Ķ���������
        //
        //   code:
        //     ������������
        public static void GetSelfXorBytes(byte[] bytes, byte[] code)
        {
            if (bytes != null)
            {
                GetSelfXorBytes(bytes, 0, bytes.Length, code);
            }
        }

        //
        // ժҪ:
        //     �� bytes ʹ�� code ��������㡣
        //
        // ����:
        //   bytes:
        //     ԭʼ����������
        //
        //   startIndex:
        //     ������Ŀ�ʼλ�á�
        //
        //   length:
        //     �����㳤�ȣ���С�� 0���������������������
        //
        //   code:
        //     ������������
        //
        // ���ؽ��:
        //     ����Ķ���������
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
        // ժҪ:
        //     �� bytes ʹ�� code ��������㡣�˷��������ò���д����� bytes ��Ϊ����ֵ��������������ڴ�ռ䡣
        //
        // ����:
        //   bytes:
        //     ԭʼ������Ķ���������
        //
        //   startIndex:
        //     ������Ŀ�ʼλ�á�
        //
        //   length:
        //     �����㳤�ȡ�
        //
        //   code:
        //     ������������
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
    // ժҪ:
    //     JSON ��ص�ʵ�ú�����
    public static class Json
    {
        //
        // ժҪ:
        //     JSON �������ӿڡ�
        public interface IJsonHelper
        {
            //
            // ժҪ:
            //     ���������л�Ϊ JSON �ַ�����
            //
            // ����:
            //   obj:
            //     Ҫ���л��Ķ���
            //
            // ���ؽ��:
            //     ���л���� JSON �ַ�����
            string ToJson(object obj);

            //
            // ժҪ:
            //     �� JSON �ַ��������л�Ϊ����
            //
            // ����:
            //   json:
            //     Ҫ�����л��� JSON �ַ�����
            //
            // ���Ͳ���:
            //   T:
            //     �������͡�
            //
            // ���ؽ��:
            //     �����л���Ķ���
            T ToObject<T>(string json);

            //
            // ժҪ:
            //     �� JSON �ַ��������л�Ϊ����
            //
            // ����:
            //   objectType:
            //     �������͡�
            //
            //   json:
            //     Ҫ�����л��� JSON �ַ�����
            //
            // ���ؽ��:
            //     �����л���Ķ���
            object ToObject(Type objectType, string json);
        }

        private static IJsonHelper s_JsonHelper;

        //
        // ժҪ:
        //     ���� JSON ��������
        //
        // ����:
        //   jsonHelper:
        //     Ҫ���õ� JSON ��������
        public static void SetJsonHelper(IJsonHelper jsonHelper)
        {
            s_JsonHelper = jsonHelper;
        }

        //
        // ժҪ:
        //     ���������л�Ϊ JSON �ַ�����
        //
        // ����:
        //   obj:
        //     Ҫ���л��Ķ���
        //
        // ���ؽ��:
        //     ���л���� JSON �ַ�����
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
        // ժҪ:
        //     �� JSON �ַ��������л�Ϊ����
        //
        // ����:
        //   json:
        //     Ҫ�����л��� JSON �ַ�����
        //
        // ���Ͳ���:
        //   T:
        //     �������͡�
        //
        // ���ؽ��:
        //     �����л���Ķ���
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
        // ժҪ:
        //     �� JSON �ַ��������л�Ϊ����
        //
        // ����:
        //   objectType:
        //     �������͡�
        //
        //   json:
        //     Ҫ�����л��� JSON �ַ�����
        //
        // ���ؽ��:
        //     �����л���Ķ���
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
    // ժҪ:
    //     Marshal ��ص�ʵ�ú�����
    public static class Marshal
    {
        private const int BlockSize = 4096;

        private static IntPtr s_CachedHGlobalPtr = IntPtr.Zero;

        private static int s_CachedHGlobalSize = 0;

        //
        // ժҪ:
        //     ��ȡ����Ĵӽ��̵ķ��й��ڴ��з�����ڴ�Ĵ�С��
        public static int CachedHGlobalSize => s_CachedHGlobalSize;

        //
        // ժҪ:
        //     ȷ���ӽ��̵ķ��й��ڴ��з����㹻��С���ڴ沢���档
        //
        // ����:
        //   ensureSize:
        //     Ҫȷ���ӽ��̵ķ��й��ڴ��з����ڴ�Ĵ�С��
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
        // ժҪ:
        //     �ͷŻ���Ĵӽ��̵ķ��й��ڴ��з�����ڴ档
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
        // ժҪ:
        //     �����ݴӶ���ת��Ϊ����������
        //
        // ����:
        //   structure:
        //     Ҫת���Ķ���
        //
        // ���Ͳ���:
        //   T:
        //     Ҫת���Ķ�������͡�
        //
        // ���ؽ��:
        //     �洢ת������Ķ���������
        public static byte[] StructureToBytes<T>(T structure)
        {
            return StructureToBytes(structure, System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)));
        }

        //
        // ժҪ:
        //     �����ݴӶ���ת��Ϊ����������
        //
        // ����:
        //   structure:
        //     Ҫת���Ķ���
        //
        //   structureSize:
        //     Ҫת���Ķ���Ĵ�С��
        //
        // ���Ͳ���:
        //   T:
        //     Ҫת���Ķ�������͡�
        //
        // ���ؽ��:
        //     �洢ת������Ķ���������
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
        // ժҪ:
        //     �����ݴӶ���ת��Ϊ����������
        //
        // ����:
        //   structure:
        //     Ҫת���Ķ���
        //
        //   result:
        //     �洢ת������Ķ���������
        //
        // ���Ͳ���:
        //   T:
        //     Ҫת���Ķ�������͡�
        public static void StructureToBytes<T>(T structure, byte[] result)
        {
            StructureToBytes(structure, System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)), result, 0);
        }

        //
        // ժҪ:
        //     �����ݴӶ���ת��Ϊ����������
        //
        // ����:
        //   structure:
        //     Ҫת���Ķ���
        //
        //   structureSize:
        //     Ҫת���Ķ���Ĵ�С��
        //
        //   result:
        //     �洢ת������Ķ���������
        //
        // ���Ͳ���:
        //   T:
        //     Ҫת���Ķ�������͡�
        internal static void StructureToBytes<T>(T structure, int structureSize, byte[] result)
        {
            StructureToBytes(structure, structureSize, result, 0);
        }

        //
        // ժҪ:
        //     �����ݴӶ���ת��Ϊ����������
        //
        // ����:
        //   structure:
        //     Ҫת���Ķ���
        //
        //   result:
        //     �洢ת������Ķ���������
        //
        //   startIndex:
        //     д��洢ת������Ķ�����������ʼλ�á�
        //
        // ���Ͳ���:
        //   T:
        //     Ҫת���Ķ�������͡�
        public static void StructureToBytes<T>(T structure, byte[] result, int startIndex)
        {
            StructureToBytes(structure, System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)), result, startIndex);
        }

        //
        // ժҪ:
        //     �����ݴӶ���ת��Ϊ����������
        //
        // ����:
        //   structure:
        //     Ҫת���Ķ���
        //
        //   structureSize:
        //     Ҫת���Ķ���Ĵ�С��
        //
        //   result:
        //     �洢ת������Ķ���������
        //
        //   startIndex:
        //     д��洢ת������Ķ�����������ʼλ�á�
        //
        // ���Ͳ���:
        //   T:
        //     Ҫת���Ķ�������͡�
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
        // ժҪ:
        //     �����ݴӶ�������ת��Ϊ����
        //
        // ����:
        //   buffer:
        //     Ҫת���Ķ���������
        //
        // ���Ͳ���:
        //   T:
        //     Ҫת���Ķ�������͡�
        //
        // ���ؽ��:
        //     �洢ת������Ķ���
        public static T BytesToStructure<T>(byte[] buffer)
        {
            return BytesToStructure<T>(System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)), buffer, 0);
        }

        //
        // ժҪ:
        //     �����ݴӶ�������ת��Ϊ����
        //
        // ����:
        //   buffer:
        //     Ҫת���Ķ���������
        //
        //   startIndex:
        //     ��ȡҪת���Ķ�����������ʼλ�á�
        //
        // ���Ͳ���:
        //   T:
        //     Ҫת���Ķ�������͡�
        //
        // ���ؽ��:
        //     �洢ת������Ķ���
        public static T BytesToStructure<T>(byte[] buffer, int startIndex)
        {
            return BytesToStructure<T>(System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)), buffer, startIndex);
        }

        //
        // ժҪ:
        //     �����ݴӶ�������ת��Ϊ����
        //
        // ����:
        //   structureSize:
        //     Ҫת���Ķ���Ĵ�С��
        //
        //   buffer:
        //     Ҫת���Ķ���������
        //
        // ���Ͳ���:
        //   T:
        //     Ҫת���Ķ�������͡�
        //
        // ���ؽ��:
        //     �洢ת������Ķ���
        internal static T BytesToStructure<T>(int structureSize, byte[] buffer)
        {
            return BytesToStructure<T>(structureSize, buffer, 0);
        }

        //
        // ժҪ:
        //     �����ݴӶ�������ת��Ϊ����
        //
        // ����:
        //   structureSize:
        //     Ҫת���Ķ���Ĵ�С��
        //
        //   buffer:
        //     Ҫת���Ķ���������
        //
        //   startIndex:
        //     ��ȡҪת���Ķ�����������ʼλ�á�
        //
        // ���Ͳ���:
        //   T:
        //     Ҫת���Ķ�������͡�
        //
        // ���ؽ��:
        //     �洢ת������Ķ���
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
    // ժҪ:
    //     ·����ص�ʵ�ú�����
    public static class Path
    {
        //
        // ժҪ:
        //     ��ȡ�淶��·����
        //
        // ����:
        //   path:
        //     Ҫ�淶��·����
        //
        // ���ؽ��:
        //     �淶��·����
        public static string GetRegularPath(string path)
        {
            return path?.Replace('\\', '/');
        }

        //
        // ժҪ:
        //     ��ȡԶ�̸�ʽ��·��������file:// �� http:// ǰ׺����
        //
        // ����:
        //   path:
        //     ԭʼ·����
        //
        // ���ؽ��:
        //     Զ�̸�ʽ·����
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
        // ժҪ:
        //     �Ƴ����ļ��С�
        //
        // ����:
        //   directoryName:
        //     Ҫ������ļ������ơ�
        //
        // ���ؽ��:
        //     �Ƿ��Ƴ����ļ��гɹ���
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
    // ժҪ:
    //     �����ص�ʵ�ú�����
    public static class Random
    {
        private static System.Random s_Random = new System.Random((int)DateTime.UtcNow.Ticks);

        //
        // ժҪ:
        //     ������������ӡ�
        //
        // ����:
        //   seed:
        //     ��������ӡ�
        public static void SetSeed(int seed)
        {
            s_Random = new System.Random(seed);
        }

        //
        // ժҪ:
        //     ���طǸ��������
        //
        // ���ؽ��:
        //     ���ڵ�������С�� System.Int32.MaxValue �� 32 λ������������
        public static int GetRandom()
        {
            return s_Random.Next();
        }

        //
        // ժҪ:
        //     ����һ��С����ָ�����ֵ�ķǸ��������
        //
        // ����:
        //   maxValue:
        //     Ҫ���ɵ���������Ͻ磨���������ȡ���Ͻ�ֵ����maxValue ������ڵ����㡣
        //
        // ���ؽ��:
        //     ���ڵ�������С�� maxValue �� 32 λ��������������������ֵ�ķ�Χͨ�������㵫������ maxValue����������� maxValue �����㣬�򷵻�
        //     maxValue��
        public static int GetRandom(int maxValue)
        {
            return s_Random.Next(maxValue);
        }

        //
        // ժҪ:
        //     ����һ��ָ����Χ�ڵ��������
        //
        // ����:
        //   minValue:
        //     ���ص���������½磨�������ȡ���½�ֵ����
        //
        //   maxValue:
        //     ���ص���������Ͻ磨���������ȡ���Ͻ�ֵ����maxValue ������ڵ��� minValue��
        //
        // ���ؽ��:
        //     һ�����ڵ��� minValue ��С�� maxValue �� 32 λ�������������������ص�ֵ��Χ���� minValue �������� maxValue�����
        //     minValue ���� maxValue���򷵻� minValue��
        public static int GetRandom(int minValue, int maxValue)
        {
            return s_Random.Next(minValue, maxValue);
        }

        //
        // ժҪ:
        //     ����һ������ 0.0 �� 1.0 ֮����������
        //
        // ���ؽ��:
        //     ���ڵ��� 0.0 ����С�� 1.0 ��˫���ȸ�������
        public static double GetRandomDouble()
        {
            return s_Random.NextDouble();
        }

        //
        // ժҪ:
        //     ����������ָ���ֽ������Ԫ�ء�
        //
        // ����:
        //   buffer:
        //     ������������ֽ����顣
        public static void GetRandomBytes(byte[] buffer)
        {
            s_Random.NextBytes(buffer);
        }
    }

    //
    // ժҪ:
    //     �ַ���ص�ʵ�ú�����
    public static class Text
    {
        private const int StringBuilderCapacity = 1024;

        [ThreadStatic]
        private static StringBuilder s_CachedStringBuilder;

        //
        // ժҪ:
        //     ��ȡ��ʽ���ַ�����
        //
        // ����:
        //   format:
        //     �ַ�����ʽ��
        //
        //   arg0:
        //     �ַ������� 0��
        //
        // ���ؽ��:
        //     ��ʽ������ַ�����
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
        // ժҪ:
        //     ��ȡ��ʽ���ַ�����
        //
        // ����:
        //   format:
        //     �ַ�����ʽ��
        //
        //   arg0:
        //     �ַ������� 0��
        //
        //   arg1:
        //     �ַ������� 1��
        //
        // ���ؽ��:
        //     ��ʽ������ַ�����
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
        // ժҪ:
        //     ��ȡ��ʽ���ַ�����
        //
        // ����:
        //   format:
        //     �ַ�����ʽ��
        //
        //   arg0:
        //     �ַ������� 0��
        //
        //   arg1:
        //     �ַ������� 1��
        //
        //   arg2:
        //     �ַ������� 2��
        //
        // ���ؽ��:
        //     ��ʽ������ַ�����
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
        // ժҪ:
        //     ��ȡ��ʽ���ַ�����
        //
        // ����:
        //   format:
        //     �ַ�����ʽ��
        //
        //   args:
        //     �ַ���������
        //
        // ���ؽ��:
        //     ��ʽ������ַ�����
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
    // ժҪ:
    //     У����ص�ʵ�ú�����
    public static class Verifier
    {
        //
        // ժҪ:
        //     CRC32 �㷨��
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
        // ժҪ:
        //     ������������� CRC32��
        //
        // ����:
        //   bytes:
        //     ָ���Ķ���������
        //
        // ���ؽ��:
        //     ������ CRC32��
        public static int GetCrc32(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new Exception("Bytes is invalid.");
            }

            return GetCrc32(bytes, 0, bytes.Length);
        }

        //
        // ժҪ:
        //     ������������� CRC32��
        //
        // ����:
        //   bytes:
        //     ָ���Ķ���������
        //
        //   offset:
        //     ����������ƫ�ơ�
        //
        //   length:
        //     ���������ĳ��ȡ�
        //
        // ���ؽ��:
        //     ������ CRC32��
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
        // ժҪ:
        //     ������������� CRC32��
        //
        // ����:
        //   stream:
        //     ָ���Ķ���������
        //
        // ���ؽ��:
        //     ������ CRC32��
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
        // ժҪ:
        //     ��ȡ CRC32 ��ֵ�Ķ��������顣
        //
        // ����:
        //   crc32:
        //     CRC32 ��ֵ��
        //
        // ���ؽ��:
        //     CRC32 ��ֵ�Ķ��������顣
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
        // ժҪ:
        //     ��ȡ CRC32 ��ֵ�Ķ��������顣
        //
        // ����:
        //   crc32:
        //     CRC32 ��ֵ��
        //
        //   bytes:
        //     Ҫ��Ž�������顣
        public static void GetCrc32Bytes(int crc32, byte[] bytes)
        {
            GetCrc32Bytes(crc32, bytes, 0);
        }

        //
        // ժҪ:
        //     ��ȡ CRC32 ��ֵ�Ķ��������顣
        //
        // ����:
        //   crc32:
        //     CRC32 ��ֵ��
        //
        //   bytes:
        //     Ҫ��Ž�������顣
        //
        //   offset:
        //     CRC32 ��ֵ�Ķ����������ڽ�������ڵ���ʼλ�á�
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