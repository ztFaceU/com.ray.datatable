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
    /// 道具库配置
    /// </summary>
    public class DRGoodsLib : DataRow
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
        /// 获取区域ID。
        /// </summary>
        public int SceneAreaId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取箱子类型。
        /// </summary>
        public int BoxType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取单个箱子投放道具数量。
        /// </summary>
        public int SingleBoxPutCount
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
            SceneAreaId = int.Parse(columnStrings[index++]);
            BoxType = int.Parse(columnStrings[index++]);
            SingleBoxPutCount = int.Parse(columnStrings[index++]);
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
                    SceneAreaId = binaryReader.Read7BitEncodedInt32();
                    BoxType = binaryReader.Read7BitEncodedInt32();
                    SingleBoxPutCount = binaryReader.Read7BitEncodedInt32();
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
