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
    /// 空投道具库配置
    /// </summary>
    public class DRAirDropGoodsLib : DataRow
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
        /// 获取类型。
        /// </summary>
        public int GoodsType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取第1天宝箱1道具列表。
        /// </summary>
        public string Day1_Box1_GoodsList
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取第1天宝箱2道具列表。
        /// </summary>
        public string Day1_Box2_GoodsList
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取第1天宝箱3道具列表。
        /// </summary>
        public string Day1_Box3_GoodsList
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取第2天宝箱1道具列表。
        /// </summary>
        public string Day2_Box1_GoodsList
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取第2天宝箱2道具列表。
        /// </summary>
        public string Day2_Box2_GoodsList
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取第2天宝箱3道具列表。
        /// </summary>
        public string Day2_Box3_GoodsList
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取第3天宝箱1道具列表。
        /// </summary>
        public string Day3_Box1_GoodsList
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取第3天宝箱2道具列表。
        /// </summary>
        public string Day3_Box2_GoodsList
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取第3天宝箱3道具列表。
        /// </summary>
        public string Day3_Box3_GoodsList
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
            GoodsType = int.Parse(columnStrings[index++]);
            Day1_Box1_GoodsList = columnStrings[index++];
            Day1_Box2_GoodsList = columnStrings[index++];
            Day1_Box3_GoodsList = columnStrings[index++];
            Day2_Box1_GoodsList = columnStrings[index++];
            Day2_Box2_GoodsList = columnStrings[index++];
            Day2_Box3_GoodsList = columnStrings[index++];
            Day3_Box1_GoodsList = columnStrings[index++];
            Day3_Box2_GoodsList = columnStrings[index++];
            Day3_Box3_GoodsList = columnStrings[index++];

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
                    GoodsType = binaryReader.Read7BitEncodedInt32();
                    Day1_Box1_GoodsList = binaryReader.ReadString();
                    Day1_Box2_GoodsList = binaryReader.ReadString();
                    Day1_Box3_GoodsList = binaryReader.ReadString();
                    Day2_Box1_GoodsList = binaryReader.ReadString();
                    Day2_Box2_GoodsList = binaryReader.ReadString();
                    Day2_Box3_GoodsList = binaryReader.ReadString();
                    Day3_Box1_GoodsList = binaryReader.ReadString();
                    Day3_Box2_GoodsList = binaryReader.ReadString();
                    Day3_Box3_GoodsList = binaryReader.ReadString();
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
