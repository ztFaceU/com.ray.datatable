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
    /// 怪物信息
    /// </summary>
    public class DRMonster : DataRow
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取怪物ID。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取怪物名称。
        /// </summary>
        public string Name
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
        /// 获取生命值。
        /// </summary>
        public int HP
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取怪物类型。
        /// </summary>
        public int RankType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取掉落物品ID。
        /// </summary>
        public int DropGoodsId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取掉落物品数量概率权重。
        /// </summary>
        public string DropGoodsNumWeight
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取掉落包裹的预设。
        /// </summary>
        public string DropPackagePrefabName
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
            ModelPrefab = columnStrings[index++];
            Attack = int.Parse(columnStrings[index++]);
            Speed = int.Parse(columnStrings[index++]);
            HP = int.Parse(columnStrings[index++]);
            RankType = int.Parse(columnStrings[index++]);
            DropGoodsId = int.Parse(columnStrings[index++]);
            DropGoodsNumWeight = columnStrings[index++];
            DropPackagePrefabName = columnStrings[index++];

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
                    ModelPrefab = binaryReader.ReadString();
                    Attack = binaryReader.Read7BitEncodedInt32();
                    Speed = binaryReader.Read7BitEncodedInt32();
                    HP = binaryReader.Read7BitEncodedInt32();
                    RankType = binaryReader.Read7BitEncodedInt32();
                    DropGoodsId = binaryReader.Read7BitEncodedInt32();
                    DropGoodsNumWeight = binaryReader.ReadString();
                    DropPackagePrefabName = binaryReader.ReadString();
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
