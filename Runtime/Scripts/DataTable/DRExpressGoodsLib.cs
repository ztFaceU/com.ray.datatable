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
    /// 快递物资包配置
    /// </summary>
    public class DRExpressGoodsLib : DataRow
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取唯一ID。
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
        /// 获取道具库id。
        /// </summary>
        public int GoodsLibId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取时间范围。
        /// </summary>
        public int TimeRange
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取单次投放量。
        /// </summary>
        public int UnitPutCount
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取道具权重列表。
        /// </summary>
        public string GoodsWeightList
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
            GoodsLibId = int.Parse(columnStrings[index++]);
            TimeRange = int.Parse(columnStrings[index++]);
            UnitPutCount = int.Parse(columnStrings[index++]);
            GoodsWeightList = columnStrings[index++];

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
                    GoodsLibId = binaryReader.Read7BitEncodedInt32();
                    TimeRange = binaryReader.Read7BitEncodedInt32();
                    UnitPutCount = binaryReader.Read7BitEncodedInt32();
                    GoodsWeightList = binaryReader.ReadString();
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
