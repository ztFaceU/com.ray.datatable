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
    /// 游戏事件表
    /// </summary>
    public class DRGameEvent : DataRow
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
        /// 获取分组。
        /// </summary>
        public int Group
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取名称。
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取标题。
        /// </summary>
        public string Title
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
        /// 获取图片。
        /// </summary>
        public string Image
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取音频。
        /// </summary>
        public string Audio
        {
            get;
            private set;
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
        /// 获取默认选项。
        /// </summary>
        public int DefaultOption
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取主ui按钮图片。
        /// </summary>
        public string ButtonIcon
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
            Group = int.Parse(columnStrings[index++]);
            Name = columnStrings[index++];
            Title = columnStrings[index++];
            Desc = columnStrings[index++];
            Image = columnStrings[index++];
            Audio = columnStrings[index++];
            EventType = int.Parse(columnStrings[index++]);
            DefaultOption = int.Parse(columnStrings[index++]);
            ButtonIcon = columnStrings[index++];

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
                    Group = binaryReader.Read7BitEncodedInt32();
                    Name = binaryReader.ReadString();
                    Title = binaryReader.ReadString();
                    Desc = binaryReader.ReadString();
                    Image = binaryReader.ReadString();
                    Audio = binaryReader.ReadString();
                    EventType = binaryReader.Read7BitEncodedInt32();
                    DefaultOption = binaryReader.Read7BitEncodedInt32();
                    ButtonIcon = binaryReader.ReadString();
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
