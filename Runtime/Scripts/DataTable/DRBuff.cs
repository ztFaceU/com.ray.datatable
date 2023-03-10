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
    public class DRBuff : DataRow
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取BuffDataID。
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
        /// 获取描述。
        /// </summary>
        public string Description
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取类型ID。
        /// </summary>
        public int BuffTypeId
        {
            get;
            private set;
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
        /// 获取时间。
        /// </summary>
        public int Duration
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取是否显示在UI上。
        /// </summary>
        public int ShowOnUI
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取图标。
        /// </summary>
        public string Icon
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取特效。
        /// </summary>
        public string Effect
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取叠加规则。
        /// </summary>
        public int BuffAddRule
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取叠加层数。
        /// </summary>
        public int MaxLayer
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取参数long。
        /// </summary>
        public long ParamLong1
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取参数long。
        /// </summary>
        public long ParamLong2
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取参数long。
        /// </summary>
        public long ParamLong3
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取中立/正面/负面。
        /// </summary>
        public int IsPositive
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取是否被电疗椅解除。
        /// </summary>
        public int IsRemoveByElectricHealChair
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取是否是恶化效果。
        /// </summary>
        public int IsCorruption
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取是否是异变知识buff。
        /// </summary>
        public int IsMutationKnowledgeBuff
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取是否是持续性恢复/伤害效果。
        /// </summary>
        public int IsContinuousBuff
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
            Description = columnStrings[index++];
            BuffTypeId = int.Parse(columnStrings[index++]);
            LevelId = int.Parse(columnStrings[index++]);
            Duration = int.Parse(columnStrings[index++]);
            ShowOnUI = int.Parse(columnStrings[index++]);
            Icon = columnStrings[index++];
            Effect = columnStrings[index++];
            BuffAddRule = int.Parse(columnStrings[index++]);
            MaxLayer = int.Parse(columnStrings[index++]);
            ParamLong1 = long.Parse(columnStrings[index++]);
            ParamLong2 = long.Parse(columnStrings[index++]);
            ParamLong3 = long.Parse(columnStrings[index++]);
            IsPositive = int.Parse(columnStrings[index++]);
            IsRemoveByElectricHealChair = int.Parse(columnStrings[index++]);
            IsCorruption = int.Parse(columnStrings[index++]);
            IsMutationKnowledgeBuff = int.Parse(columnStrings[index++]);
            IsContinuousBuff = int.Parse(columnStrings[index++]);

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
                    Description = binaryReader.ReadString();
                    BuffTypeId = binaryReader.Read7BitEncodedInt32();
                    LevelId = binaryReader.Read7BitEncodedInt32();
                    Duration = binaryReader.Read7BitEncodedInt32();
                    ShowOnUI = binaryReader.Read7BitEncodedInt32();
                    Icon = binaryReader.ReadString();
                    Effect = binaryReader.ReadString();
                    BuffAddRule = binaryReader.Read7BitEncodedInt32();
                    MaxLayer = binaryReader.Read7BitEncodedInt32();
                    ParamLong1 = binaryReader.Read7BitEncodedInt64();
                    ParamLong2 = binaryReader.Read7BitEncodedInt64();
                    ParamLong3 = binaryReader.Read7BitEncodedInt64();
                    IsPositive = binaryReader.Read7BitEncodedInt32();
                    IsRemoveByElectricHealChair = binaryReader.Read7BitEncodedInt32();
                    IsCorruption = binaryReader.Read7BitEncodedInt32();
                    IsMutationKnowledgeBuff = binaryReader.Read7BitEncodedInt32();
                    IsContinuousBuff = binaryReader.Read7BitEncodedInt32();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private KeyValuePair<int, long>[] m_ParamLong = null;

        public int ParamLongCount
        {
            get
            {
                return m_ParamLong.Length;
            }
        }

        public long GetParamLong(int id)
        {
            foreach (KeyValuePair<int, long> i in m_ParamLong)
            {
                if (i.Key == id)
                {
                    return i.Value;
                }
            }

            return 0;        }

        public long GetParamLongAt(int index)
        {
            if (index < 0 || index >= m_ParamLong.Length)
            {
            }

            return m_ParamLong[index].Value;
        }

        private void GeneratePropertyArray()
        {
            m_ParamLong = new KeyValuePair<int, long>[]
            {
                new KeyValuePair<int, long>(1, ParamLong1),
                new KeyValuePair<int, long>(2, ParamLong2),
                new KeyValuePair<int, long>(3, ParamLong3),
            };
        }
    }
}
