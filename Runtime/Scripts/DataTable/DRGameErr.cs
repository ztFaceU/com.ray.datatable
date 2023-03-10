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
    /// 错误表
    /// </summary>
    public class DRGameErr : DataRow
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取错误描述(%s表示参数）。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取。
        /// </summary>
        public string Msg
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取。
        /// </summary>
        public string Msg_cnt
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取。
        /// </summary>
        public string Msg_en
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
            Msg = columnStrings[index++];
            Msg_cnt = columnStrings[index++];
            Msg_en = columnStrings[index++];

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
                    Msg = binaryReader.ReadString();
                    Msg_cnt = binaryReader.ReadString();
                    Msg_en = binaryReader.ReadString();
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
