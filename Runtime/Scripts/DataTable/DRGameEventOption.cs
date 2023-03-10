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
    /// 游戏事件选项表
    /// </summary>
    public class DRGameEventOption : DataRow
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
        /// 获取名称。
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取出现条件。
        /// </summary>
        public string DisplayCondition
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取检定类型(属性id)。
        /// </summary>
        public int ChallengeType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取调整效果说明。
        /// </summary>
        public string AdjustmentText
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取成功结果。
        /// </summary>
        public string Success
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取失败结果。
        /// </summary>
        public string Failure
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取菜单文本。
        /// </summary>
        public string MenuText
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取选项文本。
        /// </summary>
        public string OptionText
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
            Name = columnStrings[index++];
            DisplayCondition = columnStrings[index++];
            ChallengeType = int.Parse(columnStrings[index++]);
            AdjustmentText = columnStrings[index++];
            Success = columnStrings[index++];
            Failure = columnStrings[index++];
            MenuText = columnStrings[index++];
            OptionText = columnStrings[index++];

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
                    Name = binaryReader.ReadString();
                    DisplayCondition = binaryReader.ReadString();
                    ChallengeType = binaryReader.Read7BitEncodedInt32();
                    AdjustmentText = binaryReader.ReadString();
                    Success = binaryReader.ReadString();
                    Failure = binaryReader.ReadString();
                    MenuText = binaryReader.ReadString();
                    OptionText = binaryReader.ReadString();
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
