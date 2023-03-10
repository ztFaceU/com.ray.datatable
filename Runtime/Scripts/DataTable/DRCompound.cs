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
    /// 合成表
    /// </summary>
    public class DRCompound : DataRow
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取道具id。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取类型。
        /// </summary>
        public int Type
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取描述。
        /// </summary>
        public string Desc
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取所需材料。
        /// </summary>
        public string Consume
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取需要合成持续表现。
        /// </summary>
        public int IsPersist
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取合成持续表现ID。
        /// </summary>
        public int PersistID
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
            Type = int.Parse(columnStrings[index++]);
            Desc = columnStrings[index++];
            Consume = columnStrings[index++];
            IsPersist = int.Parse(columnStrings[index++]);
            PersistID = int.Parse(columnStrings[index++]);

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
                    Type = binaryReader.Read7BitEncodedInt32();
                    Desc = binaryReader.ReadString();
                    Consume = binaryReader.ReadString();
                    IsPersist = binaryReader.Read7BitEncodedInt32();
                    PersistID = binaryReader.Read7BitEncodedInt32();
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
