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
    /// 信息
    /// </summary>
    public class DRSkill : DataRow
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取SkillID。
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
        public string Description
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取类型ID。
        /// </summary>
        public string Prefab
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取冷却。
        /// </summary>
        public int Cd
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取消耗释放开启。
        /// </summary>
        public int NeedEnable
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取吟唱持续动作id。
        /// </summary>
        public int PersistStateId
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
            Description = columnStrings[index++];
            Prefab = columnStrings[index++];
            Cd = int.Parse(columnStrings[index++]);
            NeedEnable = int.Parse(columnStrings[index++]);
            PersistStateId = int.Parse(columnStrings[index++]);

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
                    Description = binaryReader.ReadString();
                    Prefab = binaryReader.ReadString();
                    Cd = binaryReader.Read7BitEncodedInt32();
                    NeedEnable = binaryReader.Read7BitEncodedInt32();
                    PersistStateId = binaryReader.Read7BitEncodedInt32();
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
