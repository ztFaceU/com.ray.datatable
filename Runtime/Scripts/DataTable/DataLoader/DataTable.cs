using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMist.DataTable
{
    public interface IDataTable
    {
        public void LoadBytes(byte[] bytes);

    }

    public class DataTable<T> : IDataTable where T : DataRow
    {

        public DataTable() { }

        private Dictionary<int, T> allDataDic;

        public void LoadBytes(byte[] bytes)
        {
            //从Loader中取出对应Byte[]，转换并生成rows数据，至此配置数据准备结束,单行转换调用DataRow中的Parse
            allDataDic = new Dictionary<int, T>();

            using (MemoryStream memoryStream = new MemoryStream(bytes, 0, bytes.Length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                    {
                        int dataRowBytesLength = binaryReader.Read7BitEncodedInt32();
                        AddRow(bytes, (int)binaryReader.BaseStream.Position, dataRowBytesLength);
                        binaryReader.BaseStream.Position += dataRowBytesLength;
                    }
                }
            }
        }

        public List<T> GetAllRows()
        {
            return allDataDic.Values.ToList();
        }

        public T GetFirst(Predicate<T> match)
        {
            var data = allDataDic.Values.ToList().Find(match);
            return data;
        }

        public List<T> GetAll(Predicate<T> match)
        {
            var data = allDataDic.Values.ToList().FindAll(match);
            return data;
        }

        public T GetRow(int id)
        {
            if(allDataDic.TryGetValue(id,out var data))
            {
                return data;
            }
            return default(T);
        }

        public int Count
        {
            get
            {
                return allDataDic.Count;
            }
        }

        private void AddRow(byte[] bytes,int startIndex,int length)
        {
            Type dataRowType = typeof(T);
            var row = (T)Activator.CreateInstance(dataRowType);
            row.ParseDataRow(bytes, startIndex, length);
            allDataDic.Add(row.Id, row);
        }
    }
}
