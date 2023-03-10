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
    /// 剧本章节表
    /// </summary>
    public class DRScreenplay : DataRow
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取ID。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取剧本ID。
        /// </summary>
        public int ScreenplayId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取章节ID。
        /// </summary>
        public int SectionId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取剧本名称。
        /// </summary>
        public string ScreenplayName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取章节名称。
        /// </summary>
        public string SectionName
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
        /// 获取最大人数。
        /// </summary>
        public int Max
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取最少人数。
        /// </summary>
        public int Min
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取玩法类型。
        /// </summary>
        public int Type
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取玩法参数。
        /// </summary>
        public string TypeParam
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取固定个人目标。
        /// </summary>
        public int FixTarget
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取准备时长(秒)。
        /// </summary>
        public int ReadySec
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取时间分组。
        /// </summary>
        public int TimeGroup
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取主要目标描述。
        /// </summary>
        public string MainMissionDesc
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取可选择角色。
        /// </summary>
        public string AvailableCharacters
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取事件组。
        /// </summary>
        public int EventGroup
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
            ScreenplayId = int.Parse(columnStrings[index++]);
            SectionId = int.Parse(columnStrings[index++]);
            ScreenplayName = columnStrings[index++];
            SectionName = columnStrings[index++];
            Desc = columnStrings[index++];
            Max = int.Parse(columnStrings[index++]);
            Min = int.Parse(columnStrings[index++]);
            Type = int.Parse(columnStrings[index++]);
            TypeParam = columnStrings[index++];
            FixTarget = int.Parse(columnStrings[index++]);
            ReadySec = int.Parse(columnStrings[index++]);
            TimeGroup = int.Parse(columnStrings[index++]);
            MainMissionDesc = columnStrings[index++];
            AvailableCharacters = columnStrings[index++];
            EventGroup = int.Parse(columnStrings[index++]);

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
                    ScreenplayId = binaryReader.Read7BitEncodedInt32();
                    SectionId = binaryReader.Read7BitEncodedInt32();
                    ScreenplayName = binaryReader.ReadString();
                    SectionName = binaryReader.ReadString();
                    Desc = binaryReader.ReadString();
                    Max = binaryReader.Read7BitEncodedInt32();
                    Min = binaryReader.Read7BitEncodedInt32();
                    Type = binaryReader.Read7BitEncodedInt32();
                    TypeParam = binaryReader.ReadString();
                    FixTarget = binaryReader.Read7BitEncodedInt32();
                    ReadySec = binaryReader.Read7BitEncodedInt32();
                    TimeGroup = binaryReader.Read7BitEncodedInt32();
                    MainMissionDesc = binaryReader.ReadString();
                    AvailableCharacters = binaryReader.ReadString();
                    EventGroup = binaryReader.Read7BitEncodedInt32();
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
