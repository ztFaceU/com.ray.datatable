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
    /// 建筑区域buff信息
    /// </summary>
    public class DRBuildingBuff : DataRow
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
        /// 获取场景ID。
        /// </summary>
        public int SceneId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取区域类型。
        /// </summary>
        public int AreaType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取白天BuffId。
        /// </summary>
        public int DayLightBuffId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取晚上BuffId。
        /// </summary>
        public int NightBuffId
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
            SceneId = int.Parse(columnStrings[index++]);
            AreaType = int.Parse(columnStrings[index++]);
            DayLightBuffId = int.Parse(columnStrings[index++]);
            NightBuffId = int.Parse(columnStrings[index++]);

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
                    SceneId = binaryReader.Read7BitEncodedInt32();
                    AreaType = binaryReader.Read7BitEncodedInt32();
                    DayLightBuffId = binaryReader.Read7BitEncodedInt32();
                    NightBuffId = binaryReader.Read7BitEncodedInt32();
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
