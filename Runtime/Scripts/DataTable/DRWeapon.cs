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
    /// 装备武器信息
    /// </summary>
    public class DRWeapon : DataRow
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取武器ID。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取武器名称。
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取对应道具ID。
        /// </summary>
        public int ItemID
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取模型预设。
        /// </summary>
        public string ModelPrefab
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取攻击力。
        /// </summary>
        public int Attack
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取攻击速度。
        /// </summary>
        public int Speed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取动作模组。
        /// </summary>
        public string Animations
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取武器类型。
        /// </summary>
        public int Type
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取消耗数量。
        /// </summary>
        public int Expend
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取装满数量。
        /// </summary>
        public int LoadedSum
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取单次装填数量。
        /// </summary>
        public int OnceLoadCount
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
            ItemID = int.Parse(columnStrings[index++]);
            ModelPrefab = columnStrings[index++];
            Attack = int.Parse(columnStrings[index++]);
            Speed = int.Parse(columnStrings[index++]);
            Animations = columnStrings[index++];
            Type = int.Parse(columnStrings[index++]);
            Expend = int.Parse(columnStrings[index++]);
            LoadedSum = int.Parse(columnStrings[index++]);
            OnceLoadCount = int.Parse(columnStrings[index++]);

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
                    ItemID = binaryReader.Read7BitEncodedInt32();
                    ModelPrefab = binaryReader.ReadString();
                    Attack = binaryReader.Read7BitEncodedInt32();
                    Speed = binaryReader.Read7BitEncodedInt32();
                    Animations = binaryReader.ReadString();
                    Type = binaryReader.Read7BitEncodedInt32();
                    Expend = binaryReader.Read7BitEncodedInt32();
                    LoadedSum = binaryReader.Read7BitEncodedInt32();
                    OnceLoadCount = binaryReader.Read7BitEncodedInt32();
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
