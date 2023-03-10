//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：
//------------------------------------------------------------

using System.IO;
using System.Text;
using TheMist.DataTable;
using System.Collections.Generic;

namespace TheMist
{
    /// <summary>
    /// 系统配置表
    /// </summary>
    public class DRSystem : DataRow
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取模块名称 + 键名称。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取模块名称。
        /// </summary>
        public string Model
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取键名称。
        /// </summary>
        public string Key
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取值类型。
        /// </summary>
        public int ValueType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取值。
        /// </summary>
        public string Value
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            m_Id = int.Parse(columnStrings[index++]);
            Model = columnStrings[index++];
            Key = columnStrings[index++];
            ValueType = int.Parse(columnStrings[index++]);
            Value = columnStrings[index++];

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    Model = binaryReader.ReadString();
                    Key = binaryReader.ReadString();
                    ValueType = binaryReader.Read7BitEncodedInt32();
                    Value = binaryReader.ReadString();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }
}
