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
    /// 关卡事件配置
    /// </summary>
    public class DRLevelEvent : DataRow
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取事件ID。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取事件类型。
        /// </summary>
        public int EventType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取投放区域类型。
        /// </summary>
        public int PutMonsterAreaType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取投放区域id。
        /// </summary>
        public int PutMonsterAreaId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取道具投放区域id。
        /// </summary>
        public int PutGoodsAreaId
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
            EventType = int.Parse(columnStrings[index++]);
            PutMonsterAreaType = int.Parse(columnStrings[index++]);
            PutMonsterAreaId = int.Parse(columnStrings[index++]);
            PutGoodsAreaId = int.Parse(columnStrings[index++]);

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
                    EventType = binaryReader.Read7BitEncodedInt32();
                    PutMonsterAreaType = binaryReader.Read7BitEncodedInt32();
                    PutMonsterAreaId = binaryReader.Read7BitEncodedInt32();
                    PutGoodsAreaId = binaryReader.Read7BitEncodedInt32();
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
