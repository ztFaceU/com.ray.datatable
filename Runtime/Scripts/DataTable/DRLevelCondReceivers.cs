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
    /// 关卡接收器配置
    /// </summary>
    public class DRLevelCondReceivers : DataRow
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取唯一Id。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取关卡ID。
        /// </summary>
        public int LevelId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取接收器id。
        /// </summary>
        public int ReceiverId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取接收器类型。
        /// </summary>
        public int ReceiverType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取关卡事件ID。
        /// </summary>
        public int LevelEventId
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
            LevelId = int.Parse(columnStrings[index++]);
            ReceiverId = int.Parse(columnStrings[index++]);
            ReceiverType = int.Parse(columnStrings[index++]);
            LevelEventId = int.Parse(columnStrings[index++]);

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
                    LevelId = binaryReader.Read7BitEncodedInt32();
                    ReceiverId = binaryReader.Read7BitEncodedInt32();
                    ReceiverType = binaryReader.Read7BitEncodedInt32();
                    LevelEventId = binaryReader.Read7BitEncodedInt32();
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
