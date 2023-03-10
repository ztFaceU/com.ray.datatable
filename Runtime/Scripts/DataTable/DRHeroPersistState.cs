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
    /// 角色持续表现表
    /// </summary>
    public class DRHeroPersistState : DataRow
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
        /// 获取类型。
        /// </summary>
        public int Type
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取可否移动。
        /// </summary>
        public int CanMove
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取可否移动打断。
        /// </summary>
        public int CanMoveBreak
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取持续时长。
        /// </summary>
        public float Duration
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取持续中动画ID。
        /// </summary>
        public int PersistAniIndex
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
            Type = int.Parse(columnStrings[index++]);
            CanMove = int.Parse(columnStrings[index++]);
            CanMoveBreak = int.Parse(columnStrings[index++]);
            Duration = float.Parse(columnStrings[index++]);
            PersistAniIndex = int.Parse(columnStrings[index++]);

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
                    Type = binaryReader.Read7BitEncodedInt32();
                    CanMove = binaryReader.Read7BitEncodedInt32();
                    CanMoveBreak = binaryReader.Read7BitEncodedInt32();
                    Duration = binaryReader.ReadSingle();
                    PersistAniIndex = binaryReader.Read7BitEncodedInt32();
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
