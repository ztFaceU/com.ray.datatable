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
    /// 场景交互文案表
    /// </summary>
    public class DRSceneInteractionText : DataRow
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
        /// 获取交互物名称。
        /// </summary>
        public string Title
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取面板类型枚举值。
        /// </summary>
        public int InteractionType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取子面板类型枚举值。
        /// </summary>
        public int InteractionSubType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取交互物图片。
        /// </summary>
        public string Image
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取背景。
        /// </summary>
        public string Bg
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取描述标题。
        /// </summary>
        public string DesTitle
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取描述。
        /// </summary>
        public string Des
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取使用效果。
        /// </summary>
        public string UseEffectDes
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取使用按钮文本。
        /// </summary>
        public string UseButtonText
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取取消按钮文本。
        /// </summary>
        public string CancelButtonText
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
            Title = columnStrings[index++];
            InteractionType = int.Parse(columnStrings[index++]);
            InteractionSubType = int.Parse(columnStrings[index++]);
            Image = columnStrings[index++];
            Bg = columnStrings[index++];
            DesTitle = columnStrings[index++];
            Des = columnStrings[index++];
            UseEffectDes = columnStrings[index++];
            UseButtonText = columnStrings[index++];
            CancelButtonText = columnStrings[index++];

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
                    Title = binaryReader.ReadString();
                    InteractionType = binaryReader.Read7BitEncodedInt32();
                    InteractionSubType = binaryReader.Read7BitEncodedInt32();
                    Image = binaryReader.ReadString();
                    Bg = binaryReader.ReadString();
                    DesTitle = binaryReader.ReadString();
                    Des = binaryReader.ReadString();
                    UseEffectDes = binaryReader.ReadString();
                    UseButtonText = binaryReader.ReadString();
                    CancelButtonText = binaryReader.ReadString();
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
